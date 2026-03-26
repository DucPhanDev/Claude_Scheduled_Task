using UnityEngine;
using System;
using System.Collections.Generic;
public class PopupManager : PersistentSingleton<PopupManager>
{
    [Header("Popup Prefabs")]
    [SerializeField] private List<PopUpPrefabEntry> popupPrefabs = new List<PopUpPrefabEntry>();

    [SerializeField] private Transform popupContainer;

    private readonly Stack<PopupBase> activePopups = new Stack<PopupBase>();

    public PopupBase Show(PopUpType type, PopUpData data = null)
    {
        var prefab = GetPrefabForType(type);
        if (prefab == null)
        {
            Debug.LogError($"No prefab found for popup type: {type}");
            return null;
        }

        var instance = Instantiate(prefab, popupContainer);
        instance.transform.SetAsLastSibling();

        instance.name = $"{type}_Popup";

        instance.Show(data);
        activePopups.Push(instance);

        return instance;
    }

    public void CloseTop()
    {
        if (activePopups.Count == 0) return;

        var top = activePopups.Pop();
        top.Hide();
        Destroy(top.gameObject, 0.4f); // delay for hide animation if any
    }

    public void CloseAll()
    {
        while (activePopups.Count > 0)
        {
            var popup = activePopups.Pop();
            popup.Hide();
            Destroy(popup.gameObject, 0.4f);
        }
    }

    private PopupBase GetPrefabForType(PopUpType type)
    {
        foreach (var entry in popupPrefabs)
        {
            if (entry.type == type)
            {
                return entry.prefab;
            }
        }
        return null;
    }

    // Helper methods
    public void ShowConfirm(string message, System.Action onConfirm = null, System.Action onCancel = null)
    {
        Show(PopUpType.Popup_Confirm, new ConfirmPopUpData
        {
            Message = message,
            OnConfirm = onConfirm,
            OnCancel = onCancel
        });
    }

    public void ShowNotice(string message, float autoCloseAfter = 0f)
    {
        Show(PopUpType.PopUp_Notice, new NoticePopUpData
        {
            Message = message,
            AutoCloseAfterSeconds = autoCloseAfter
        });
    }
}
