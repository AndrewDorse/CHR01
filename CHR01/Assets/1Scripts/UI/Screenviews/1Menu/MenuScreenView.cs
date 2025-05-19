using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenView : ScreenView
{
    public Button startBattleButton;
    public TMPro.TMP_Text coinsAmount;
    public TMPro.TMP_Text levelText;

    public Button privacyPolicyButton;
    public Button settingsButton;


    public override ScreenController Construct()
    {
        return new MenuScreenController(this);
    }
}
