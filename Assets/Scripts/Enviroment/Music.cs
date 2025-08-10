using UnityEngine;
using System.Collections.Generic;

public class Music : MonoBehaviour
{
    public static Music Instance;

    [Header("Music Settings")]
    public AudioSource musicSource;
    public List<AudioClip> musicTracks;
    public bool playRandomMusic = true;

    [Header("SFX Settings")]
    public AudioSource sfxSource;
    public List<AudioClip> sfxClips;

    [Range(0.8f, 1.2f)]
    public float minPitch = 0.95f;
    [Range(0.8f, 1.2f)]
    public float maxPitch = 1.05f;

    private Dictionary<string, AudioClip> sfxDict;

    private int currentTrackIndex = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Pretvaramo listu SFX u dictionary radi brzog pristupa
        sfxDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in sfxClips)
        {
            if (clip != null)
            {
                sfxDict[clip.name] = clip;

                // 🔹 Forsiraj dekompresiju unapred da ne kasni
                AudioClip dummy = clip;
                float _ = dummy.length; // samo da ga Unity učita u RAM
            }
        }
    }

    void Start()
    {
        PlayNextTrack();
    }

    void Update()
    {
        // Ako muzika prestane, pusti sledeću
        if (!musicSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    // 🎵 Pušta sledeću pesmu
    void PlayNextTrack()
    {
        if (musicTracks.Count == 0) return;

        if (playRandomMusic)
        {
            currentTrackIndex = Random.Range(0, musicTracks.Count);
        }
        else
        {
            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
        }

        musicSource.clip = musicTracks[currentTrackIndex];
        musicSource.Play();
    }

    // 🔊 Pusti SFX po imenu
    public void PlaySFX(string name, float volume = 1f)
    {
        if (sfxDict.ContainsKey(name))
        {
            sfxSource.pitch = Random.Range(minPitch, maxPitch);
            sfxSource.PlayOneShot(sfxDict[name], volume);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }
}
