using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {MainMenu=1, GamePlay = 2, Win = 3, Lose = 4, Setting = 5, Pause =6}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject backGround2;
    [SerializeField] private GameObject PreviewLevelZombies;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private PlantCartControl plantCartControl;

    private static GameState gameState;
    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }


    public void ChangeState(GameState state)
    {
        gameState = state;
    }
    public bool IsState(GameState state) => gameState == state;
    public GameState GetState()
    {
        return gameState;
    }


    private void Start()
    {
        OnHome();
    }

    public void OnInit()
    {
        LevelManager.Instance.OnInit();
    }

    public void OnDespawn()
    {
        LevelManager.Instance.OnDespawn();
    }

    public void OnLoad()
    {
        ChangeState(GameState.Pause);
        cameraMove.MoveCamera1();

        UIManager.Instance.CloseAll();
        backGround.SetActive(true);
        PreviewLevelZombies.SetActive(true);

        LevelManager.Instance.OnLoadLevel();
        SoundManager.Instance.PlayBGM(FX.ChoisePlantClip);

        Invoke(nameof(ShowPlantSelectionPanel), 2f);
    }

    public void OnPlay()
    {
        OnDespawn();
        OnLoad();
        OnInit();
    }
    
    public void OnStart()
    {
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        SoundManager.Instance.PlayBGM(FX.IngameClip);
        SoundManager.Instance.PlaySFX(FX.zombieClip);
        LevelManager.Instance.OnStart();
        ChangeState(GameState.GamePlay);
        CharacterManager.Instance.ResumAllCharacter();
    }

    public void OnPause()
    {
        ChangeState(GameState.Pause);
        CharacterManager.Instance.PauseAllCharacter();
    }

    public void OnResume()
    {
        ChangeState(GameState.GamePlay);
        CharacterManager.Instance.ResumAllCharacter();
    }

    public void OnWin()
    {
        ChangeState(GameState.Win);
        UIManager.Instance.OpenUI<CanvasVictory>();
        SoundManager.Instance.PlayBGM(FX.WinClip);
    }

    public void OnLose()
    {
        ChangeState(GameState.Lose);
        UIManager.Instance.OpenUI<CanvasFail>();
        SoundManager.Instance.PlayBGM(FX.LoseClip);
    }

    public void OnNextLevel()
    {
        LevelManager.Instance.OnNextLevel();
        OnPlay();
    }

    public void OnRetry()
    {
        OnPlay();
    }

    public void OnSettings()
    {
        OnPause();
    }
    
    //tro ve home
    public void OnHome()
    {
        ChangeState(GameState.MainMenu);
        OnDespawn();

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        backGround.SetActive(false);
        PreviewLevelZombies.SetActive(false);

        SoundManager.Instance.PlayBGM(FX.MenuClip);
    }

    public void ShowPlantSelectionPanel()
    {
        UIManager.Instance.OpenUI<CanvasPlantSelection>();
    }

    public void ConfirmPlantSelection()
    {
        cameraMove.MoveCamera2();
        plantCartControl.HideAllPlantCards();
        SoundManager.Instance.StopBGM();
        Invoke(nameof(ShowReadySetPlant), 1.2f);
    }

    private void ShowReadySetPlant()
    {
        backGround2.SetActive(true);
        SoundManager.Instance.PlayBGM(FX.PrePlayClip);
        PreviewLevelZombies.SetActive(false);
        Invoke(nameof(HideReadySetPlant), 3f);
    }
    private void HideReadySetPlant()
    {
        backGround2.SetActive(false);
        OnStart();
    }
}


