using UnityEngine;

public interface IEvent
{

}

public class OnDirectionSelectedEvent : IEvent
{
    public Vector2 direction;  // vector hướng đã chọn (normalized)
}


public class GameManagerReadyEvent : IEvent
{

}

public class DrinkFinishedCheckEvent : IEvent
{

}

public class DrinkSpawnedEvent : IEvent
{
    public int currentDrinkId;
    public int nextDrinkId;
}

public class DrinkDeliveredEvent : IEvent
{
    public int money;
}
public class TimeUpdatedEvent : IEvent
{
    public float remainingTime;
}
public class DrinkStoppedEvent : IEvent
{
    public Vector3 stopPosition;
    public DrinkController drink;  // để lấy config.id, bounceCount...
}