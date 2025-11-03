using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPlant : Plant
{
    protected Zombie targetZombie;
    protected float attackRank;
    public float AttackRank => attackRank;

    protected PlantIdleState plantIdleState = new PlantIdleState();
    public PlantIdleState PlantIdleState => plantIdleState;


    public override void OnInit()
    {
        base.OnInit();
        attackRank = plantData.attackRank;
        targetZombie = null;
    }

    public virtual Zombie FineZombieInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            attackRank,
            attackLayer
        );
        if (hit.collider != null)
        {
            targetZombie = hit.collider.GetComponent<Zombie>();
            return targetZombie;
        }
        return null;
    }

    public abstract void ChangeAttackState();
}
