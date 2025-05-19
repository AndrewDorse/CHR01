using UnityEngine;

public class Enums
{
    public enum GameStage
    {
        Boot, 
        Menu,
        Playmode,
    }

    public enum RequestStage
    {
        NotSent,
        Sent,
        Successfull,
        Unsuccessfull,
    }
}
