using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;

public class NoInternetPopup : Popup
{
    [SerializeField] private GameObject _portr;
    [SerializeField] private GameObject _land;

     
    public void Setup(Action startCallback)
    {
        startCallback?.Invoke(); 

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
