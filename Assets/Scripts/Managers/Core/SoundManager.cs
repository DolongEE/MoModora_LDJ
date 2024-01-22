using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SoundSetting
{
    public float _bgmVolume;
    public float _effectVolume;

    public SoundSetting()
    {
        _bgmVolume = 1.0f;
        _effectVolume = 1.0f;
    }
}

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    SoundSetting SoundSetting = new SoundSetting();
    public float BgmVolume {  get { return SoundSetting._bgmVolume; } set {  SoundSetting._bgmVolume = value; } }
    public float EffectVolume { get { return SoundSetting._effectVolume; } set {  SoundSetting._effectVolume = value; } }

    public void Init()
    {
        GameObject root = GameObject.Find("@SoundRoot");
        if(root == null)
        {
            root = new GameObject { name = "@SoundRoot" };
            Object.DontDestroyOnLoad(root);
        }

        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
        for(int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = root.transform;
        }
        LoadSoundSetting();
        _audioSources[(int)Define.Sound.Bgm].loop = true;
    }

    public bool Play(Define.Sound type, string path, float pitch = 1.0f)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        AudioSource audioSource = _audioSources[(int)type];

        AudioClip audioClip;

        switch (type)
        {
            case Define.Sound.Bgm:
                path = $"Sounds/Bgm/{path}";
                audioClip = GetAudioClip(path);

                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
                break;
            case Define.Sound.Effect:
                path = $"Sounds/Effect/{path}";
                audioClip = GetAudioClip(path);

                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
                break;
            case Define.Sound.UI:
                path = $"Sounds/UI/{path}";
                audioClip = GetAudioClip(path);

                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
                break;
        }
        return false;
    }

    AudioClip GetAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip))
            return audioClip;
        
        audioClip = Managers.Resource.Load<AudioClip>(path);
        _audioClips.Add(path, audioClip);

        return audioClip;
    }
    void SetSound()
    {
        _audioSources[(int)Define.Sound.Effect].volume = EffectVolume;
        _audioSources[(int)Define.Sound.Bgm].volume = BgmVolume;
    }

    public void SaveSoundSetting(float effect, float bgm)
    {
        EffectVolume = effect;
        BgmVolume = bgm;

        PlayerPrefs.SetFloat($"EffectVolume", EffectVolume);
        PlayerPrefs.SetFloat($"BgmVolume", BgmVolume);
        
        SetSound();
    }

    public void LoadSoundSetting()
    {
        EffectVolume = PlayerPrefs.GetFloat($"EffectVolume", 1.0f);
        BgmVolume = PlayerPrefs.GetFloat($"BgmVolume", 1.0f);

        SetSound();
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        _audioClips.Clear();
    }
}
