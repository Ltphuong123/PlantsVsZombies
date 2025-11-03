// LawnMowerController.cs
using System.Collections.Generic;
using UnityEngine;

public class LawnMowerController : MonoBehaviour
{
    [SerializeField] private GameObject lawnMowerPrefab; 
    [SerializeField] private Transform[] mowerSpawnPoints; 
    private List<GameObject> spawnedMowers = new List<GameObject>();

    public void OnInit()
    {
        SpawnAllMowers();
    }

    public void SpawnAllMowers()
    {
        foreach (Transform spawnPoint in mowerSpawnPoints)
        {
            GameObject newMower = Instantiate(lawnMowerPrefab, spawnPoint.position, spawnPoint.rotation);
            newMower.transform.SetParent(this.transform);
            spawnedMowers.Add(newMower);
        }
    }

    public void OnDespawn()
    {
        foreach (GameObject mower in spawnedMowers)
        {
            if (mower != null)
            {
                Destroy(mower);
            }
        }
        spawnedMowers.Clear();
    }
}