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

        //_view.dropBallButton.onClick.AddListener(
        //    () =>
        //    {
        //        LevelController.Instance.SendBall();
        //        EventsProvider.OnAnyButtonClick?.Invoke();
        //    });


        _view.backToMenuButton.onClick.AddListener(Pause);

          

        EventsProvider.OnScoreChanged += SetScoreText;
        EventsProvider.OnLivesChanged += SetLives;
        SetScoreText(0);
        SetLives(LevelController.Instance.Lives);

        SetCoinsText();
    }

    private void SetScoreText(float value)
    {
        _view.scoreAmount.text = value.ToString();
    }

    private void SetLives(int obj)
    {
        for (int i = 0; i < _view.lifes.Length; i++)
        {
            if (i < obj)
            {
                _view.lifes[i].SetActive(true);
            }
            else
            {
                _view.lifes[i].SetActive(false);
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;

        SettingsPopup popup = Master.UIController.GetPopup<SettingsPopup>();
        popup.Setup(null);
    }

     

    private void SetValueText(int value)
    {
        //_view.ballValueText.text = value.ToString();
    }

    private void SetCoinsText()
    {
       // _view.moneyAmount.text = SaveManager.SaveModel.Gold.ToString();
    }

    public override void Dispose()
    {
        base.Dispose();
        EventsProvider.OnScoreChanged -= SetScoreText;
        EventsProvider.OnLivesChanged -= SetLives;
    }
}
