using System.Collections.Generic;
using Base;
using UnityEngine;

namespace Save
{
    public static class SaveManager
    {
        #region 存储数据

        private static Dictionary<string, bool> m_boolDic = new Dictionary<string, bool>();
        private static Dictionary<string, int> m_intDic = new Dictionary<string, int>();

        public static void Register(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Debug.LogWarning("不可为空键");
                return;
            }

            m_boolDic[key] = true;
        }

        public static void Register(string key, int value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Debug.LogWarning("不可为空键");
                return;
            }

            m_intDic[key] = value;
        }

        #endregion

        #region 获取数据

        public static bool GetBool(string key)
        {
            if (m_boolDic.ContainsKey(key))
                return true;
            return false;
        }

        public static int GetInt(string key, int def = 0)
        {
            if (m_intDic.ContainsKey(key))
                return m_intDic[key];
            return def;
        }

        #endregion
    }
}
