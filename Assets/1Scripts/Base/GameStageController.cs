using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageController
{
    public Enums.GameStage currentStage = Enums.GameStage.Boot;

    private SceneController _sceneController = new SceneController();


    public void ChangeStage(Enums.GameStage stage)
    {
        (Action, bool) info = SceneChangeCallbacks.GetCallback(currentStage, stage);

        Action callback = info.Item1;
        bool needToChangeScene = info.Item2;

        currentStage = stage;

        if (needToChangeScene)
        {
            int sceneNumber = GetSceneNumberByStage(stage);

            _sceneController.LoadScene(sceneNumber, callback);

        }
        else
        {
            callback?.Invoke();
        }
    }

    public void ChangeStage(Enums.GameStage stage, Action callback)
    {
        (Action, bool) info = SceneChangeCallbacks.GetCallback(currentStage, stage);
         
        bool needToChangeScene = info.Item2;

        currentStage = stage;

        if (needToChangeScene)
        {
            int sceneNumber = GetSceneNumberByStage(stage);

            _sceneController.LoadScene(sceneNumber, callback);

        }
        else
        {
            callback?.Invoke();
        }
    }

    private int GetSceneNumberByStage(Enums.GameStage stage)
    {
        int result = 0;

        if (stage == Enums.GameStage.Menu)
        {
            result = 0;
        }
        else if (stage == Enums.GameStage.Playmode)
        {
            result = 0;
        }

        return result;
    }
}