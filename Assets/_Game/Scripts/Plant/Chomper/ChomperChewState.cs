// ChomperChewState.cs
using UnityEngine;

public class ChomperChewState : PlantState
{
    private Chomper chomper;
    private float chewTimer;
    public void OnEnter(PlantBase plant)
    {
        chomper = (Chomper)plant;
        chomper.ChangeAnim(Constants.ANIM_CHEW);
        chewTimer = 0f;
    }
    public void OnExecute(PlantBase plant)
    {
        chewTimer += Time.deltaTime;
        if (chewTimer >= chomper.DigestTime)
        {
            chomper.ChangeState(chomper.PlantIdleState);
        }
    }
    public void OnExit(PlantBase plant)
    {
    }
}