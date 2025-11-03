// PotatoMineExplosionState.cs
using UnityEngine;

public class PotatoMineExplosionState : PlantState
{
    private PotatoMine potatoMine;
    private float timer;

    public void OnEnter(Plant plant)
    {
        timer = 0f;
        potatoMine = (PotatoMine)plant;
        potatoMine.ChangeAnim(Constants.ANIM_EXPLODE);
        SoundManager.Instance.PlaySFX(FX.explosionClip);
    }
    
    public void OnExecute(Plant plant)
    {
        timer += Time.deltaTime;
        if (timer >= 0.2f)
        {
            potatoMine.Explode();
        }
        if (timer >= 1f)
        {
            potatoMine.OnDie();
        }
    }
    public void OnExit(Plant plant)
    {
    }
}