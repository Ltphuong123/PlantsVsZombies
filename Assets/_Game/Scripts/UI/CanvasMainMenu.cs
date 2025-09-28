using TMPro;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{

    public void PlayButton()
    {
        LevelManager.Instance.OnPlay();
    }
    public void SettingButton()
    {
        GameManager.Instance.GameSettings();
    }
}
