using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeScreenController : ScreenController
{
    private PlayModeScreenView _view;
     


    public PlayModeScreenController(PlayModeScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        _view.dropBallButton.onClick.AddListener(
            () =>
            {
                LevelController.Instance.SendBall();
                EventsProvider.OnAnyButtonClick?.Invoke();
            });


        _view.backToMenuButton.onClick.AddListener(Pause);

        _view.increaseBallsButton.onClick.AddListener(
            () =>
            {
                EventsProvider.OnAnyButtonClick?.Invoke();
                LevelController.Instance.IncreaseAmount();
            });

        _view.increaseValueButton.onClick.AddListener(
            () =>
        {
            EventsProvider.OnAnyButtonClick?.Invoke();
            LevelController.Instance.IncreaseValue();
        }
         );


        _view.ballToWinText.text = LevelController.Instance.ValueToWin.ToString();


        EventsProvider.OnAmountChanged += SetAmountText;
        EventsProvider.OnValueChanged += SetValueText;
        EventsProvider.OnSaveModelChanged += SetCoinsText;

        SetCoinsText();
    }


    private void Pause()
    {
        SettingsPopup popup = Master.UIController.GetPopup<SettingsPopup>();
        popup.Setup(null);
    }

    private void SetAmountText(int amount)
    {
        _view.ballAmountText.text = amount.ToString();
    }

    private void SetValueText(int value)
    {
        _view.ballValueText.text = value.ToString();
    }

    private void SetCoinsText()
    {
        _view.moneyAmount.text = SaveManager.SaveModel.Gold.ToString();
    }
}
