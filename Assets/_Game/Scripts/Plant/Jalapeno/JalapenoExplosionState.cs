using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalapenoExplosionState : PlantState
{
    private Jalapeno jalapeno;
    private float timer;
    public void OnEnter(Plant plant)
    {
        timer = 0f;
        jalapeno = (Jalapeno)plant;
        jalapeno.ChangeAnim(Constants.ANIM_IDLE);
        SoundManager.Instance.PlaySFX(FX.explosionClip);
    }
    
    public void OnExecute(Plant plant)
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            jalapeno.ChangeAnim(Constants.ANIM_EXPLODE);
            jalapeno.Explode();
        }
        if (timer >= 1.2f)
        {
            jalapeno.OnDie();
        }
    }
    public void OnExit(Plant plant)
    {
    }
}