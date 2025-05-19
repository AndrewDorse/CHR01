using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] protected Button _backBGButton;

    private float _alpha;


    public void Initialize(Action backCallback)
    {
        if (_backBGButton != null)
        {
            _backBGButton.onClick.AddListener(() => backCallback?.Invoke());
        }
    }

    public void Open(Action callback = null)
    {
        StopAllCoroutines();

        _alpha = 1;
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        callback?.Invoke();
        // StartCoroutine(AppearCoroutine(Constants.SCREEN_APPEARING_TIME, callback));
    }

    public void Close(Action callback = null)
    {
        StopAllCoroutines();

        if (!gameObject.activeInHierarchy) return;

        _alpha = 0;
        _canvasGroup.alpha = 0;

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _backBGButton.onClick.RemoveAllListeners();

        callback?.Invoke();

        Destroy();
       // if (gameObject.activeInHierarchy)
       // StartCoroutine(FadeCoroutine(Constants.SCREEN_DISEPPEARING_TIME, callback));
    }

    public void CloseNow()
    {
        StopAllCoroutines();

        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }







    private IEnumerator FadeCoroutine(float time, Action callback = null)
    {
        while (_alpha > 0)
        {
            _alpha -= Time.unscaledDeltaTime / time;
            _canvasGroup.alpha = _alpha;

            yield return null;
        }

        callback?.Invoke();

        Destroy();
    }

    private IEnumerator AppearCoroutine(float time, Action callback = null)
    {
        while (_alpha < 1)
        {
            _alpha += Time.unscaledDeltaTime / time;
            _canvasGroup.alpha = _alpha;

            yield return null;
        }

        callback?.Invoke();
    }
}
