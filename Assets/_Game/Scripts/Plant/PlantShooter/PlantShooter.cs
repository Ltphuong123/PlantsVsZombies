using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooter : AttackPlant
{
    [SerializeField] protected Transform firePoint;

    protected float attackRate;
    protected float attackDamage;

    public float AttackRate => attackRate;
    public float AttackDamage => attackDamage;

    private PlantShooterFireState plantShooterFireState = new PlantShooterFireState();
    public PlantShooterFireState PlantShooterFireState => plantShooterFireState;

    public override void OnInit()
    {
        base.OnInit();
        attackRate = plantData.attackRate;
        attackDamage = plantData.attackDamage;
        ChangeState(PlantIdleState);
    }

    public void Fire()
    {
        SoundManager.Instance.PlaySFX(FX.PlantShoot);
        PeaBullet peaBullet = SimplePool.Spawn<PeaBullet>(PoolType.PeaBullet, firePoint.position, Quaternion.identity);
        peaBullet.OnInit(targetZombie, AttackDamage, 10f);
    }
    public override void ChangeAttackState()
    {
        ChangeState(plantShooterFireState);
    }
}
