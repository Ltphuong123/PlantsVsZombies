using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFire : Zombie
{
    [SerializeField] private Transform firePoint;
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(zombieWalkState);
    }

    public override void Attack()
    {
        Zombiebullet zombiebullet = SimplePool.Spawn<Zombiebullet>(PoolType.Zombiebullet, firePoint.position, Quaternion.identity);
        zombiebullet.OnInit(GetAttackDamage(), 10f);
    }
    
    public override void OnDie()
    {
        base.OnDie();
        ChangeState(zombieDeadState);
    }
}