using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantBase : Character
{
    [SerializeField] protected  PlantType plantType;
    [SerializeField] protected PlantData plantData; 
    [SerializeField] protected SpriteRenderer sprite;
    protected PlantState currentState;

    public PlantType PlantType => plantType;
    public PlantData PlantData => plantData;

    
    public override void OnInit()
    {
        base.OnInit();
        plantData = GameDataManager.Instance.GetPlantData(plantType);
        maxHealth = plantData.health;
        currentHealth = maxHealth;
        TF.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0f
        );
    }
    
    public void ChangeState(PlantState newState)
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

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        HitEffect();
    }
    
    private void HitEffect()
    {
        sprite.color = new Color(1, 0.7f, 0.7f, 1f);
        Invoke(nameof(HitEffect2), 0.2f);
    }
    private void HitEffect2()
    {
        sprite.color = Color.white;
    }

    public override void OnDie()
    {
        CharacterManager.Instance.OnDespawnPlant(this);
        OnDespawn();
    }
    public override void OnDespawn()
    {
        if (collider != null)
        {
            collider.enabled = false;
        }
        currentState = null;
        ChangeAnim("empty");
        base.OnDespawn();
    }
}