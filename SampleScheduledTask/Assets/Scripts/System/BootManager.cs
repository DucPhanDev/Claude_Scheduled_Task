using UnityEngine;

public class BootManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ViewManager.Instance.HideAllView();
    }
    void Start()
    {
        LoadSceneManager.Instance.LoadScene(Utils.MENU_SCENE, () => ViewManager.Instance.ShowView(ViewType.View_MainMenu));
    }

}
