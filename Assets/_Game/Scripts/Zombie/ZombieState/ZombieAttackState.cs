// ZombieAttackState.cs
using UnityEngine;

public class ZombieAttackState : ZombieState
{
    private PlantBase targetPlant;
    private float attackTimer;
    public void OnEnter(ZombieBase zombie)
    {
        zombie.ChangeAnim(Constants.ANIM_ATTACK);
        attackTimer = 0f;
    }

    public void OnExecute(ZombieBase zombie)
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

    public void OnExit(ZombieBase zombie)
    {
    }
}