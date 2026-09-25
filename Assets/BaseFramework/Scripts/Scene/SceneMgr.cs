
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseFramework.Runtime
{
    /// <summary>
    /// 场景加载管理器
    /// </summary>
    public class SceneMgr : SingletonNormal<SceneMgr>
    {
        private SceneMgr() { }

        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="name"></param>
        public void LoadScene(string name, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            LoadScene(name, null, loadmode);
        }
        public void LoadScene(string name, Action action, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(name, loadmode);
            action?.Invoke();
        }

        public void LoadScene(int index, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            LoadScene(index, null, loadmode);
        }
        public void LoadScene(int index, Action action, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            string sceneName = Utility.String.SubResourceName(SceneUtility.GetScenePathByBuildIndex(index));
            LoadScene(sceneName, action, loadmode);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public void LoadSceneAsync(string name, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            LoadSceneAsync(name, null, loadmode);
        }
        public void LoadSceneAsync(string name, Action callback, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            MonoMgr.GetInstance().StartMyCoroutine(LoadSceneAsyncCoroutine(name, callback, loadmode));
        }

        public void LoadSceneAsync(int index, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            LoadSceneAsync(index, null, loadmode);
        }
        public void LoadSceneAsync(int index, Action callback, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            string sceneName = Utility.String.SubResourceName(SceneUtility.GetScenePathByBuildIndex(index));
            LoadSceneAsync(sceneName, callback, loadmode);
        }

        private IEnumerator LoadSceneAsyncCoroutine(string name, Action callback, LoadSceneMode loadmode = LoadSceneMode.Single)
        {
            var ao = SceneManager.LoadSceneAsync(name, loadmode);

            while (!ao.isDone)
            {
                //通过事件中心发送加载进度
                EventCenter.GetInstance().EventTrigger<float>(EventConfig.LoadSceneProgress, ao.progress);
                yield return null;
            }

            EventCenter.GetInstance().EventTrigger<float>(EventConfig.LoadSceneProgress, 1);
            callback?.Invoke();
        }
    }
}