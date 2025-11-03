using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private HashSet<PlantBase> activePlants = new HashSet<PlantBase>();
    private HashSet<ZombieBase> activeZombies = new HashSet<ZombieBase>();

    [SerializeField] private GridController gridController;

    public PlantBase SpawnPlant(PoolType poolType, Vector3 pos, Quaternion rot)
    {
        PlantBase plant = SimplePool.Spawn<PlantBase>(poolType, pos, rot);
        activePlants.Add(plant);
        return plant;
    }

    public void OnDespawnPlant(PlantBase plant)
    {
        gridController.RevomePlant(plant);
        activePlants.Remove(plant);
    }

    public ZombieBase SpawnZombie(PoolType poolType, Vector3 pos, Quaternion rot)
    {
        ZombieBase zombie = SimplePool.Spawn<ZombieBase>(poolType, pos, rot);
        activeZombies.Add(zombie);
        return zombie;
    }

    public void OnDespawnZombie(ZombieBase zombie)
    {
        activeZombies.Remove(zombie);
    }

    public HashSet<ZombieBase> GetAllZombies()
    {
        return activeZombies;
    }

    public HashSet<PlantBase> GetAllPlants()
    {
        return activePlants;
    }

    public int GetCountZombie()
    {
        int count = 0;
        foreach (ZombieBase z in activeZombies)
        {
            if (z.gameObject.activeSelf)
                count++;
        }
        return count;
    }

    public int GetCountPlant()
    {
        int count = 0;
        foreach (PlantBase p in activePlants)
        {
            if (p.gameObject.activeSelf)
                count++;
        }
        return count;
    }

    public void PauseAllZombie()
    {
        foreach (ZombieBase z in activeZombies)
        {
            if (z.gameObject.activeSelf)
            {
                z.PauseCharacter();
            }
        }
    }

    public void PauseAllPlant()
    {
        foreach (PlantBase p in activePlants)
        {
            if (p.gameObject.activeSelf)
            {
                p.PauseCharacter();
            }
        }
    }

    public void PauseAllCharacter()
    {
        PauseAllPlant();
        PauseAllZombie();
    }

    public void ResumAllZombie()
    {
        foreach (ZombieBase z in activeZombies)
        {
            if (z.gameObject.activeSelf)
            {
                z.ResumCharacter();
            }
        }
    }

    public void ResumAllPlant()
    {
        foreach (PlantBase p in activePlants)
        {
            if (p.gameObject.activeSelf)
            {
                p.ResumCharacter();
            }
        }
    }

    public void ResumAllCharacter()
    {
        ResumAllZombie();
        ResumAllPlant();
    }

}
