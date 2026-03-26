using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource_BGM;

    [SerializeField] private AudioSource[] arr_AudioSources_SFX;
    [SerializeField] private AudioClip[] arr_AudioClips;

    private Dictionary<string, AudioClip> dic_AudioClips = new Dictionary<string, AudioClip>();

    // === Added: SFX/Music control ===
    private bool sfxEnabled = true;
    private bool musicEnabled = true;

    private const string PREF_SFX = "SFX_Enabled";
    private const string PREF_MUSIC = "Music_Enabled";
    // === End added ===

    protected override void Awake()
    {
        base.Awake();
        if (arr_AudioClips != null && arr_AudioClips.Length > 0)
            foreach (var e in arr_AudioClips)
            {
                dic_AudioClips.Add(e.name, e);
            }

        // === Added: load prefs ===
        sfxEnabled = PlayerPrefs.GetInt(PREF_SFX, 1) == 1;
        musicEnabled = PlayerPrefs.GetInt(PREF_MUSIC, 1) == 1;
        UpdateBGMVolume();
        // === End added ===
    }
    // Thêm phương thức mới để phát SFX với volume và pitch tùy chỉnh
    public void Play_SFX_Custom(string sfxName, float volume = 1f, float pitch = 1f)
    {
        // === Added: check SFX enabled ===
        if (!sfxEnabled) return;

        dic_AudioClips.TryGetValue(sfxName, out AudioClip audClip);
        if (audClip != null)
        {
            foreach (var e in arr_AudioSources_SFX)
            {
                if (!e.isPlaying)
                {
                    e.volume = volume;  // Điều chỉnh volume
                    e.pitch = pitch;    // Điều chỉnh pitch
                    e.PlayOneShot(audClip);
                    // Reset về mặc định sau phát (tùy chọn)
                    StartCoroutine(ResetAudioSource(e, audClip.length));
                    break;
                }
            }
        }
    }

    private IEnumerator ResetAudioSource(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.volume = 1f;  // Reset volume
        source.pitch = 1f;   // Reset pitch
    }
    public void Play_BGM(string bgmName)
    {
        dic_AudioClips.TryGetValue(bgmName, out AudioClip audClip);
        if (audClip != null)
        {
            audioSource_BGM.clip = audClip;

            // === Modified: only play if music is enabled ===
            if (musicEnabled)
                audioSource_BGM.Play();
        }
    }

    //Usage Example: Play_SFX("SFX_Click");
    public void Play_SFX(string sfxName)
    {
        // === Added: check SFX enabled ===
        if (!sfxEnabled) return;

        dic_AudioClips.TryGetValue(sfxName, out AudioClip audClip);
        if (audClip != null)
        {
            foreach (var e in arr_AudioSources_SFX)
            {
                if (!e.isPlaying)
                {
                    e.PlayOneShot(audClip);
                    break;
                }
            }
        }
    }

    public bool Play_SFX_Loop(string sfxName, float volume = 1f, float pitch = 1f)
    {
        if (!sfxEnabled) return false;
        if (!dic_AudioClips.TryGetValue(sfxName, out AudioClip audClip)) return false;

        AudioSource source = null;
        foreach (var e in arr_AudioSources_SFX)
        {
            if (e.isPlaying && e.clip == audClip)
            {
                source = e;
                break;
            }
        }

        if (source == null)
            source = System.Array.Find(arr_AudioSources_SFX, e => !e.isPlaying);

        if (source == null) return false;

        source.clip = audClip;
        source.loop = true;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        return true;
    }

    public void Stop_SFX(string sfxName)
    {
        if (!dic_AudioClips.TryGetValue(sfxName, out AudioClip audClip)) return;

        foreach (var e in arr_AudioSources_SFX)
        {
            if (e.clip == audClip && e.isPlaying && e.loop)
            {
                e.Stop();
                e.loop = false;
                e.volume = 1f;
                e.pitch = 1f;
            }
        }
    }

    public void Set_SFX_Pitch(string sfxName, float pitch)
    {
        if (!dic_AudioClips.TryGetValue(sfxName, out AudioClip audClip)) return;

        foreach (var e in arr_AudioSources_SFX)
        {
            if (e.clip == audClip && e.isPlaying)
            {
                e.pitch = pitch;
            }
        }
    }

    public void Set_SFX_Volume(string sfxName, float volume)
    {
        if (!dic_AudioClips.TryGetValue(sfxName, out AudioClip audClip)) return;

        foreach (var e in arr_AudioSources_SFX)
        {
            if (e.clip == audClip && e.isPlaying)
            {
                e.volume = volume;
            }
        }
    }


    // === Added: Public control APIs ===
    public void SetSFXEnabled(bool enabled)
    {
        sfxEnabled = enabled;
        PlayerPrefs.SetInt(PREF_SFX, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        PlayerPrefs.SetInt(PREF_MUSIC, enabled ? 1 : 0);
        PlayerPrefs.Save();
        UpdateBGMVolume();

        if (musicEnabled && !audioSource_BGM.isPlaying && audioSource_BGM.clip != null)
        {
            audioSource_BGM.Play();
        }
        else if (!musicEnabled && audioSource_BGM.isPlaying)
        {
            audioSource_BGM.Pause();
        }
    }

    public bool IsSFXEnabled() => sfxEnabled;
    public bool IsMusicEnabled() => musicEnabled;

    private void UpdateBGMVolume()
    {
        if (audioSource_BGM != null)
            audioSource_BGM.volume = musicEnabled ? 1f : 0f;
    }
    // === End added ===
}
