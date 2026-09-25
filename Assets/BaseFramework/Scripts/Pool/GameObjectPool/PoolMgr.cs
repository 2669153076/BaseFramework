
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>对象池管理器</summary>
    public sealed partial class PoolMgr : SingletonNormal<PoolMgr>
    {
        public static bool IsOpenLayout = true; //是否开启布局，创建父子关系。真机下频繁修改父子关系会消耗性能

        private PoolMgr() { }

        private readonly Dictionary<string, Pool> _poolDic = new();  //对象池字典

        private GameObject _poolRootObj;    //对象池管理器根物体，在开启布局的情况下，所有的池子都生成在该节点下面

        /// <summary>获取对象</summary>
        /// <param name="path">对象路径或对象名</param>
        /// <returns></returns>
        public GameObject GetGameObject(string path)
        {
            Pool pool = GetOrCreatePool(path);

            if (pool == null)
            {
                //TODO:打印警告日志
                Debug.LogWarning("对象获取失败");
                return null;
            }

            return pool.Get();
        }



        /// <summary>放入对象</summary>
        /// <param name="path">对象路径或对象名</param>
        /// <param name="obj"></param>
        public bool PushGameObject(GameObject obj)
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
            return identity.ReturnToPool();
        }

        /// <summary>彻底销毁</summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
            return identity.DestoryFromPool();
        }

        /// <summary>预热对象池</summary>
        /// <param name="path"></param>
        /// <param name="count"></param>
        public void Prewarm(string path, int count)
        {
            Pool pool = GetOrCreatePool(path);
            pool?.Prewarm(count);
        }

        /// <summary>释放所有对象池</summary>
        public void Release()
        {
            foreach (var value in _poolDic.Values)
            {
                value.Clear();
            }
            _poolDic.Clear();

            if (_poolRootObj != null)
            {
                GameObject.Destroy(_poolRootObj);
                _poolRootObj = null;
            }
        }

        /// <summary>获取根节点</summary>
        /// <returns></returns>
        private Transform GetRoot()
        {
            if (IsOpenLayout && _poolRootObj == null)
            {
                _poolRootObj = new GameObject("PoolRoot");
            }
            return _poolRootObj == null ? null : _poolRootObj.transform;
        }

        private Pool GetOrCreatePool(string path)
        {
            string name = Utility.String.SubResourceName(path);
            if (!_poolDic.TryGetValue(name, out Pool pool))
            {
                //TODO：资源加载
                GameObject prefab = Resources.Load<GameObject>(path);
                if (prefab == null)
                {
                    //TODO:打印错误日志
                    Debug.LogError($"对象获取失败，路径错误或者对象不存在{path}");
                    return null;
                }
                prefab.name = name;
                pool = new Pool(prefab, GetRoot());
                _poolDic.Add(name, pool);
            }
            return pool;
        }
    }
}