using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private HashSet<Plant> activePlants = new HashSet<Plant>();
    private HashSet<Zombie> activeZombies = new HashSet<Zombie>();

    [SerializeField] private GridController gridController;

    public Plant SpawnPlant(PoolType poolType, Vector3 pos, Quaternion rot)
    {
        Plant plant = SimplePool.Spawn<Plant>(poolType, pos, rot);
        activePlants.Add(plant);
        return plant;
    }

    public void OnDespawnPlant(Plant plant)
    {
        gridController.RevomePlant(plant);
        activePlants.Remove(plant);
    }

    public Zombie SpawnZombie(PoolType poolType, Vector3 pos, Quaternion rot)
    {
        Zombie zombie = SimplePool.Spawn<Zombie>(poolType, pos, rot);
        activeZombies.Add(zombie);
        return zombie;
    }

    public void OnDespawnZombie(Zombie zombie)
    {
        activeZombies.Remove(zombie);
    }

    public HashSet<Zombie> GetAllZombies()
    {
        return activeZombies;
    }

    public HashSet<Plant> GetAllPlants()
    {
        return activePlants;
    }

    public int GetCountZombie()
    {
        int count = 0;
        foreach (Zombie z in activeZombies)
        {
            if (z.gameObject.activeSelf)
                count++;
        }
        return count;
    }

    public int GetCountPlant()
    {
        int count = 0;
        foreach (Plant p in activePlants)
        {
            if (p.gameObject.activeSelf)
                count++;
        }
        return count;
    }

    public void PauseAllZombie()
    {
        foreach (Zombie z in activeZombies)
        {
            if (z.gameObject.activeSelf)
            {
                z.PauseCharacter();
            }
        }
    }

    public void PauseAllPlant()
    {
        foreach (Plant p in activePlants)
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
        foreach (Zombie z in activeZombies)
        {
            if (z.gameObject.activeSelf)
            {
                z.ResumCharacter();
            }
        }
    }

    public void ResumAllPlant()
    {
        foreach (Plant p in activePlants)
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
