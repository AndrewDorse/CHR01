using UnityEngine;
using UnityEngine.UI;

public class PlayModeScreenView : ScreenView
{
    public Button dropBallButton;
    public Button backToMenuButton;
    public Button increaseBallsButton;
    public Button increaseValueButton;
    public Button decreaseBallsButton;
    public Button decreaseValueButton;


    public TMPro.TMP_Text ballAmountText;
    public TMPro.TMP_Text ballValueText;

    public TMPro.TMP_Text moneyAmount;
    public TMPro.TMP_Text ballToWinText;


    public override ScreenController Construct()
    {
        return new PlayModeScreenController(this);
    }
}
