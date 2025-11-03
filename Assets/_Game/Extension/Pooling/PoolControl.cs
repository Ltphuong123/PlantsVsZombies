using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] private PoolAmount[] poolAmounts;

    private void Awake() {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/");
        for (int i = 0; i < gameUnits.Length; i++)
        {
            if (!SimplePool.GetPool(gameUnits[i].PoolType))
            {
                Transform par = new GameObject(gameUnits[i].name).transform;
                par.SetParent(this.transform);
                SimplePool.Preload(gameUnits[i], 0,par);
            }
        }

        for (int i = 0; i < poolAmounts.Length; i++)
        {
            if (!SimplePool.GetPool(gameUnits[i].PoolType))
            {
                SimplePool.Preload(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
            }
        }
    }
}


[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
public enum PoolType
{
    NormalZombie=0,
    NormalZombieHP=1,
    FlyingZombie=2,
    ZombieFire=3,
    LostLimbZombie=4,

    Zombiebullet = 20,

    Sun = 30,
    PeaBullet = 40,
    BulletEfect = 60,

    Peashooter = 70,
    Sunflower = 71,
    Spikeweed = 72,
    PotatoMine = 73,
    Jalapeno = 74,
    Chomper = 75,
    Squash = 76,
    WallNut = 77
}
