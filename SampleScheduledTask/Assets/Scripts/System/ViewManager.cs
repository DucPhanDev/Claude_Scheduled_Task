using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : PersistentSingleton<ViewManager>
{
    [SerializeField] private ViewBase[] views;

    //public void ShowView(ViewType viewType,ViewData viewData = null)
    //{
    //    foreach (var view in views)
    //    {
    //        if(view.ViewType == viewType) 
    //        {
    //            view.ShowView(viewData);
    //        }
    //        else
    //        {
    //            view.HideView();
    //        }
    //    }
    //}

    public void HideView(ViewType viewType,ViewData viewData = null)
    {
        foreach(var view in views)
        {
            if(view.ViewType == viewType) 
            {
                view.HideView();
            }
        }
    }

    public void HideAllView()
    {
        foreach(var view in views)
        {
            view.HideView();
        }
    }

    public ViewBase ShowView(ViewType viewType,ViewData viewData = null)
    {
        ViewBase result = null;
        foreach (var view in views)
        {
            if(view.ViewType == viewType) 
            {                
                result = view;
            }
            else
            {
                view.HideView();
            }
        }
        result.ShowView(viewData);
        return result;
    }

    public void ShowPopUp()
    {
    }

    public ViewBase Get_View(ViewType viewType)
    {
        foreach (var e in views)
        {
            if (e.ViewType == viewType)
                return e;
        }
        return null;
    }

}
