using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private Text text;
    
    public void PlayButton()
    {
        GameManager.Instance.OnPlay();
    }
    public void SettingButton()
    {
        GameManager.Instance.OnSettings();
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }
}
