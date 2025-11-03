using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantIdleState : PlantState
{
    AttackPlant attackPlant;
    public void OnEnter(PlantBase plant)
    {
        plant.ChangeAnim(Constants.ANIM_IDLE);
        attackPlant = (AttackPlant)plant;
    }

    public void OnExecute(PlantBase plant)
    {
        ZombieBase zombie = attackPlant.FineZombieInRange();
        if (zombie!= null)
        {
            attackPlant.ChangeAttackState();
        }
    }

    public void OnExit(PlantBase plant)
    {
    }
}