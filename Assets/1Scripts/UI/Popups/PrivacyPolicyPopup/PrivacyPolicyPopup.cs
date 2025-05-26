using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;

public class PrivacyPolicyPopup : Popup
{
    [SerializeField] private Button _closeButton;

     
    public void Setup(Action startCallback)
    {
        startCallback?.Invoke();


        _closeButton.onClick.AddListener( ()=> Close());
    }

     

     
}
