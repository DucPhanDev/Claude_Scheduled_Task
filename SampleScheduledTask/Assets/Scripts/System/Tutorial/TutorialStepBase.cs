using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStepBase : MonoBehaviour
{

    [SerializeField] protected GameObject content;
    [SerializeField] protected float timeWait_ShowTut = 0f;
    [SerializeField] protected float timeFactor_ShowTut = 1f;
    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    public virtual void ShowStep()
    {
    }
    public virtual void DoneStep()
    {
    }
    public virtual void HideStep()
    {
        content.gameObject.SetActive(false);
    }
    public virtual void OnInteract()
    {
    }

    protected virtual IEnumerator IE_DelayShow()
    {
        yield return null;
    }
}
