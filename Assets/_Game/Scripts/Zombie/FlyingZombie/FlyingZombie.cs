using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingZombie : LostLimbZombie
{
    public override void ZombieGib()
    {
        base.ZombieGib();
    }

    public override float GetMoveSpeed()
    {
        if (hasLostLimb)
        {
            return moveSpeed * 2f;
        }
        return moveSpeed;
    }
    
    public override float GetAttackDamage()
    {
        if (hasLostLimb)
        {
            return attackDamage * 0.5f;
        }
        return attackDamage;
    }

}