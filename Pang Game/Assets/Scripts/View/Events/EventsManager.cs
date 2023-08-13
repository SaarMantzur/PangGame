using UnityEngine.Events;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This class contains all the events relevent to this application.
/// Used for communication from different layers or object that 
/// are not directly related to each other.
/// </summary>
public static class EventsManager
{

    public static UnityEvent EndGameEvent = new UnityEvent();

    public static UnityEvent ShowGameMenuEvent = new UnityEvent();

    public static UnityEvent FinishLevelEvent = new UnityEvent();


    public static UnityEvent MoveRightEvent = new UnityEvent();
    public static UnityEvent MoveLeftEvent = new UnityEvent();
    public static UnityEvent MoveIdleEvent = new UnityEvent();
    public static UnityEvent FireEvent = new UnityEvent();

    public static UnityEvent ProjectileDestroyedEvent = new UnityEvent();

    public static UnityEvent<BallView> SplitEvent = new UnityEvent<BallView>();

    public static UnityEvent StartGameOnDefaultLevelEvent = new UnityEvent();
    public static UnityEvent<int> StartGameEvent = new UnityEvent<int>();
    public static UnityEvent<DataStructures.LevelInstructions> StartNewLevelEvent = new UnityEvent<DataStructures.LevelInstructions>();

}
