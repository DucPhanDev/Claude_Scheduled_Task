using UnityEngine;

public class TimeManager : PersistentSingleton<TimeManager>
{
    private float timeScaleFactor = 1f;
    private float defaultTimeScale = 1f;

    public void SetTimeScale(float newTimeScaleFactor)
    {
        timeScaleFactor = newTimeScaleFactor;
        Time.timeScale = defaultTimeScale * timeScaleFactor;
    }

    public void SetDefaultTimeScale()
    {
        timeScaleFactor = defaultTimeScale;
        Time.timeScale = defaultTimeScale * timeScaleFactor;
    }
}
