using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneManager : PersistentSingleton<LoadSceneManager>
{
    public void LoadScene(string sceneName, Action callBack = null)
    {
        SceneManager.LoadScene(sceneName);
        callBack?.Invoke();
    }
}
