using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant Cart", menuName = "Plants vs Zombies/Plant Cart")]
public class PlantCartData : ScriptableObject
{
    public string plantName;
    public int sunCost;
    public float rechargeTime;

    public Sprite seedPacketSprite;
    public PoolType poolTypePlant;
}
