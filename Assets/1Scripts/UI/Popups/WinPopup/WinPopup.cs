using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinPopup : Popup
{
    [SerializeField] private Button _goToMenuButton;
    [SerializeField] private Button _playAgainButton;

    [SerializeField] private TMPro.TMP_Text _scoreText;
    [SerializeField] private TMPro.TMP_Text _bestScoreText;


    public void Setup(Action startCallback, Action toMenu, Action playAgain, float score, int bestScore)
    {
        startCallback?.Invoke();

        _goToMenuButton.onClick.AddListener(()=>
        {

            EventsProvider.OnAnyButtonClick?.Invoke();
            Close();
            toMenu?.Invoke();
        });

        _playAgainButton.onClick.AddListener(() =>
        {
            EventsProvider.OnAnyButtonClick?.Invoke();
            Close();
            playAgain?.Invoke();
        });

        _scoreText.text = Mathf.RoundToInt(score).ToString();
        _bestScoreText.text = Mathf.RoundToInt(bestScore).ToString();


    }
}
