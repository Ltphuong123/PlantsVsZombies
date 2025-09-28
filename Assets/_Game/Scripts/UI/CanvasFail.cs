using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    public void MainMenuButton()
    {
        LevelManager.Instance.OnHome();
    }
    public void ReTryButton()
    {
        LevelManager.Instance.OnPlay();
    }
}
