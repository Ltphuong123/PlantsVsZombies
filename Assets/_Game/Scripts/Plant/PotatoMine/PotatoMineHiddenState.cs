// PotatoMineHiddenState.cs
using UnityEngine;

public class PotatoMineHiddenState : PlantState
{
    private PotatoMine potatoMine;
    private float armTimer;
    private float armTime = 15f;

    public void OnEnter(PlantBase plant)
    {
        potatoMine = (PotatoMine)plant;
        potatoMine.ChangeAnim(Constants.ANIM_HIDDEN);
        armTimer = 0f;
        armTime = potatoMine.ArmTime;
    }

    public void OnExecute(PlantBase plant)
    {
        armTimer += Time.deltaTime;
        if (armTimer >= armTime)
        {
            potatoMine.ChangeAnim("armed");
        }
        if (armTimer >= armTime+1f)
        {
            potatoMine.ChangeState(potatoMine.PlantIdleState);
        }
    }

    public void OnExit(PlantBase plant) {}
}