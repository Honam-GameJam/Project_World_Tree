using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}