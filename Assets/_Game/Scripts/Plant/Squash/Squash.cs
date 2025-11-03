using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash  : AttackPlant
{
    private SquashAttackState squashAttackState = new SquashAttackState(); 
    protected float attackDamage;
    public float AttackDamage => attackDamage;
    public override void OnInit()
    {
        base.OnInit();
        attackDamage = plantData.attackDamage;
        ChangeState(PlantIdleState);
    }

    public override void ChangeAttackState()
    {
        ChangeState(squashAttackState);
    }
}