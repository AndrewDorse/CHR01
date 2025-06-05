using UnityEngine;
 
[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Scriptable/PlayableCharacter")]
public class PlayableCharacter : ScriptableObject
{
    public Sprite icon;

    public GameObject prefabMenu;

    public GameObject prefabGame;

    public int Number = 1;
}