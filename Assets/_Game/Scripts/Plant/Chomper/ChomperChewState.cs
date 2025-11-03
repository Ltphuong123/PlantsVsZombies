// ChomperChewState.cs
using UnityEngine;

public class ChomperChewState : PlantState
{
    private Chomper chomper;
    private float chewTimer;
    public void OnEnter(Plant plant)
    {
        chomper = (Chomper)plant;
        chomper.ChangeAnim(Constants.ANIM_CHEW);
        chewTimer = 0f;
    }
    public void OnExecute(Plant plant)
    {
        chewTimer += Time.deltaTime;
        if (chewTimer >= chomper.DigestTime)
        {
            chomper.ChangeState(chomper.PlantIdleState);
        }
    }
    public void OnExit(Plant plant)
    {
    }
}