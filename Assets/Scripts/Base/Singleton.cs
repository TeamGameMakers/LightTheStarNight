namespace Base
{
    public class Singleton<T> where T : class, new()
    {
        private static T m_instance;
        public static T Instance => m_instance ??= new T();
    }
}
