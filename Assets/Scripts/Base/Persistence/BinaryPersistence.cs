using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Base.Persistence
{
    /// <summary>
    /// 二进制数据管理器。
    /// 提供二进制序列化和反序列化。
    /// 没有加密处理。
    /// </summary>
    public class BinaryPersistence : Singleton<BinaryPersistence>, IDataPersistence
    {
        private static string suffix = "";
        public static string Suffix => suffix;

        /// <summary>
        /// 存储为二进制文件。
        /// 不判断路径存在。
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="path">存储路径</param>
        public void Write(object data, string path)
        {
            path += Suffix;

            // 序列化
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
                fs.Flush();
                fs.Close();
            }
            Debug.Log("文件储存至: " + path);
        }

        /// <summary>
        /// 读取二进制文件生成对象。
        /// </summary>
        /// <typeparam name="T">要实现无参构造函数</typeparam>
        /// <param name="path">读取路径</param>
        /// <returns>文件不存在则返回默认对象</returns>
        public T Read<T>(string path) where T : new()
        {
            path += Suffix;

            // 判断文件是否存在
            if (!File.Exists(path)) {
                Debug.Log("未在该路径找到文件: " + path);
                // 返回默认对象
                return default(T);
            }

            // 反序列化
            T res;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                BinaryFormatter bf = new BinaryFormatter();
                res = (T)bf.Deserialize(fs);
                fs.Close();
            }

            Debug.Log("文件读取完成");
            return res;
        }
    }
}
