using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClipData clipData)
    {
        if (clipData == null) return;

        _bgmSource.Stop();

        _bgmSource.clip = clipData.Clip;
        _bgmSource.volume = clipData.Volume;
        _bgmSource.spatialBlend = clipData.SpatialBlend;

        _bgmSource.Play();
    }

    public void PlaySFX(AudioClipData clipData)
    {
        if (clipData == null) return;

        _sfxSource.spatialBlend = clipData.SpatialBlend;
        _sfxSource.PlayOneShot(clipData.Clip, clipData.Volume);
    }

    public void PlaySequentialSFX(AudioClipsData clipsData, ref int index)
    {
        if (clipsData == null || clipsData.Clips.Length == 0) return;

        _sfxSource.spatialBlend = clipsData.SpatialBlend;
        AudioClip clip = clipsData.Clips[index % clipsData.Clips.Length];
        _sfxSource.PlayOneShot(clip, clipsData.Volume);

        index++;
    }
}
