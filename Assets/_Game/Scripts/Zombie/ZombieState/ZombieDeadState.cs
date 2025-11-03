// ZombieDeadState.cs
using UnityEngine;

public class ZombieDeadState : ZombieState
{
    private float timer;
    public void OnEnter(ZombieBase zombie)
    {
        timer = 0f;
        zombie.ChangeAnim(Constants.ANIM_DEAD);
    }

    public void OnExecute(ZombieBase zombie)
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0f;
            zombie.OnDespawn();
        }
    }

    public void OnExit(ZombieBase zombie)
    {
    }
}