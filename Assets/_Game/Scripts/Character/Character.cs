using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : GameUnit
{
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask attackLayer;
    [SerializeField] protected Collider2D collider;
    protected float maxHealth;
    protected string currentAnimName;
    protected bool IsDead => currentHealth <= 0;

    public virtual void OnInit()
    {
        if (collider != null && collider.enabled == false)
        {
            collider.enabled = true;
        }
        ResumCharacter();
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
    
    public void PauseCharacter()
    {
        animator.speed = 0f;
    }
    public void ResumCharacter()
    {
        animator.speed = 1f;
    }


    public virtual void TakeDamage(float damage)
    {
        if (!IsDead)
        {
            currentHealth -= damage;
            if (IsDead)
            {
                currentHealth = 0;
                OnDie();
            }
        }
    }

    public virtual void OnDie()
    {
        if (collider != null&&collider.enabled==true)
        {
            collider.enabled = false;
        }
    }
    
    public virtual void OnDespawn()
    {
        if (collider != null&&collider.enabled==true)
        {
            collider.enabled = false;
        }
        SimplePool.Despawn(this);
    }
}
