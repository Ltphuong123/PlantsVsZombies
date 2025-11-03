using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet :GameUnit
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 20;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] protected LayerMask attackLayer;
    private ZombieBase zombie; 

    public void OnInit(ZombieBase zombie, float damage, float speed)
    {
        this.zombie = zombie;
        this.speed = speed;
        this.damage = damage;
        Invoke("OnDespawn", lifeTime);
    }
    void Update()
    {
        if (!GameManager.Instance.IsState(GameState.GamePlay)) return;

        Vector3 moveDelta = Vector3.right * speed * Time.deltaTime;
        float rayLength = moveDelta.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, rayLength, attackLayer);

        if (hit.collider != null)
        {
            ZombieBase zombie = hit.collider.GetComponent<ZombieBase>();
            if (zombie != null)
            {
                SoundManager.Instance.PlaySFX(FX.BulletHitClip);
                BulletEfect peaBullet = SimplePool.Spawn<BulletEfect>(PoolType.BulletEfect, hit.point, Quaternion.identity);
                peaBullet.OnInit();
                zombie.TakeDamage(damage);
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
