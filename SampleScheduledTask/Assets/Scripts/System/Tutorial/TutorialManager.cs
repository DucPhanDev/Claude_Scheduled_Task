using UnityEngine;

public class TutorialManager : PersistentSingleton<TutorialManager>
{
    public int Current_TutorialId;
    public TutorialStepBase[] Arr_TutorialSteps;
    public GameObject Tutorial_Panel;
    protected override void Awake()
    {
        base.Awake();
        Arr_TutorialSteps = GetComponentsInChildren<TutorialStepBase>(true);
        Tutorial_Panel.SetActive(false);
        foreach (TutorialStepBase e in Arr_TutorialSteps)
            e.HideStep();
    }
    public TutorialStepBase Get_CurrentTutorialBase()
    {
        return Arr_TutorialSteps[Current_TutorialId];
    }
    public bool CheckTutorial()
    {
        if (!PlayerDataManager.Instance.PlayerData.Is_FisnishTutorial)
        {
            StartTutorial();
            return true;
        }
        else
        {
            Tutorial_Panel.SetActive(false);
            return false;
        }
    }

    public bool IsFinishTutorial()
    {
        return PlayerDataManager.Instance.PlayerData.Is_FisnishTutorial || PlayerDataManager.Instance.PlayerData.Current_Level_ID > 1;
    }

    private void StartTutorial()
    {

        Tutorial_Panel.SetActive(true);
        Arr_TutorialSteps[0].ShowStep();
        Debug.Log("---> Start Tutorial");
    }

    public void ShowNext_Tutorial()
    {
        Arr_TutorialSteps[Current_TutorialId].DoneStep();
        Current_TutorialId++;
        if (Current_TutorialId >= Arr_TutorialSteps.Length)
        {
            Tutorial_Panel.SetActive(false);
            Debug.Log("---> Finish Tutorial");
            PlayerDataManager.Instance.PlayerData.Is_FisnishTutorial = true;
            PlayerDataManager.Instance.SavePlayerData();
            return;
        }
        Arr_TutorialSteps[Current_TutorialId].ShowStep();
    }
}
