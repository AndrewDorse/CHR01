using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "Scriptable/Level")]
public class Level : ScriptableObject
{
    public int level;
     
    public GameObject levelPrefab;

    public int valueToWin;  
}
