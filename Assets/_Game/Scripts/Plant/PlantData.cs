using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantData", menuName = "Plants vs Zombies/Plant Data")]
public class PlantData : ScriptableObject
{
    [Header("General Info")]
    public PlantType plantType;
    public float health;

    [Header("Shooter Stats (Dùng cho Peashooter,...)")]
    public float attackDamage;
    public float attackRate;
    public float attackRank;

    [Header("Production Stats (Dùng cho Sunflower)")]
    public float productionRate;

    [Header("Explosive Stats (Dùng cho PotatoMine, Jalapeno)")]
    public float explosionRadius;

    [Header("Potato Mine Specifics")]
    public float armTime;

    [Header("Chomper Stats")]
    public float digestTime;
}