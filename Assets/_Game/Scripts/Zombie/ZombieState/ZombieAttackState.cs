// ZombieAttackState.cs
using UnityEngine;

public class ZombieAttackState : ZombieState
{
    private Plant targetPlant;
    private float attackTimer;
    public void OnEnter(Zombie zombie)
    {
        zombie.ChangeAnim(Constants.ANIM_ATTACK);
        attackTimer = 0f;
    }

    public void OnExecute(Zombie zombie)
    {
        targetPlant = zombie.FindTargetInRange();
        if (targetPlant== null|| targetPlant.gameObject.activeSelf==false)
        {
            zombie.ChangeState(zombie.ZombieWalkState);
            return;
        }
        attackTimer += Time.deltaTime;
        if (attackTimer >= zombie.AttackRate)
        {
            attackTimer = 0f;
            zombie.Attack();
        }
    }

    public void OnExit(Zombie zombie)
    {
    }
}