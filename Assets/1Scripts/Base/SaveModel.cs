
using NUnit.Framework;
using System;
using System.Collections.Generic;

[System.Serializable]
public class SaveModel
{
    public int Mode = 0;
    public bool FirstLaunch;


    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            EventsProvider.OnSaveModelChanged?.Invoke();
        }
    }


    public bool LastLaunchWebview = false;

    public int Level = 1; 
    public DateTime LastAskForNotifications = DateTime.MinValue;
    public int NotificationPermissionAsked = 0;

    public int _gold;

    public bool Vibro = false; 
    public bool Sound = false;

    public List<BestResultSlot> BestResults;
    public int CharacterNumber = 1;

    public SaveModel()
    {
        LastAskForNotifications = DateTime.Now.AddDays(-5);

        Level = 1;

        LastLaunchWebview = false;
        _gold = 1000;
        FirstLaunch = true;
        BestResults = new List<BestResultSlot>();
    }
}