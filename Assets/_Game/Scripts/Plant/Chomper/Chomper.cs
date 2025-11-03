using UnityEngine;

public class Chomper : AttackPlant
{
    private float digestTime;
    public float DigestTime => digestTime;
    private bool hasEaten = false;
    public bool HasEaten => hasEaten;

    private ChomperAttackState chomperAttackState = new ChomperAttackState();
    private ChomperChewState chomperChewState = new ChomperChewState();
    public ChomperChewState ChomperChewState => chomperChewState;

    
    public void SetHasEaten( bool b)
    {
        hasEaten = b;
    }


    public override void OnInit()
    {
        base.OnInit();
        digestTime = plantData.digestTime;
        ChangeState(PlantIdleState);
    }

    public override void ChangeAttackState()
    {
        ChangeState(chomperAttackState);
    }
}