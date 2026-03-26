using UnityEngine;
public enum ViewType
{
    View_MainMenu = 1,
    View_InGame = 2,
    View_Result = 3,
}

public enum PopUpType
{
    PopUp_Pause = 1,
    PopUp_Notice = 2,
    Popup_Confirm = 3,
    Popup_Info = 4,
}

public static class Utils
{
    public static readonly string BOOT_SCENE = "Boot_Scene";
    public static readonly string MENU_SCENE = "Menu_Scene";
    public static readonly string GAMEPLAY_SCENE = "GamePlay_Scene";


    public const string PlayerDataKey = "PlayerData";

    public const float DEFAULT_FORCE_MAGNITUDE = 10f;
    public const string PATHLOAD_DRINK_PREFAB = "Prefabs/DrinkPrefab_Id_";
    public const string PATHLOAD_DRINK_PREFAB_2D = "Prefabs/DrinkPrefab_2D/DrinkPrefab_2D_Id_";
    public const string PATHLOAD_TARGET_PREFAB_2D = "Prefabs/TargetPrefab_2D/TargetPrefab_2D";
    public const string PATHLOAD_TARGET_PREFAB = "Prefabs/TargetPrefab";

}
