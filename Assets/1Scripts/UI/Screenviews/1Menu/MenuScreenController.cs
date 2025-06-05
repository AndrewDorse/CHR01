using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenController : ScreenController
{
    private MenuScreenView _view;
     


    public MenuScreenController(MenuScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();


        _view.startBattleButton.onClick.AddListener(StartBattle);
        _view.settingsButton.onClick.AddListener(OpenSettings);
        _view.privacyPolicyButton.onClick.AddListener(OpenPolicy);
        _view.skinsButton.onClick.AddListener(OpenSkins);

        _view.coinsAmount.text = SaveManager.SaveModel.Gold.ToString();
        _view.levelText.text = "Level " + SaveManager.SaveModel.Level;
    }

    private void StartBattle()
    {
        EventsProvider.OnAnyButtonClick?.Invoke();
        LevelController.Instance.StartLevel();

        Master.GameStageController.ChangeStage(Enums.GameStage.Playmode);
    } 

    private void OpenSettings()
    {
        EventsProvider.OnAnyButtonClick?.Invoke();
        SettingsPopup popup = Master.UIController.GetPopup<SettingsPopup>();
        popup.Setup(null);
    }

    private void OpenPolicy()
    {
        EventsProvider.OnAnyButtonClick?.Invoke();
        PrivacyPolicyPopup popup = Master.UIController.GetPopup<PrivacyPolicyPopup>();
        popup.Setup(null);
    }

    private void OpenSkins()
    {
        EventsProvider.OnAnyButtonClick?.Invoke();
        SkinsPopup popup = Master.UIController.GetPopup<SkinsPopup>();
        popup.Setup(null);
    }
}
