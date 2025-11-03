// PotatoMine.cs
using UnityEngine;

public class PotatoMine : AttackPlant
{
    private float armTime;
    private float attackDamage;
    private float explosionRadius;

    public float ArmTime => armTime;
    public float AttackDamage => attackDamage;

    private PotatoMineHiddenState potatoMineHiddenState = new PotatoMineHiddenState();
    private PotatoMineExplosionState potatoMineExplosionState = new PotatoMineExplosionState();


    public override void OnInit()
    {
        base.OnInit();
        armTime = plantData.armTime;
        attackDamage = plantData.attackDamage;
        explosionRadius = plantData.explosionRadius;
        ChangeState(potatoMineHiddenState);
    }

    public void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, attackLayer);
        foreach (var hit in hits)
        {
            Zombie zombie = hit.gameObject.GetComponent<Zombie>();

            if (zombie != null)
            {
                zombie.TakeDamage(attackDamage);
            }
        }
    }

    public override void ChangeAttackState()
    {
        ChangeState(potatoMineExplosionState);
    }


}