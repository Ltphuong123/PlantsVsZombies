using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    private bool isCracked1;
    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(Constants.ANIM_IDLE);
        isCracked1 = false;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if (currentHealth < maxHealth * 0.7)
        {
            if (!isCracked1)
            {
                ChangeAnim("idle1");
                isCracked1 = true;
            }
        } 
        if(currentHealth < maxHealth*0.3) ChangeAnim("idle2");
    }
}
