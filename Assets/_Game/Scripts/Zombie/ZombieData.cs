using UnityEngine;

[CreateAssetMenu(fileName = "NewZombieData", menuName = "Plants vs Zombies/Zombie Data/a")]
public class ZombieData : ScriptableObject
{
    public ZombieType zombieType;
    public float maxHealth;
    public float moveSpeed;
    public float attackDamage;
    public float attackRank;
    public float attackRate;
}

