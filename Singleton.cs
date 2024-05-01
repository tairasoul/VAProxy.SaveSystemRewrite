using UnityEngine;

namespace JsonRewrite
{
    public class Singleton<T> where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new(typeof(T).Name);
                        instance = singletonObject.AddComponent<T>();
                        GameObject.DontDestroyOnLoad(singletonObject);
                    }
                }
                return instance;
            }
        }
    }
}