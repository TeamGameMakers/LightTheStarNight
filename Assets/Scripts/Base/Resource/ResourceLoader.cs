using System;
using System.Collections;
using Base.Mono;
using UnityEngine;

namespace Base.Resource
{
    /// <summary>
    /// 资源加载器。
    /// 从 Resources 目录下加载资源。
    /// 提供同步、异步的加载方法。
    /// </summary>
    public static class ResourceLoader
    {
        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">在 Resources 下的子目录，如 "Prefabs/Player"</param>
        /// <param name="instantiateGameObject">当类型为 GameObject 时，是否直接实例化</param>
        /// <returns></returns>
        public static T Load<T>(string path, bool instantiateGameObject = true) where T : UnityEngine.Object
        {
            T res = Resources.Load<T>(path);
            if (res is GameObject && instantiateGameObject) {
                return GameObject.Instantiate(res);
            }
            return res;
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">在 Resources 下的子目录，如 "Prefabs/Player"</param>
        /// <param name="callback">加载结束的回调函数</param>
        /// <param name="instantiateGameObject">当类型为 GameObject 时，是否直接实例化</param>
        public static void LoadAsync<T>(string path, Action<T> callback, bool instantiateGameObject = true) where T : UnityEngine.Object
        {
            MonoManager.Instance.StartCoroutine(LoadAsyncCoroutine(path, callback, instantiateGameObject));
        }

        private static IEnumerator LoadAsyncCoroutine<T>(string path, Action<T> callback, bool instantiateGameObject) where T : UnityEngine.Object
        {
            ResourceRequest rq = Resources.LoadAsync<T>(path);
            yield return rq;

            if (rq.asset is GameObject && instantiateGameObject) {
                callback(GameObject.Instantiate(rq.asset) as T);
            }
            else {
                callback(rq.asset as T);
            }
        }
    }
}
