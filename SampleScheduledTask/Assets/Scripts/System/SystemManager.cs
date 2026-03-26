using System.Collections.Generic;
using UnityEngine;

public class SystemManager : PersistentSingleton<SystemManager>
{
    public Dictionary<string, GameObject> Dic_CachedObj = new Dictionary<string, GameObject>();
    
    void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        if (Dic_CachedObj.ContainsKey(name))
        {
            if (name.Equals("EventSystem"))
            {
                Object.DestroyImmediate(gameObject);
            }
            else
            {
                Dic_CachedObj[name] = this.gameObject;
                Object.DestroyImmediate(this.gameObject);
            }
        }
        else
            Dic_CachedObj[name] = gameObject;
    }     
}
