using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantIdleState : PlantState
{
    AttackPlant attackPlant;
    public void OnEnter(Plant plant)
    {
        plant.ChangeAnim(Constants.ANIM_IDLE);
        attackPlant = (AttackPlant)plant;
    }

    public void OnExecute(Plant plant)
    {
        Zombie zombie = attackPlant.FineZombieInRange();
        if (zombie!= null)
        {
            attackPlant.ChangeAttackState();
        }
    }

    public void OnExit(Plant plant)
    {
    }
}