using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class contains all the events relevent to this application.
/// Used for communication from different layers or object that 
/// are not directly related to each other.
/// </summary>
public static class EventsManager
{
    public static UnityEvent StartGameEvent;

    public static UnityEvent EndGameEvent;

    public static UnityEvent ShowGameMenuEvent;

    public static void InitializeAllEvents()
    {
        StartGameEvent = new UnityEvent();
        EndGameEvent = new UnityEvent();
        ShowGameMenuEvent = new UnityEvent();
    }
}
