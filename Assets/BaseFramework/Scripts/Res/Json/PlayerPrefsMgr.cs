
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// PlayerPrefs数据管理器
    /// </summary>
    public sealed class PlayerPrefsMgr : SingletonNormal<PlayerPrefsMgr>
    {
        private PlayerPrefsMgr() { }

        private List<string> _keys = new();

        /// <summary>
        /// 保存数据<br\>
        /// 如果传入的数据为null，对应的key为key_IsNull
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="key">数据唯一Key</param>
        public void SaveData(string key, object data)
        {
            DeleteInternal(key);    //清理旧数据，防止脏数据
            
            if (data == null)
            {
                //TODO:打印错误日志
                Debug.LogError($"传入数据为空,对应的key为{key}");
                PlayerPrefs.SetInt(key + "_IsNull", 1);
                AddKey(key + "_IsNull");
                PlayerPrefs.Save();
                JsonFileMgr.GetInstance().SaveData(_keys, "playerprefsKeys");
                return;
            }

            Type type = data.GetType();
            FieldInfo[] fieldInfos = type.GetFields();  //返回传入数据类型的所有公共字段
            string saveKey = string.Empty;

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                //数据key=传入的key_类型名_字段类型名_字段名
                saveKey = $"{key}_{type.FullName}_{fieldInfos[i].FieldType.FullName}_{fieldInfos[i].Name}";
                //如果是自定义类，可以通过反射获取字段数据
                SaveValue(saveKey, fieldInfos[i].GetValue(data));
            }
            PlayerPrefs.Save();
            JsonFileMgr.GetInstance().SaveData(_keys, "playerprefsKeys");
        }

        /// <summary>
        /// 存储不同类型的数据对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        private void SaveValue(string key, object value)
        {
            if (value == null)
            {
                PlayerPrefs.SetInt(key + "_IsNull", 1);
               AddKey(key + "_IsNull");
                return;
            }

            Type type = value.GetType();
            if (type == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)value);
                AddKey(key);
            }
            else if (type == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)value);
                AddKey(key);
            }
            else if (type == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)value);
                AddKey(key);
            }
            else if (type == typeof(bool))
            {
                PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
                AddKey(key);
            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                IList list = value as IList;

                PlayerPrefs.SetInt(key + "_Count", list.Count);
                AddKey(key + "_Count");

                int index = 0;
                foreach (object item in list)
                {
                    SaveValue($"{key}_{index}", item);
                    ++index;
                }
            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                IDictionary dict = value as IDictionary;

                PlayerPrefs.SetInt(key + "_Count", dict.Keys.Count);
                AddKey(key + "_Count");

                int keyIndex = 0;
                foreach (object item in dict.Keys)
                {
                    SaveValue($"{key}_Key_{keyIndex}", item);
                    SaveValue($"{key}_Value_{keyIndex}", dict[item]);
                    ++keyIndex;
                }
            }
            else
            {
                FieldInfo[] fields = type.GetFields();
                foreach (FieldInfo item in fields)
                {
                    string fieldKey = $"{key}_{item.Name}";
                    SaveValue(fieldKey, item.GetValue(value));
                }
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="type">要获取的数据类型</param>
        /// <returns></returns>
        public object LoadData(string key, Type type)
        {
            if (string.IsNullOrEmpty(key))
            {
                //TODO:打印错误日志
                Debug.LogError("传入的key为空");
                return null;
            }

            if (PlayerPrefs.HasKey(key + "_IsNull") &&PlayerPrefs.GetInt(key + "_IsNull") == 1)
            {
                return null;
            }


            object data = Activator.CreateInstance(type);
            FieldInfo[] fieldInfos = type.GetFields();
            string loadKey = string.Empty;

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                loadKey = $"{key}_{type.FullName}_{fieldInfos[i].FieldType.FullName}_{fieldInfos[i].Name}";
                fieldInfos[i].SetValue(data, LoadValue(loadKey, fieldInfos[i].FieldType));
            }

            return data;
        }

        private object LoadValue(string key, Type type)
        {
            if (PlayerPrefs.HasKey(key + "_IsNull") && PlayerPrefs.GetInt(key + "_IsNull") == 1)
            {
                return null;
            }

            if (type == typeof(int))
            {
                return PlayerPrefs.GetInt(key,0);
            }
            else if (type == typeof(float))
            {
                return PlayerPrefs.GetFloat(key,0);
            }
            else if (type == typeof(string))
            {
                return PlayerPrefs.GetString(key,string.Empty);

            }
            else if (type == typeof(bool))
            {
                return PlayerPrefs.GetInt(key,0) == 1 ? true : false;

            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                int count = PlayerPrefs.GetInt(key + "_Count",0);
                IList list = Activator.CreateInstance(type) as IList;
                for (int i = 0; i < count; i++)
                {
                    list.Add(LoadValue($"{key}_{i}", type.GetGenericArguments()[0]));
                }
                return list;

            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                int keyCount = PlayerPrefs.GetInt(key + "_Count",0);

                IDictionary dict = Activator.CreateInstance(type) as IDictionary;

                Type[] keyType = type.GetGenericArguments();
                for (int i = 0; i < keyCount; i++)
                {
                    dict.Add(LoadValue($"{key}_Key_{i}", keyType[0]), LoadValue($"{key}_Value_{i}", keyType[1]));
                }

                return dict;
            }
            else
            {
                object obj = Activator.CreateInstance(type);
                FieldInfo[] fields = type.GetFields();

                string newKey = string.Empty;
                foreach (var item in fields)
                {
                    newKey = $"{key}_{item.Name}";
                    item.SetValue(obj, LoadValue(newKey, item.FieldType));
                }
                return obj;
            }
        }
        /// <summary>
        /// 删除指定key的数据
        /// </summary>
        /// <param name="key"></param>
        public void Delete(string key)
        {
            DeleteInternal(key);

            PlayerPrefs.Save();
            JsonFileMgr.GetInstance().SaveData(_keys, "playerprefsKeys");
        }

        private void DeleteInternal(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                //TODO:打印错误日志
                Debug.LogError("传入的key为空");
                return;
            }

            if (_keys == null || _keys.Count <= 0)
            {
                _keys = JsonFileMgr.GetInstance().LoadData<List<string>>("playerprefsKeys") ?? new List<string>();
            }

            string prefix = key + "_";
            foreach (string saveKey in new List<string>(_keys))
            {
                if (saveKey == key || saveKey.StartsWith(prefix))
                {
                    PlayerPrefs.DeleteKey(saveKey);
                    _keys.Remove(saveKey);
                }
            }
        }

        /// <summary>
        /// 清理所有数据
        /// </summary>
        public void ClearAll()
        {
            _keys.Clear();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            JsonFileMgr.GetInstance().SaveData(_keys, "playerprefsKeys");
        }

        private void AddKey(string key)
        {
            if (!_keys.Contains(key))
            {
                _keys.Add(key);
            }
        }
    }
}