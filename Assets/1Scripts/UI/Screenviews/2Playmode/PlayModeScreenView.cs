using UnityEngine;
using UnityEngine.UI;

public class PlayModeScreenView : ScreenView
{
    public Button dropBallButton;
    public Button backToMenuButton;
    


     

    public TMPro.TMP_Text scoreAmount; 
    public GameObject[] lifes;

    public override ScreenController Construct()
    {
        return new PlayModeScreenController(this);
    }
}
