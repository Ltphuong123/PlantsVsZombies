using UnityEngine;

public enum FX
{
    MenuClip=0,
    IngameClip=1,
    ChoisePlantClip=2,
    PrePlayClip=3,
    WinClip=4,
    LoseClip=5,
    ClickButtonClip=6,
    PlantShoot=7,
    BulletHitClip=8,
    selectClip=9,
    PlantingClip=10,
    zombieClip=11,
    lawnmowerClip=12,
    explosionClip=13,
    floopClip=14,
    
}
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioClip[] audioClips;


    [Header("Volumes")]
    [SerializeField][Range(0f, 1f)] private float sfxVolume = 0.5f;
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
            PlayBGM(FX.MenuClip, true);
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
                PlayBGM(FX.MenuClip, true);
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

    public void PlayBGM(FX fX, bool loop = true)
    {
        AudioClip clip = audioClips[(int)fX];
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

    public void PlaySFX(FX fX)
    {
        AudioClip clip = audioClips[(int)fX];
        if (!isSoundEnabled || clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}