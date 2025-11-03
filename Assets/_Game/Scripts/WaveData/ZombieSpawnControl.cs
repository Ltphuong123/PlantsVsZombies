using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ZombieSpawnControl : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject flagIconPrefab;

    private List<WaveData> waveDatas;
    private float timer = 0f;
    private float levelTime;
    private float groupTimer = 0f;
    private float delayBetweenGroups = 1f;
    private int nextWaveIndex = 0;
    private WaveData currentActiveWave = null;
    private int currentGroupIndex = 0;
    private bool lastGroup;
    private CanvasGamePlay canvasGamePlay;
    private List<GameObject> flags = new List<GameObject>();
    private bool iswin;

    public void OnInit(List<WaveData> waveDatas, float levelTime)
    {
        this.waveDatas = waveDatas;
        timer = 0;
        nextWaveIndex = 0;
        currentActiveWave = null;
        groupTimer = 0f;
        currentGroupIndex = 0;
        timer = 0f;
        lastGroup = false;
        this.levelTime = levelTime;
        flags.Clear();
        iswin = false;
        SetupProgress(); 
    }

    public void SetupProgress()
    {
        canvasGamePlay = UIManager.Instance.OpenUI<CanvasGamePlay>();
        canvasGamePlay.UpdatePregess(0);
        SetupFlagIcons();
        canvasGamePlay.Close(0f);
    }
    private void SetupFlagIcons()
    {
        Transform flagsContainer = canvasGamePlay.GetFlagsContainer();
        float barWidth = flagsContainer.GetComponent<RectTransform>().rect.width;

        foreach (WaveData wave in waveDatas)
        {
            if (wave.waveType != WaveType.Normal)
            {
                GameObject newFlag = Instantiate(flagIconPrefab, flagsContainer);
                flags.Add(newFlag);
                float positionRatio = 1 - wave.startDelay / levelTime;

                float flagXPosition = barWidth * positionRatio;

                RectTransform flagRect = newFlag.GetComponent<RectTransform>();
                flagRect.anchoredPosition = new Vector2(flagXPosition, flagRect.anchoredPosition.y);
            }
        }
    }

    void Update()
    {
        if (iswin) return;
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (waveDatas != null)
            {
                timer += Time.deltaTime;
                if (timer / levelTime <1)
                {
                    canvasGamePlay.UpdatePregess(timer / levelTime);
                }
                if(timer / levelTime >= 1)
                {
                    canvasGamePlay.UpdatePregess(1);
                }
                CheckForNextWave();
                if (currentActiveWave != null)
                {
                    ProcessCurrentWave();
                }
                if (lastGroup)
                {
                    if (CharacterManager.Instance.GetCountZombie() == 0)
                    {
                        Invoke(nameof(win), 2f);
                        iswin = true;
                    }
                }
            }
        }
    }
    private void win()
    {
        GameManager.Instance.OnWin();
    }
    
    private void CheckForNextWave()
    {
        if (nextWaveIndex < waveDatas.Count)
        {
            WaveData nextWave = waveDatas[nextWaveIndex];

            if (timer >= nextWave.startDelay)
            {
                if (currentActiveWave == null)
                {
                    ActivateWave(nextWave);
                    nextWaveIndex++;
                }
            }
        }
    }
    private void ActivateWave(WaveData wave)
    {
        currentActiveWave = wave;
        currentGroupIndex = 0;
        groupTimer = delayBetweenGroups;
    }
    private void ProcessCurrentWave()
    {
        groupTimer += Time.deltaTime;
        if (groupTimer >= delayBetweenGroups)
        {
            groupTimer = 0f;
            if (currentGroupIndex < currentActiveWave.zombieGroups.Count)
            {
                SpawnGroup(currentActiveWave.zombieGroups[currentGroupIndex]);
                currentGroupIndex++;
            }
            else
            {
                currentActiveWave = null;
                if (nextWaveIndex == waveDatas.Count) lastGroup = true;
            }
        }
    }
    private void SpawnGroup(ZombieGroup group)
    {
        PoolType[] zombiesToSpawn = group.zombiesInRows;

        List<int> availableRowIndexes = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availableRowIndexes.Add(i);
        }

        foreach (PoolType zombieType in zombiesToSpawn)
        {
            int randomIndexInList = Random.Range(0, availableRowIndexes.Count);
            int chosenRowIndex = availableRowIndexes[randomIndexInList];
            availableRowIndexes.RemoveAt(randomIndexInList);
            Transform spawnPoint = spawnPoints[chosenRowIndex];
            ZombieBase zombie = CharacterManager.Instance.SpawnZombie(zombieType, spawnPoint.position, Quaternion.identity);
            if (zombie != null)
            {
                zombie.OnInit();
            }
        }
    }
    public void OnDespawn()
    {
        if(flags!= null)
        foreach (GameObject item in flags)
        {
            Destroy(item);
        }
    }
}
