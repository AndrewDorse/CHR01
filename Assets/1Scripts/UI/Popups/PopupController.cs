using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupController : MonoBehaviour
{
    [SerializeField] private Popup[] _popups;

    private List<Popup> _popupLayouts;

    private bool _isPopupBlocked = false;

  

    public void SetPopupBlock(bool block)
    {
        _isPopupBlocked = block;
    }

    private void Start()
    {
        if (_popupLayouts == null)
        {
            _popupLayouts = new List<Popup>();
        }
    }

    public T GetPopup<T>(Action<Popup> callback = null) where T : Popup
    {
        if (_popupLayouts == null)
        {
            _popupLayouts = new List<Popup>();
        }

        T itemPopup = Instantiate(GetPopupByType<T>() as T, transform);
        itemPopup .Initialize(Back);
        _popupLayouts.Add(itemPopup);

        callback?.Invoke(itemPopup );

        return itemPopup;
    }

   

    private T GetPopupByType<T>() where T : Popup
    {
        foreach (Popup popup in _popups)
        {
            if (popup is T)
            {
                return popup as T;
            }
        }

        return null;
    }

    

  

    private void Back()
    {
        if (_popupLayouts == null || _popupLayouts.Count == 0) return;

        int lastIndex = _popupLayouts.Count - 1;
        Popup closingPopup = _popupLayouts[lastIndex];

        closingPopup.Close(closingPopup.Destroy);
        _popupLayouts.RemoveAt(lastIndex);

        
    }
}





