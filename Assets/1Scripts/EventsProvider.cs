using System;
using UnityEngine;

public static class EventsProvider
{
    public static TimeEvents TimeEvents = new TimeEvents();

    public static Action OnLevelStart;
    public static Action OnLevelEnd;

    public static Action<float> OnLoadingSceneValueChanged;

    public static Action OnDataSync;

    public static Action OnSaveModelChanged;

    


    public static Action<bool> OnConfigRequestDone;

    public static Action<float> OnScoreChanged;
    public static Action<int> OnLivesChanged;

    public static Action<DeviceOrientation> OnOrientationChanged;

    public static Action OnAnyButtonClick;
}


public class TimeEvents
{
    public Action[] Actions;

    public TimeEvents()
    {
        Actions = new Action[6];
    }
}