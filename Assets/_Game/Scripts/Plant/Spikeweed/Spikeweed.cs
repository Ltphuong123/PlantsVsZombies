using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeweed : AttackPlant
{
    private float attackRate;
    private float attackDamage;
    public float AttackRate => attackRate;

    private SpikeweedAttackState spikeweedAttackState = new SpikeweedAttackState();

    public override void OnInit()
    {
        base.OnInit();
        attackRate = plantData.attackRate;
        attackDamage = plantData.attackDamage;
        ChangeState(PlantIdleState);
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRank, attackLayer);
        foreach (var hit in hits)
        {
            Zombie zombie = hit.gameObject.GetComponent<Zombie>();

            if (zombie != null)
            {
                zombie.TakeDamage(attackDamage);
            }
        }
    }

    public float GetAttackRate()
    {
        return attackRate;
    }

    public override Zombie FineZombieInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position - new Vector3(0.2f,0.2f,0f),
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

    public override void ChangeAttackState()
    {
        ChangeState(spikeweedAttackState);
    }
}