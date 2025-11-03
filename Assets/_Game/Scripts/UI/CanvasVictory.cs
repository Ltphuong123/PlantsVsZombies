using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] protected Animator animator;
    public override void Close(float time)
    {
        animator.SetTrigger("close");
        Invoke(nameof(DelayClose), time + 0.3f);
    }
    public void DelayClose()
    {
        base.Close(0);
    }
    public void NextButton()
    {
        GameManager.Instance.OnPlay();
    }
    public void MainMenuButton()
    {
        GameManager.Instance.OnHome();
    }

}
