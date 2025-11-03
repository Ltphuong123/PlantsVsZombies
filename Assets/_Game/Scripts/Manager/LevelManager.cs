using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private ZombieSpawnControl zombieSpawnControl;
    [SerializeField] private PlayController playController;
    [SerializeField] private GridController gridManager;
    [SerializeField] private LawnMowerController lawnMowerController;
    [SerializeField] private LevelData[] levelDatas;
    private int curLevel = 0;
    [SerializeField] private PlantCartControl plantCartControl;

    public void OnInit()
    {
        plantCartControl.OnInitSun(100);
    }

    public void OnPlay()
    {
        OnDespawn();
        OnLoadLevel();
        OnInit();
    }
    
    public void OnStart()
    {
        plantCartControl.UpdateCardAvailability();
    }

    public void OnLoadLevel()
    {
        zombieSpawnControl.OnInit(levelDatas[curLevel].waveDatas, levelDatas[curLevel].time);
        gridManager.OnInit();
        playController.OnInit();
        lawnMowerController.OnInit();
        plantCartControl.OnInit();
    }

    public void OnWin()
    {
    }

    public void OnLose()
    {
    }

    public void OnNextLevel()
    {
        // curLevel += 1;
    }

    public void OnDespawn()
    {
        playController.OnDespawn();
        zombieSpawnControl.OnDespawn();
        gridManager.OnDespawn();
        lawnMowerController.OnDespawn();
        SimplePool.CollectAll();
    }
}
