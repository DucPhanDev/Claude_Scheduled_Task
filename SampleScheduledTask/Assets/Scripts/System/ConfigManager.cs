using UnityEngine;
using System;
using System.Collections.Generic;

public class ConfigManager : PersistentSingleton<ConfigManager>
{
    public List<LevelConfig> Levels { get; private set; } = new List<LevelConfig>();
    public List<DrinkConfig> Drinks { get; private set; } = new List<DrinkConfig>();
    protected override void Awake()
    {
        base.Awake();

        LoadLevelConfig();
        LoadDrinkConfig();
    }

    public int Get_FirstLevel()
    {
        if (!PlayerDataManager.Instance.PlayerData.Is_FisnishTutorial)
        {
            return 0;
        }
        return 1;
    }

    public int Get_LastMission()
    {
        if (Levels == null || Levels.Count == 0)
        {
            Debug.LogWarning("Levels list is empty. Cannot determine last mission.");
            return 0;
        }
        int maxLevel = int.MinValue;
        foreach (var level in Levels)
        {
            if (level.level > maxLevel)
            {
                maxLevel = level.level;
            }
        }
        return maxLevel;
    }
    


    private void LoadLevelConfig()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("config/ConfigLevel");
        if (jsonFile == null)
        {
            Debug.LogError("Không tìm thấy Resources/config/ConfigLevel.json");
            return;
        }

        LevelListWrapper wrapper = JsonUtility.FromJson<LevelListWrapper>(jsonFile.text);
        if (wrapper != null && wrapper.levels != null)
        {
            Levels = wrapper.levels;
            Debug.Log($"Đã load {Levels.Count} levels từ ConfigLevel.json");
        }
        else
        {
            Debug.LogError("JSON ConfigLevel không hợp lệ");
        }
    }

    private void LoadDrinkConfig()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("config/ConfigDrink");
        if (jsonFile == null)
        {
            Debug.LogError("Không tìm thấy Resources/config/ConfigDrink.json");
            return;
        }

        DrinksWrapper wrapper = JsonUtility.FromJson<DrinksWrapper>(jsonFile.text);
        if (wrapper != null && wrapper.drinks != null)
        {
            Drinks = wrapper.drinks;
            Debug.Log($"Đã load {Drinks.Count} drinks từ ConfigDrink.json");
        }
        else
        {
            Debug.LogError("JSON ConfigDrink không hợp lệ");
        }
    }

    // Helper để lấy config nhanh (nếu cần dùng ở nơi khác)
    public LevelConfig GetLevel(int levelId)
    {
        return Levels.Find(l => l.level == levelId);
    }

    public DrinkConfig GetDrink(int drinkId)
    {
        return Drinks.Find(d => d.id == drinkId);
    }
}
