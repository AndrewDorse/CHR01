using DG.Tweening;
using System.Collections; 
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private GameObject _portrait;
    [SerializeField] private GameObject _landscape;

    public void Open()
    {
        //DeviceOrientationChange.OnOrientationChange += OrientationChanged;

        //OrientationChanged(Input.deviceOrientation);

        gameObject.SetActive(true);

        StopAllCoroutines();

        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        EventsProvider.OnOrientationChanged += OrientationChanged;
    }


    public void Close()
    {
        DeviceOrientationChange.OnOrientationChange -= OrientationChanged;

        StopAllCoroutines();

        if (!gameObject.activeInHierarchy) return;

        _canvasGroup.alpha = 1;

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            //_canvasGroup.DOFade(0, 0.01f).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
        }
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
            _portrait.SetActive(true);
            _landscape.SetActive(false);
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            _portrait.SetActive(false);
            _landscape.SetActive(true);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            _portrait.SetActive(false);
            _landscape.SetActive(true);
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
    }

    private void OnDisable()
    {
        EventsProvider.OnOrientationChanged -= OrientationChanged;
    }
}