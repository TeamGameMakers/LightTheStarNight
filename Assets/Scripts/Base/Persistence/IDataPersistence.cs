using UnityEngine;

namespace Base.Persistence
{
    /// <summary>
    /// 数据持久化接口。
    /// </summary>
    public interface IDataPersistence
    {
        /// <summary>
        /// 写入数据。
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="path">存储路径</param>
        public void Write(object data, string path);

        /// <summary>
        /// 读取数据。
        /// </summary>
        /// <typeparam name="T">读取类型</typeparam>
        /// <param name="path">读取路径</param>
        /// <returns></returns>
        public T Read<T>(string path) where T : new();
    }
}
