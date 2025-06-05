using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;

public class SkinsPopup : Popup
{
    [SerializeField] private UniversalButton[] _buttons;
    
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Button _closeButton;

    private GameObject _currentModel;

    public void Setup(Action startCallback)
    {
        startCallback?.Invoke();

        _closeButton.onClick.AddListener(()=> Close());


        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Setup(LevelController.Instance.PlayableCharacters[i], Click);
        }


        Click(LevelController.Instance.PlayableCharacters[SaveManager.SaveModel.CharacterNumber - 1]);
    }


    private void Click(PlayableCharacter character)
    {
        if(_currentModel != null)
        {
            Destroy(_currentModel);
        }

        _currentModel = Instantiate(character.prefabMenu, _modelTransform);

        SaveManager.SaveModel.CharacterNumber = character.Number;
        SaveManager.Save();
    }
     

    private void OnDestroy()
    {
        
    } 
}
