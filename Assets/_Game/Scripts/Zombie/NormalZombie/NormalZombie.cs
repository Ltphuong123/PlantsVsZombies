using UnityEngine;

public class NormalZombie : ZombieBase
{
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(zombieWalkState);
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
}