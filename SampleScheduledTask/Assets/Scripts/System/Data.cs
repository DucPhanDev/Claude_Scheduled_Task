using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ViewData
{
}

[Serializable]
public class PopUpData
{
}

[Serializable]
public class PopUpData_Info : PopUpData
{
    public string Title;
    public string Message;
    public string ImgPath;
}

[Serializable]
public class PlayerData
{
    public string Name;
    public int Current_Level_ID;
    public int Max_Record;
    public bool Is_FisnishTutorial;
    public List<int> List_Show_MesgLevel = new List<int>();
}

//---------------------IMPLEMENTATIONS---------------------//
[Serializable]
public class PopUpPrefabEntry
{
    public PopUpType type;
    public PopupBase prefab;
}
[Serializable]
public class ResultViewData : ViewData
{
    public bool isWin;
    public int finalMoney;
    public int goalMoney;  // Optional, để hiển thị progress
}
public class ConfirmPopUpData : PopUpData
{
    public string Title = "Confirm";
    public string Message;
    public string YesText = "Yes";
    public string NoText = "No";
    public Action OnConfirm;
    public Action OnCancel;
}

public class NoticePopUpData : PopUpData
{
    public string Message;
    public float AutoCloseAfterSeconds = 0f; // 0 = no auto close
}
[Serializable]
public class LevelConfig
{
    public int level;
    public int customer_seat_count;
    public int goal_money;
    public float time_limit;
    public List<TargetData> targets;
    public int show_msg;
    public string msg_title;
    public string msg_img;
    public string msg_content;
}

[Serializable]
public class TargetData
{
    public int id;
    public int reward;
    public float max_distance;  // 
    public List<int> available_drinks;
    public int fixed_seat = -1;           // -1 = random ghế
    public int fixed_random_drink_pos = -1; // -1 = random vị trí trong 6
}

[Serializable]
public class DrinkConfig
{
    public int id;
    public string name;
    public string description;

    public float mass = 1f;
    public float linearDrag = 0.5f;
    public float angularDrag = 0.05f;
    public float bounciness = 0.3f;

    public bool mustRicochet = false;
    public int requiredBounces = 0;
    public bool breakOnWall = false;
    public bool hasTimer = false;
    public float timerSeconds = 0f;
}
[Serializable]
public class LevelListWrapper
{
    public List<LevelConfig> levels;
}
[Serializable]
public class DrinksWrapper
{
    public List<DrinkConfig> drinks;
}



