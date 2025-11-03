using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooterFireState : PlantState
{
    private PlantShooter plantShooter;
    private float fireTimer;

    public void OnEnter(PlantBase plant)
    {
        plantShooter = (PlantShooter)plant;
        plantShooter.ChangeAnim(Constants.ANIM_FIRE);
        fireTimer = 0;
    }
    public void OnExecute(PlantBase plant)
    {
        ZombieBase zombie = plantShooter.FineZombieInRange();
        if (zombie == null)
        {
            plantShooter.ChangeState(plantShooter.PlantIdleState);
            return;
        }
        fireTimer += Time.deltaTime;
        if (fireTimer >= plantShooter.AttackRate)
        {
            fireTimer = 0f;
            plantShooter.Fire();
        }
    }
    public void OnExit(PlantBase plant)
    {
    }
}
