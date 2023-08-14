using UnityEngine.Events;

/// <summary>
/// This class contains all the events relevent to this application.
/// Used for communication from different layers or object that 
/// are not directly related to each other.
/// </summary>
public static class EventsManager
{
    #region  Game Events
    public static UnityEvent StartGameEvent = new UnityEvent();
    public static UnityEvent EndGameEvent = new UnityEvent();
    public static UnityEvent ShowGameMenuEvent = new UnityEvent();
    public static UnityEvent FinishLevelEvent = new UnityEvent();
    public static UnityEvent FinishGameEvent = new UnityEvent();
    public static UnityEvent RestartAllLevels = new UnityEvent();
    #endregion

    #region Play Events
    public static UnityEvent MoveRightEvent = new UnityEvent();
    public static UnityEvent MoveLeftEvent = new UnityEvent();
    public static UnityEvent MoveIdleEvent = new UnityEvent();
    public static UnityEvent FireEvent = new UnityEvent();
    public static UnityEvent ProjectileSentEvent = new UnityEvent();
    public static UnityEvent ProjectileDestroyedEvent = new UnityEvent();
    public static UnityEvent<BallView> SplitEvent = new UnityEvent<BallView>();
    public static UnityEvent<BallView> BallHitRoofEvent = new UnityEvent<BallView>();
    public static UnityEvent StopMovingEvent = new UnityEvent();
    #endregion
}
