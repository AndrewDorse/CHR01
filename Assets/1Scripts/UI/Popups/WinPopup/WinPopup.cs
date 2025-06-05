using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UniWebViewLogger;

public class WinPopup : Popup
{
    [SerializeField] private Button _goToMenuButton;
    [SerializeField] private Button _playAgainButton;

    [SerializeField] private TMPro.TMP_Text _scoreText;
    [SerializeField] private TMPro.TMP_Text _bestScoreText;
    [SerializeField] private GameObject[] _stars;

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


        int stars = 1;

        if (score > 250)
        {
            stars = 2;
        }
        else if (score > bestScore && score > 500)
        {
            stars = 3;
        }



        for (int i = 0; i < _stars.Length; i++)
        {
            if (i < stars)
            {
                _stars[i].SetActive(true);
            }
            else
            {
                _stars[i].SetActive(false);
            }
        }
    }
}
