using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Hellmade.Sound;

public class SettingsPopup : Popup
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _vibroButton;

    [SerializeField] private Button _goToMenuButton;

    [SerializeField] private Sprite[] _toggleSprites;


    public void Setup(Action startCallback)
    {
        startCallback?.Invoke();

        _soundButton.onClick.AddListener(()=>
        {
            EventsProvider.OnAnyButtonClick?.Invoke();
            ChangeSound();
        });

        _vibroButton.onClick.AddListener(() =>
        {
            EventsProvider.OnAnyButtonClick?.Invoke();
            ChangeVibro();
        });


        if (Master.GameStageController.currentStage == Enums.GameStage.Playmode)
        {
            _goToMenuButton.gameObject.SetActive(true);

            _goToMenuButton.onClick.AddListener(() =>
            {
                EventsProvider.OnAnyButtonClick?.Invoke();
                Close();
                Master.GameStageController.ChangeStage(Enums.GameStage.Menu);
            });
        }
        else
        {
            _goToMenuButton.gameObject.SetActive(false);
        }


        UpdateButtonsView();
    }

    private void ChangeSound()
    {
        SaveManager.SaveModel.Sound = !SaveManager.SaveModel.Sound;

        if (SaveManager.SaveModel.Sound == false)
        {
            //EazySoundManager.GlobalVolume = 0f;
        }
        else
        {
         //   EazySoundManager.GlobalVolume = 0.8f;
        }

        UpdateButtonsView();
    }

    private void ChangeVibro()
    {
        SaveManager.SaveModel.Vibro = !SaveManager.SaveModel.Vibro;

        UpdateButtonsView();
    }

    private void UpdateButtonsView()
    {
        if (SaveManager.SaveModel.Sound)
        {
            _soundButton.image.sprite = _toggleSprites[0];
        }
        else
        {
            _soundButton.image.sprite = _toggleSprites[1];
        }

        if (SaveManager.SaveModel.Vibro)
        {
            _vibroButton.image.sprite = _toggleSprites[0];
        }
        else
        {
            _vibroButton.image.sprite = _toggleSprites[1];
        }
    }
}
