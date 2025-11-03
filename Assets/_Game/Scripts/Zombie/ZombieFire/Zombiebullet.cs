using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiebullet : GameUnit
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 20;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] protected LayerMask attackLayer;

    public virtual void OnInit(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
        Invoke("OnDespawn", lifeTime);
    }
    
    void Update()
    {
        if (!GameManager.Instance.IsState(GameState.GamePlay)) return;

        Vector3 moveDelta = Vector3.left * speed * Time.deltaTime;
        float rayLength = moveDelta.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength, attackLayer);

        if (hit.collider != null)
        {
            PlantBase plant = hit.collider.GetComponent<PlantBase>();
            if (plant != null)
            {
                SoundManager.Instance.PlaySFX(FX.BulletHitClip);
                plant.TakeDamage(damage);
                OnDespawn();
                return;
            }
        }
        transform.position += moveDelta;
    }
    


    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

}
