using UnityEngine;

public class ZombieWalkState : ZombieState
{
    public void OnEnter(Zombie zombie)
    {
        zombie.ChangeAnim(Constants.ANIM_MOVE);
    }

    public void OnExecute(Zombie zombie)
    {
        zombie.ChangeAnim(Constants.ANIM_MOVE);
        zombie.Move();
        Plant targetPlant = zombie.FindTargetInRange();
        if (targetPlant != null)
        {
            zombie.ChangeState(zombie.ZombieAttackState);
        }
    }
    public void OnExit(Zombie zombie)
    {
    }
}