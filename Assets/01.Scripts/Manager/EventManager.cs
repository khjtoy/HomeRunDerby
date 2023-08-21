using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    private static Dictionary<string, Action<EventParam>> eventDictionary = new Dictionary<string, Action<EventParam>>();

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(eventParam);
        }
    }
}
public struct EventParam
{
    public Vector3 vectorParam;
    public bool boolParam;
    public int intParam;
}

