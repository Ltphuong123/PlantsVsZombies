using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostLimbZombie : ZombieBase
{
    [SerializeField] private GameObject zombieGibObj;
    [SerializeField] private GameObject zombieGibPrefab;

    protected bool hasLostLimb = false;
    private GameObject zombieGib;

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(zombieWalkState);
        zombieGibObj.SetActive(true);
        hasLostLimb = false;
    }
    public virtual void ZombieGib()
    {
        Transform pos = zombieGibObj.transform;
        zombieGibObj.SetActive(false);
        zombieGib = Instantiate(zombieGibPrefab, pos.position, pos.rotation);
        Destroy(zombieGib, 1f);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (hasLostLimb == false)
        {
            if (currentHealth < maxHealth*0.5f)
            {
                ZombieGib();
                hasLostLimb = true;
            }
        }
    }
    public override void Attack()
    {
        targetPlant.TakeDamage(GetAttackDamage());;
    }
    public override void OnDie()
    {
        base.OnDie();
        ChangeState(zombieDeadState);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        if (zombieGib != null) Destroy(zombieGib);
        zombieGib = null;
    }
}