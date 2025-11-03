using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [SerializeField] private List<ZombieData> zombieDatas;
    [SerializeField] private List<PlantData> plantDatas;
    private Dictionary<ZombieType, ZombieData> dataZombieDictionary  = new Dictionary<ZombieType, ZombieData>();
    private Dictionary<PlantType, PlantData> dataPlantDictionary = new Dictionary<PlantType, PlantData>();

    private void Start() {
        foreach (var data in zombieDatas)
        {
            if (!dataZombieDictionary.ContainsKey(data.zombieType))
            {
                dataZombieDictionary.Add(data.zombieType, data);
            }
        }
        foreach (var data in plantDatas)
        {
            if (!dataPlantDictionary.ContainsKey(data.plantType))
            {
                dataPlantDictionary.Add(data.plantType, data);
            }
        }
    }

    public ZombieData GetZombieData(ZombieType zombieType)
    {
        if (dataZombieDictionary.TryGetValue(zombieType, out ZombieData data))
        {
            return data;
        }
        return null;
    }

    public PlantData GetPlantData(PlantType plantType)
    {
        if (dataPlantDictionary.TryGetValue(plantType, out PlantData data))
        {
            return data;
        }
        return null;
    }

}
