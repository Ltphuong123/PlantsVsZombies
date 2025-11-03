using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieBase : Character
{
    [SerializeField] protected ZombieType zombieType; 
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] protected ZombieData zombieData;
    
    protected float moveSpeed;
    protected float attackDamage;
    protected float attackRank;
    protected float attackRate;
    protected ZombieState currentState;
    protected PlantBase targetPlant;

    public float AttackRank => attackRank;
    public float AttackRate => attackRate;
    public ZombieType ZombieType => zombieType;
    public ZombieData ZombieData => zombieData;

    protected ZombieWalkState zombieWalkState = new ZombieWalkState();
    protected ZombieDeadState zombieDeadState = new ZombieDeadState();
    protected ZombieAttackState zombieAttackState = new ZombieAttackState();
    public ZombieWalkState ZombieWalkState => zombieWalkState;
    public ZombieDeadState ZombieDeadState => zombieDeadState;
    public ZombieAttackState ZombieAttackState => zombieAttackState;


    public override void OnInit()
    {
        base.OnInit();
        zombieData = GameDataManager.Instance.GetZombieData(zombieType);
        if (zombieData != null)
        {
            maxHealth = zombieData.maxHealth;
            moveSpeed = zombieData.moveSpeed;
            attackDamage = zombieData.attackDamage;
            attackRank = zombieData.attackRank;
            attackRate = zombieData.attackRate;
        }
        targetPlant = null;
        currentHealth = maxHealth;
    }

    public virtual float GetMoveSpeed()
    {
        return moveSpeed;
    }
    
    public virtual float GetAttackDamage()
    {
        return attackDamage;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            currentState?.OnExecute(this);
        }
    }

    public void ChangeState(ZombieState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public virtual PlantBase FindTargetInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            attackRank,
            attackLayer
        );
        if (hit.collider != null)
        {
            targetPlant = hit.collider.GetComponent<PlantBase>();
            if (targetPlant != null)
            {
                return targetPlant;
            }
        }
        return null;
    }

    public abstract void Attack();

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        HitEffect();
    }

    private void HitEffect()
    {
        foreach (SpriteRenderer item in sprites)
        {
            item.color = new Color(1, 0.7f, 0.7f, 1f);
        }
        Invoke(nameof(HitEffect2), 0.2f);
    }
    private void HitEffect2()
    {
        foreach (SpriteRenderer item in sprites)
        {
            item.color = Color.white;
        }
    }

    public virtual void Move()
    {
        transform.position += Vector3.left * GetMoveSpeed() * Time.deltaTime;
    }

    public override void OnDespawn()
    {
        ChangeAnim("die");
        CharacterManager.Instance.OnDespawnZombie(this);
        base.OnDespawn();
    }
}