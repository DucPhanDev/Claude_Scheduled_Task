using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
/// 
// 1. Singleton giữ nguyên khi load scene (DontDestroyOnLoad)
public abstract class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting) return null;

            if (_instance == null || !_instance)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name + " (Persistent)");
                    _instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    private void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        _applicationIsQuitting = false;
    }
#endif
}