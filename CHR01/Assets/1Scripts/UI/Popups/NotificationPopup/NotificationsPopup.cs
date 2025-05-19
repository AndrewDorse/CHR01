using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class NotificationsPopup : Popup
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _skipButton;
    [SerializeField] private GameObject _portr;
    [SerializeField] private GameObject _land;

    public void Setup(Action startCallback, Action accept, Action skip)
    {
        startCallback?.Invoke();

        _acceptButton.onClick.AddListener(()=>
        {
            
            accept?.Invoke();

            EventsProvider.OnAnyButtonClick?. Invoke();
            Close();
        });

        _skipButton.onClick.AddListener(() =>
        {
            EventsProvider.OnAnyButtonClick?.Invoke();
            skip?.Invoke();
        });

        EventsProvider.OnOrientationChanged += OrientationChanged; 
    } 

    private void OrientationChanged(DeviceOrientation deviceOrientation)
    {
        StartCoroutine(cas(deviceOrientation));
    }

    private IEnumerator cas(DeviceOrientation deviceOrientation)
    { 
        yield return new WaitForEndOfFrame(); 
        
        if (deviceOrientation == DeviceOrientation.Portrait)
        {
            _portr.SetActive(true);
            _land.SetActive(false);
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            _portr.SetActive(false);
            _land.SetActive(true);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            _portr.SetActive(false);
            _land.SetActive(true);
            Screen.orientation = ScreenOrientation.LandscapeRight;

        }
    }

    private void OnDestroy()
    {
        EventsProvider.OnOrientationChanged -= OrientationChanged;
    }
}
