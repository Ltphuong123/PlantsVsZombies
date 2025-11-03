using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAttackState : PlantState
{
    private Squash squash;
    private float timer;
    private Zombie target;
    private float elapsedTime;
    private Vector2 startPosition;
    private bool isMoving;

    public void OnEnter(Plant plant)
    {
        squash = (Squash)plant;
        squash.ChangeAnim(Constants.ANIM_ATTACK);
        timer = 0f;
        squash.TF.position = new Vector3(
            squash.TF.position.x,
            squash.TF.position.y,
            -6f
        );
        target = squash.FineZombieInRange();
        elapsedTime = 0;
        startPosition = plant.transform.position;
        isMoving = true;
    }

    public void OnExecute(Plant plant)
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / 0.5f);
            plant.transform.position = Vector3.Lerp(startPosition, target.transform.position, progress);

            if (progress >= 1.0f)
            {
                isMoving = false;
                plant.transform.position = target.transform.position;
                target.TakeDamage(squash.AttackDamage);
            }
        }
        timer += Time.deltaTime;

        if (timer >= 1.5f)
        {
            squash.OnDie();
        }
    }
    public void OnExit(Plant plant)
    {
    }
}