using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBase : MonoBehaviour
{
    public ViewType ViewType;
    public bool DoneInit => doneInit;

    protected ViewData viewData;
    protected bool doneInit;


    protected virtual void Awake()
    {
    }
    
    protected virtual void Start()
    {
    }

    public virtual void ShowView(ViewData data)
    {
        viewData = data;
        this.gameObject.SetActive(true);
    }

    public virtual void HideView()
    {
        this.gameObject.SetActive(false);
    }



}
