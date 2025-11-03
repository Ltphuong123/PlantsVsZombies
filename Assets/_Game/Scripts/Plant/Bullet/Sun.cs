using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : GameUnit
{
    [SerializeField] private float initialJumpForce;
    [SerializeField] private float horizontalForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float destroyDelayAfterLanding;
    [SerializeField] private Vector3 collectDestination;
    [SerializeField] private float moveSpeed;

    private Vector2 currentVelocity;
    private bool hasLanded;
    private float targetYPosition;
    private bool isCollecting;

    public virtual void OnInit()
    {
        currentVelocity = new Vector2(Random.Range(-horizontalForce, horizontalForce), initialJumpForce);
        targetYPosition = transform.position.y - 1f;
        hasLanded = false;
        isCollecting = false;
    }
    
    public void Collect()
    {
        SoundManager.Instance.PlaySFX(FX.floopClip);
        isCollecting = true;
        CancelInvoke("OnDespawn");
    }

    void Update()
    {
        if (isCollecting)
        {
            if (collectDestination != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, collectDestination, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, collectDestination) < 0.1f)
                {
                    OnDespawn();
                }
            }
            else
            {
                OnDespawn();
            }
        }
        else if (!hasLanded)
        {
            currentVelocity.y -= gravityScale * Time.deltaTime * 9.8f;
            transform.Translate(currentVelocity * Time.deltaTime);

            if (transform.position.y <= targetYPosition)
            {
                transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
                hasLanded = true;
                currentVelocity = Vector2.zero;
                Invoke("OnDespawn", destroyDelayAfterLanding);
            }
        }
    }

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}