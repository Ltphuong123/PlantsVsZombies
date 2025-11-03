using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEfect : GameUnit
{
    public void OnInit()
    {
        Invoke(nameof(OnDespawn), 0.1f);
    }

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

}
