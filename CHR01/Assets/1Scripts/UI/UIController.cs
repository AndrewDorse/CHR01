using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;



public class UIController : MonoBehaviour
{
    public Canvas BattleCanvas => _uiBattleCanvas;
    public Canvas MenuCanvas => _menuCanvas;

    [SerializeField] private ScreenView[] _views;
    [SerializeField] private Canvas _uiBattleCanvas;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Transform _screenTransform;
    [SerializeField] private GameObject _transparentLoadingScreen;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private PopupController _popupController;

    private ScreenController _currentScreen;
    private ScreenView _currentView;





    public void OpenScreen(Enums.GameStage type)
    {
        foreach (ScreenView view in _views)
        {
            if (view.Type == type)
            {
                CreateView(view);
            }
        }
    }

    public void SetLightLoadingScreen()
    {
        _transparentLoadingScreen.SetActive(true);
    }

    public void SetLoadingScreen()
    {
        DOTween.KillAll();
        _loadingScreen.Open();
    }

    public void RemoveLoadingScreen()
    {
        _loadingScreen.Close();
        //StartCoroutine(RemoveLoadingScreenCoroutine());
    }
      
    private void CreateView(ScreenView view)
    {
        Clear();

        _currentView = Instantiate(view, _screenTransform);
        _currentScreen = _currentView.Construct();
    }

    private void Clear()
    {
        if (_currentScreen != null)
        {
            _currentScreen.Close();
        }
    }

    public T GetPopup<T>(Action<Popup> callback = null) where T : Popup
    {
        return _popupController.GetPopup<T>(callback);
    }

   

    public void SetVisibleScreen(bool visible)
    {
        _currentView.gameObject.SetActive(visible);
    }
}
