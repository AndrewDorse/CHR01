using System;
using UnityEngine;
using UnityEngine.UI;

public class UniversalButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    public void Setup(PlayableCharacter character, Action<PlayableCharacter> action)
    {
        _icon.sprite = character.icon;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => action?.Invoke(character));
    }
}
