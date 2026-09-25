using BaseFramework.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>对象池</summary>
    public sealed class Pool
    {
        private readonly Stack<GameObject> _objAvailableStack = new();   //空闲对象栈
        private readonly GameObject _prefab;    //缓存原型，用于池子空的时候进行实例化
        private GameObject _father;     //池子父节点，所有失活对象都在该节点下面

        private readonly HashSet<GameObject> _objUsedHashset = new();    //池子外物体的列表
        private int _capacity;    //池子容量
        private readonly int _maxCapacity;   //池子最大容量
        private readonly int _expandAmount; //每次扩容数量
        private readonly bool _autoExpand;  //是否允许自动扩容


        public int AvailableCount => _objAvailableStack.Count;    //空闲物体的数量
        public int UsedCount => _objUsedHashset.Count;    //使用中的对象数量
        public int TotalCount => AvailableCount + UsedCount;    //当前对象总数
        public int Capacity => _capacity;
        public int MaxCapacity => _maxCapacity;

        /// <summary>是否到达当前容量</summary>
        public bool IsFull => TotalCount >= _capacity;
        /// <summary>是否到达最大容量</summary>
        public bool IsMaxCapacity => TotalCount >= _maxCapacity;

        public Pool(GameObject prefab, Transform grandfather)
        {
            if (prefab == null)
            {
                //TODO:打印错误日志
                Debug.LogError("创建对象失败，传入参数为空");
                return;
            }

            _prefab = prefab;

            if (PoolMgr.IsOpenLayout)
            {
                _father = new GameObject(Utility.String.RemoveCloneSuffix(prefab.name) + 's');
                _father.transform.SetParent(grandfather);
            }

            PoolConfig config = _prefab.GetComponent<PoolConfig>();
            if (config == null)
            {
                //TODO：打印警告日志
                Debug.LogWarning("没有挂载PoolConfig脚本");

                _capacity = 10;
                _maxCapacity = 100;
                _expandAmount = 10;
                _autoExpand = true;
            }
            else
            {
                _capacity = Mathf.Max(1, config.InitialCapacity);
                _maxCapacity = Mathf.Max(_capacity, config.MaxCapacity);
                _expandAmount = Mathf.Max(1, config.ExpandAmount);
                _autoExpand = config.AutoExpand;

            }
        }

        public bool TryGet(out GameObject result)
        {
            result = null;

            if (_objAvailableStack.Count > 0)
            {
                result = _objAvailableStack.Pop();

                PrepareForSpawn(result);
                return true;
            }

            if (TotalCount >= _capacity)
            {
                if (!_autoExpand || !Expand())
                {
                    //TODO:打印警告日志
                    Debug.LogWarning($"{_prefab.name}对象池容量达到限制，无法获取对象");
                    return false;
                }
            }

            result = CreateObject();

            if (result == null)
            {
                return false;
            }

            PoolIdentity identity = GetOrCreateIdentity(result);
            identity.BindPool(this);
            _objUsedHashset.Add(result);
            PrepareForSpawn(result);

            return true;

        }


        /// <summary>
        /// 获取对象<br/>
        /// 获取失败返回null
        /// </summary>
        /// <returns></returns>
        public GameObject Get()
        {
            TryGet(out GameObject result);
            return result;
        }


        /// <summary>回收对象</summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Return(GameObject obj)
        {
            if (obj == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning("传入的参数不能为空");
                return false;
            }

            PoolIdentity poolIdentity = obj.GetComponent<PoolIdentity>();
            if (poolIdentity == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning($"对象{obj.name}没有挂载脚本PoolIdentity");
                return false;
            }

            if (poolIdentity.SelfPool != this)
            {
                //TODO:打印警告日志
                Debug.LogWarning($"对象{obj.name}不属于当前Pool");
                return false;
            }

            if (!_objUsedHashset.Remove(obj))
            {
                //TODO:打印警告日志
                Debug.LogWarning($"{obj.name}不属于正在使用的对象，禁止重复回收");
                return false;
            }

            poolIdentity.CallDespawn();   //执行回收生命周期函数
            obj.SetActive(false);

            if (PoolMgr.IsOpenLayout)
                obj.transform.SetParent(_father.transform);

            _objAvailableStack.Push(obj);

            return true;

        }
        /// <summary>手动扩容</summary>
        /// <param name="amount">扩容增量</param>
        /// <returns></returns>
        public bool Expand(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_capacity >= _maxCapacity)
            {
                return false;
            }

            _capacity = Mathf.Min(_capacity + amount, _maxCapacity);
            return true;
        }


        /// <summary>预热对象池</summary>
        /// <param name="count">对象数量</param>
        public void Prewarm(int count)
        {
            count = Mathf.Clamp(count, 0, _capacity);
            while (_objAvailableStack.Count + _objUsedHashset.Count < count)
            {
                GameObject obj = CreateObject();
                if (obj == null)
                {
                    break;
                }

                PoolIdentity identity = GetOrCreateIdentity(obj);
                identity.BindPool(this);
                obj.SetActive(false);
                if (PoolMgr.IsOpenLayout)
                    obj.transform.SetParent(_father.transform);
                _objAvailableStack.Push(obj);

            }
        }


        /// <summary>清理对象池</summary>
        public void Clear()
        {
            while (_objAvailableStack.Count > 0)
            {
                GameObject obj = _objAvailableStack.Pop();
                if (obj == null)
                {
                    continue;
                }
                PoolIdentity poolIdentity = obj.GetComponent<PoolIdentity>();
                poolIdentity?.UnbindPool();
                Object.Destroy(obj);
            }

            foreach (var item in _objUsedHashset)
            {
                if (item == null)
                {
                    continue;
                }
                PoolIdentity poolIdentity = item.GetComponent<PoolIdentity>();
                poolIdentity?.UnbindPool();
                Object.Destroy(item);
            }

            _objUsedHashset.Clear();


            if (_father != null)
            {
                GameObject.Destroy(_father);
                _father = null;
            }
        }


        public bool Destroy(GameObject obj)
        {
            if (obj == null)
            {
                return false;
            }
            PoolIdentity identity = obj.GetComponent<PoolIdentity>();
            if (identity == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning($"对象{obj.name}没有挂载脚本PoolIdentity");
                return false;
            }

            if (identity.SelfPool != this)
            {
                //TODO:打印警告日志
                Debug.LogWarning($"对象{obj.name}不属于当前Pool");
                return false;
            }

            _objUsedHashset.Remove(obj);
            RemoveFromAvailable(obj);
            if (obj.activeSelf)
            {
                identity.CallDespawn();
            }

            identity.UnbindPool();
            Object.Destroy(obj);
            return true;
        }

        private GameObject CreateObject()
        {
            GameObject obj = Object.Instantiate(_prefab);
            obj.name = _prefab.name;
            return obj;
        }
        private PoolIdentity GetOrCreateIdentity(GameObject obj)
        {
            PoolIdentity poolIdentity = obj.GetComponent<PoolIdentity>();
            if (poolIdentity == null)
            {
                poolIdentity = obj.AddComponent<PoolIdentity>();
            }
            return poolIdentity;
        }

        /// <summary>从空闲对象栈中移除对象</summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool RemoveFromAvailable(GameObject target)
        {
            if (_objAvailableStack.Count == 0)
            {
                return false;
            }
            bool found = false;

            Stack<GameObject> temp = new(_objAvailableStack.Count);
            while (_objAvailableStack.Count > 0)
            {
                GameObject item = _objAvailableStack.Pop();
                if (item == target)
                {
                    found = true;
                    continue;
                }
                temp.Push(item);
            }

            while (temp.Count > 0)
            {
                _objAvailableStack.Push(temp.Pop());
            }
            return found;
        }

        /// <summary>自动扩容</summary>
        /// <returns></returns>
        private bool Expand()
        {
            if (_capacity >= _maxCapacity)
            {
                return false;
            }

            _capacity = Mathf.Min(_capacity + _expandAmount, _maxCapacity);
            return true;
        }

        /// <summary>准备生成</summary>
        /// <param name="obj"></param>
        private void PrepareForSpawn(GameObject obj)
        {
            if (PoolMgr.IsOpenLayout)
            {
                obj.transform.SetParent(null);
            }

            obj.SetActive(true);
            PoolIdentity poolIdentity = GetOrCreateIdentity(obj);
            poolIdentity.BindPool(this);
            _objUsedHashset.Add(obj);
            poolIdentity.CallSpawn();
        }

    }
}
