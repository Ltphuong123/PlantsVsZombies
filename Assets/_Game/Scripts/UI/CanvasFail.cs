using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
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
    public void ReTryButton()
    {
        GameManager.Instance.OnRetry();
    }
}
