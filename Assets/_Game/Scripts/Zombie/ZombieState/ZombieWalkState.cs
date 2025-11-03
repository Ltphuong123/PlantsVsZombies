using UnityEngine;

public class ZombieWalkState : ZombieState
{
    public void OnEnter(ZombieBase zombie)
    {
        zombie.ChangeAnim(Constants.ANIM_MOVE);
    }

    public void OnExecute(ZombieBase zombie)
    {
        zombie.ChangeAnim(Constants.ANIM_MOVE);
        zombie.Move();
        PlantBase targetPlant = zombie.FindTargetInRange();
        if (targetPlant != null)
        {
            zombie.ChangeState(zombie.ZombieAttackState);
        }
    }
    public void OnExit(ZombieBase zombie)
    {
    }
}