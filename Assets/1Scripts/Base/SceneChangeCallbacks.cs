using System;

public static class SceneChangeCallbacks
{
    private static Enums.GameStage _previousStage;

    public static (Action, bool) GetCallback(Enums.GameStage currentStage, Enums.GameStage nextStage)
    {
        Action callback = null;
        bool needToChangeScene = false;

        _previousStage = currentStage;

        if (currentStage == Enums.GameStage.Boot)
        {
            if (nextStage == Enums.GameStage.Menu)
            {
                callback = MenuCallBack;
                needToChangeScene = false;
            }
        }
        else if (currentStage == Enums.GameStage.Menu)
        {
            if (nextStage == Enums.GameStage.Playmode)
            {
                callback = PlaymodeCallBack;
                needToChangeScene = false;
            }
        }
        else if (currentStage == Enums.GameStage.Playmode)
        {
            if (nextStage == Enums.GameStage.Menu)
            {
                callback = MenuCallBack;
                needToChangeScene = false;
            }
        }

        return (callback, needToChangeScene);
    }


    private static void MenuCallBack()
    {
        Master.UIController.OpenScreen(Enums.GameStage.Menu);
    }

    private static void PlaymodeCallBack()
    {
        Master.UIController.OpenScreen(Enums.GameStage.Playmode);
    }
}
