using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;

    public static bool IsInstantiated
    {
        get
        {
            return _instance != null;
        }
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T instance = GameObject.FindObjectOfType<T>() as T;

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }
                else
                {
                    create_instance(instance);
                }

                Debug.Assert(_instance != null, "failed create instnace of " + typeof(T).ToString());
            }
            return _instance;
        }
    }

    private static void create_instance(Object instance)
    {
        _instance = instance as T;
        _instance.Init();
    }

    protected virtual void Awake()
    {
        create_instance(this);

        T[] instances = FindObjectsOfType<T>();

        if(instances.Length >= 2)
        {
            for(int i = 0; i < instances.Length - 1; i++)
            {
                Destroy(instances[i].gameObject);
            }
        }
    }

    protected virtual void Init()
    {
        DontDestroyOnLoad(_instance);
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }
}

