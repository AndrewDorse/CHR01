using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Newtonsoft.Json;
using System.Net;

public static class SaveManager
{
    public static SaveModel SaveModel => _saveModel;

    private static SaveModel _saveModel;


    public static void Save()
    { 
        PlayerPrefs.SetString("save", JsonConvert.SerializeObject(_saveModel)); 
    } 

    public static void Load()
    {
        string data = PlayerPrefs.GetString("save");
         
        SaveModel model =  JsonConvert.DeserializeObject<SaveModel>(data);

        if (model == null)
        {
            Debug.Log("### NEW SAVE");

            model = new SaveModel();
            _saveModel = model; 
        }

        _saveModel = model;
        Debug.Log("### LastAskForNotifications  = " + SaveManager.SaveModel.LastAskForNotifications);
        Debug.Log("### save  = " + data);

        
        Save();
        EventsProvider.OnSaveModelChanged += Save;
    }

    public static void SendResult(int level, float score)
    { 
        if(SaveModel.BestResults == null)
        {
            SaveModel.BestResults = new List<BestResultSlot>();
        }

        for(int i = 0;  SaveModel.BestResults.Count > i; i++)
        {
            if (SaveModel.BestResults[i].Level == level)
            {
                if (score > SaveModel.BestResults[i].Result)
                {
                    SaveModel.BestResults[i].Result = Mathf.RoundToInt(score);
                    return;
                }
                else
                {
                    return;
                }
            } 
        }

        SaveModel.BestResults.Add(new BestResultSlot(level, Mathf.RoundToInt(score)));

        Save();
    }

    public static int GetResult(int level, float score)
    {
        if (SaveModel.BestResults == null)
        {
            SaveModel.BestResults = new List<BestResultSlot>();
        }

        for (int i = 0; SaveModel.BestResults.Count > i; i++)
        {
            if (SaveModel.BestResults[i].Level == level)
            {
                if (score < SaveModel.BestResults[i].Result)
                { 
                    return SaveModel.BestResults[i].Result;
                }
                else
                {
                    return Mathf.RoundToInt(score);
                }
            }
        }

        return Mathf.RoundToInt(score);
    }
}  





