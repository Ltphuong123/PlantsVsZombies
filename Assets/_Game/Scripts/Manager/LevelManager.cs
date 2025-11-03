using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private ZombieSpawnControl zombieSpawnControl;
    [SerializeField] private PlayController playController;
    [SerializeField] private GridController gridManager;
    [SerializeField] private LawnMowerController lawnMowerController;
    [SerializeField] private LevelData[] levelDatas;
    private int curLevel = 0;
    [SerializeField] private int sun;
    [SerializeField] private PlantCartControl plantCartControl;

    public void OnInit()
    {
        sun = 1000;
        plantCartControl.UpdateSunUI(sun);
    }

    public void OnPlay()
    {
        OnDespawn();
        OnLoadLevel();
        OnInit();
    }
    
    public void OnStart()
    {
        plantCartControl.UpdateCardAvailability(sun);
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

    public void AddSun(int s)
    {
        sun += s;
        plantCartControl.UpdateSunUI(sun);
    }
    public int GetSun()
    {
        return sun;
    }
    
}
