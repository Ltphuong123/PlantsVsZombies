using UnityEngine;

public class Sunflower : Plant
{
    [SerializeField] private Transform sunSpawnPoint;
    private float productionRate;
    public float ProductionRate => productionRate;
    private SunflowerIdleState sunflowerIdleState = new SunflowerIdleState();

    public override void OnInit()
    {
        base.OnInit();
        productionRate = plantData.productionRate;
        ChangeState(sunflowerIdleState);
    }
    public void PerformAction()
    {
        Sun sun = SimplePool.Spawn<Sun>(PoolType.Sun, sunSpawnPoint.position, Quaternion.identity);
        sun.OnInit();
    }
    public void WaitGenerate(float t)
    {
        float k = 0.6f + (t / productionRate) * 0.4f;
        sprite.color = new Color(k, k, k);
    }
    
}