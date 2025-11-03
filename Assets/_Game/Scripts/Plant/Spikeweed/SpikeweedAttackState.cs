using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeweedAttackState : PlantState
{
    private float attackRate;
    private Spikeweed spikeweed;
    private float fireTimer;
    public void OnEnter(Plant plant)
    {
        spikeweed = (Spikeweed)plant;
        spikeweed.ChangeAnim(Constants.ANIM_ATTACK);
        fireTimer = 0;
        attackRate = spikeweed.AttackRate;
    } 
    public void OnExecute(Plant plant)
    {
        Zombie zombie = spikeweed.FineZombieInRange();
        if (zombie == null)
        {
            spikeweed.ChangeState(spikeweed.PlantIdleState);
            return;
        }
        fireTimer += Time.deltaTime;
        if (fireTimer >= attackRate)
        {
            fireTimer = 0f;
            spikeweed.Attack();
        }
    }
    public void OnExit(Plant plant)
    {
    }
}