using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip ingameClip;
    [SerializeField] private AudioClip backgroundClip;

    [SerializeField] private AudioClip clickButtonClip;
    [SerializeField] private AudioClip DropObjectToGrillClip;
    [SerializeField] private AudioClip DropObjectToGrillNonDelyClip;
    [SerializeField] private AudioClip[] FinishComboClips;
    [SerializeField] private AudioClip PickupObjectFromGrillClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip kholuaClip;
    [SerializeField] private AudioClip phabangClip;
    [SerializeField] private AudioClip boosterTimeClip;
    [SerializeField] private AudioClip boosterSwapClip;
    [SerializeField] private AudioClip GetRewardClip;
    


    [Header("Volumes")]
    [SerializeField][Range(0f, 1f)] private float sfxVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float bgmVolume = 0.5f;

    private bool isSoundEnabled = true;
    private bool isMusicEnabled = true;

    private const string SOUND_KEY = "SoundEnabled";
    private const string MUSIC_KEY = "MusicEnabled";

    private void Awake()
    {
        isSoundEnabled = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;
        isMusicEnabled = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;

        sfxSource.volume = sfxVolume;
        bgmSource.volume = bgmVolume;

        if (isMusicEnabled)
        {
            PlayMenuMusic();
        }
    }

    #region Toggle
    public void ToggleSound(bool enable)
    {
        isSoundEnabled = enable;
        PlayerPrefs.SetInt(SOUND_KEY, enable ? 1 : 0);
        PlayerPrefs.Save();

        if (!isSoundEnabled)
        {
            sfxSource.Stop();
        }
    }

    public void ToggleMusic(bool enable)
    {
        isMusicEnabled = enable;
        PlayerPrefs.SetInt(MUSIC_KEY, enable ? 1 : 0);
        PlayerPrefs.Save();

        if (!isMusicEnabled)
        {
            bgmSource.Stop();
        }
        else
        {
            if (!bgmSource.isPlaying && bgmSource.clip != null)
            {
                bgmSource.Play();
            }
            else if (!bgmSource.isPlaying && bgmSource.clip == null)
            {
                PlayMenuMusic();
            }
        }
    }

    public bool GetToggleMusic()
    {
        return isMusicEnabled;
    }
    public bool GetToggleSound()
    {
        return isSoundEnabled;
    }

    #endregion

    #region Volume
    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }
    #endregion

    #region BGM
    public void PlayMenuMusic() => PlayBGM(menuClip, true);
    public void PlayIngameMusic() => PlayBGM(ingameClip, true);
    public void PlayBackgroundMusic() => PlayBGM(backgroundClip, true);

    private void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (!isMusicEnabled || clip == null) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
    #endregion

    #region SFX
    public void PlayClick() => PlaySFX(clickButtonClip);
    public void PlayDrop() => PlaySFX(DropObjectToGrillClip);
    public void PlayDrop2() => PlaySFX(DropObjectToGrillNonDelyClip);
    public void PlayFinishCombo(int i)
    {
        if(i>FinishComboClips.Length) PlaySFX(FinishComboClips[FinishComboClips.Length-1]);
        else PlaySFX(FinishComboClips[i-1]);
    }
    public void PlayPickup() => PlaySFX(PickupObjectFromGrillClip);
    public void PlayWin() => PlaySFX(winClip);
    public void PlayLose() => PlaySFX(loseClip);
    public void PlayKholua() => PlaySFX(kholuaClip);
    public void PlayPhabang() => PlaySFX(phabangClip);
    public void PlayBoosterTime() => PlaySFX(boosterTimeClip);
    public void PlayBoosterSwap() => PlaySFX(boosterSwapClip);
    public void PlayGetRewardClip() => PlaySFX(GetRewardClip);



    private void PlaySFX(AudioClip clip)
    {
        if (!isSoundEnabled || clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
    #endregion
}
