using UnityEngine;

public class SunflowerIdleState : PlantState
{
    private Sunflower sunflower;
    private float productionTimer;
    private float productionRate;

    public void OnEnter(PlantBase plant)
    {
        sunflower = (Sunflower)plant;
        sunflower.ChangeAnim(Constants.ANIM_IDLE);
        productionTimer = 0f;
        productionRate = sunflower.ProductionRate;
    }

    public void OnExecute(PlantBase plant)
    {
        productionTimer += Time.deltaTime;
        sunflower.WaitGenerate(productionTimer);
        if (productionTimer >= productionRate)
        {
            sunflower.PerformAction();
            productionTimer = 0f;
        }
    }

    public void OnExit(PlantBase plant)
    {
    }
}