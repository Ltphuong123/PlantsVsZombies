using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jalapeno :  Plant
{
    private float attackDamage;
    private float explosionRadius;
    private JalapenoExplosionState jalapenoExplosionState = new JalapenoExplosionState();

    public override void OnInit()
    {
        base.OnInit();
        attackDamage = plantData.attackDamage;
        explosionRadius = plantData.explosionRadius;
        ChangeState(jalapenoExplosionState);
    }

    public void Explode()
    {
        Vector2 origin = transform.position;
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(origin, Vector2.right, explosionRadius, attackLayer);
        foreach (var hit in hitsRight)
        {
            Zombie zombie = hit.collider.GetComponent<Zombie>();

            if (zombie != null)
            {
                zombie.TakeDamage(attackDamage);
            }
        }
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(origin, Vector2.left, explosionRadius, attackLayer);
        foreach (var hit in hitsLeft)
        {
            Zombie zombie = hit.collider.GetComponent<Zombie>();

            if (zombie != null)
            {
                zombie.TakeDamage(attackDamage);
            }
        }
    }
}