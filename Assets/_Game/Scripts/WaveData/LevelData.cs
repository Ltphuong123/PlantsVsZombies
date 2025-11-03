using UnityEngine;
using System.Collections.Generic;

public enum WaveType
{
    Normal = 0,
    Hard = 1,
    SuperHard = 2,
}


[System.Serializable]
public class ZombieGroup
{
    public PoolType[] zombiesInRows;
}

[System.Serializable]
public class WaveData
{
    public WaveType waveType;
    public float startDelay;
    public List<ZombieGroup> zombieGroups;
}

[CreateAssetMenu(fileName = "New Level", menuName = "Plants vs Zombies/Level")]
public class LevelData : ScriptableObject
{
    public int time;
    public List<WaveData> waveDatas;
}