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
    public static UnityEvent StartGameEvent = new UnityEvent();

    public static UnityEvent EndGameEvent = new UnityEvent();

    public static UnityEvent ShowGameMenuEvent = new UnityEvent();



    public static UnityEvent MoveRightEvent = new UnityEvent();
    public static UnityEvent MoveLeftEvent = new UnityEvent();
}
