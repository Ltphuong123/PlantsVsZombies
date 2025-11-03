// ChomperAttackState.cs
using UnityEngine;

public class ChomperAttackState : PlantState
{
    private Chomper chomper;
    private float timer;

    public void OnEnter(PlantBase plant)
    {
        chomper = (Chomper)plant;
        chomper.ChangeAnim(Constants.ANIM_ATTACK);
        timer = 0f;
        chomper.SetHasEaten(false);
    }

    public void OnExecute(PlantBase plant)
    {
        timer += Time.deltaTime;
        if (timer >= 0.6f && !chomper.HasEaten)
        {
            ZombieBase target = chomper.FineZombieInRange();
            if (target != null)
            {
                target.OnDespawn();
                chomper.SetHasEaten(true);
            }
            else
            {
                chomper.ChangeState(chomper.PlantIdleState);
                return;
            }
        }
        if (timer >= 1f)
        {
            chomper.SetHasEaten(false);
            chomper.ChangeState(chomper.ChomperChewState);
        }
    }
    public void OnExit(PlantBase plant)
    {
    }
}