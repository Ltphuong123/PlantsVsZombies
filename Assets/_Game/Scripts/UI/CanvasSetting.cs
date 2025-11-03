using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CanvasSetting : UICanvas
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gamePlay;
    [SerializeField] protected Animator animator;
    [SerializeField] private Slider slidersfxSource;
    [SerializeField] private Slider sliderbgmSource; 
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string BGM_VOLUME_KEY = "BGMVolume";


    public void SetState(UICanvas canvas)
    {
        mainMenu.SetActive(false);
        gamePlay.SetActive(false);

        if (canvas is CanvasMainMenu)
        {
            mainMenu.SetActive(true);
        }
        else if (canvas is CanvasGamePlay)
        {
            gamePlay.SetActive(true);
        }
    }
    public override void Close(float time)
    {
        animator.SetTrigger("close");
        Invoke(nameof(DelayClose), time + 0.3f);
    }
    public void DelayClose()
    {
        base.Close(0);
    }

    public void MainMenuButton()
    {
        GameManager.Instance.OnHome();
    }
    public void ReTryButton()
    {
        GameManager.Instance.OnPlay();
    }
    public void ResumeButton()
    {
        Close(0);
        GameManager.Instance.OnResume();
    }

    private void Start()
    {
        float savedSfx = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
        float savedBgm = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.5f);

        slidersfxSource.value = savedSfx;
        sliderbgmSource.value = savedBgm;

        SoundManager.Instance.SetSfxVolume(savedSfx);
        SoundManager.Instance.SetBgmVolume(savedBgm);

        slidersfxSource.onValueChanged.AddListener(OnSfxVolumeChanged);
        sliderbgmSource.onValueChanged.AddListener(OnBgmVolumeChanged);
    }

    private void OnDestroy()
    {
        slidersfxSource.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        sliderbgmSource.onValueChanged.RemoveListener(OnBgmVolumeChanged);
    }

    private void OnSfxVolumeChanged(float value)
    {
        SoundManager.Instance.SetSfxVolume(value);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        PlayerPrefs.Save();
    }

    private void OnBgmVolumeChanged(float value)
    {
        SoundManager.Instance.SetBgmVolume(value);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, value);
        PlayerPrefs.Save();
    }




}
