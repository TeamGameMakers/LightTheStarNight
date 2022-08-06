using UnityEngine;

namespace Base
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;
        public static T Instance {
            get {
                if (m_instance == null) {
                    GameObject go = new GameObject(typeof(T).Name);
                    m_instance = go.AddComponent<T>();
                }
                return m_instance;
            }
        }

        public bool dontDestroy = false;

        protected virtual void Awake()
        {
            if (m_instance == null) {
                m_instance = this as T;
            }
            else {
                Destroy(gameObject);
            }
        }

        protected virtual void Start()
        {
            if (dontDestroy) {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            m_instance = null;
        }
    }
}
