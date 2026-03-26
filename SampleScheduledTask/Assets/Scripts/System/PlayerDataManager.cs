using UnityEngine;

public class PlayerDataManager : PersistentSingleton<PlayerDataManager>
{
    public PlayerData PlayerData;
    private void Start()
    {
        PlayerData = LoadPlayerData();
    }
    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString(Utils.PlayerDataKey, json);
        PlayerPrefs.Save();
    }
    public PlayerData LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(Utils.PlayerDataKey))
        {
            string json = PlayerPrefs.GetString(Utils.PlayerDataKey);
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
            if (!PlayerData.Is_FisnishTutorial)
                PlayerData.Current_Level_ID = ConfigManager.Instance.Get_FirstLevel();
        }
        else
        {
            PlayerData = new PlayerData { Current_Level_ID = ConfigManager.Instance.Get_FirstLevel() };
            string json = JsonUtility.ToJson(PlayerData);
            PlayerPrefs.SetString(Utils.PlayerDataKey, json);
            PlayerPrefs.Save();
        }
        return PlayerData;
    }

    public void Add_MesgLevel_Showed(int levelID)
    {
        if (!PlayerData.List_Show_MesgLevel.Contains(levelID))
        {
            PlayerData.List_Show_MesgLevel.Add(levelID);
            SavePlayerData();
        }
    }

    public bool Has_MesgLevel_Showed(int levelID)
    {
        return PlayerData.List_Show_MesgLevel.Contains(levelID);
    }

    public static void ClearPlayerData()
    {
        PlayerPrefs.DeleteKey(Utils.PlayerDataKey);
    }
}
