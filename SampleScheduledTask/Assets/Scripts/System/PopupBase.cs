using UnityEngine;

public class PopupBase : MonoBehaviour
{
    public PopUpType PopupType;

    public virtual void Show(PopUpData data)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        // Destroy(gameObject, 0.4f); // optional - destroy after hide animation
    }

    protected void CloseThisPopup()
    {
        PopupManager.Instance.CloseTop();
    }
}
