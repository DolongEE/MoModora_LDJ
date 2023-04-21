using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class csSettings : MonoBehaviour 
{
    public static csSettings instance;

    public float soundVolume = 1f;
    public bool isSoundMute = false;
    public Slider sl;
    public Toggle tg;
    public GameObject pnlSetting;
    public GameObject btnPlaySound;

    private AudioSource myaudio;
    public AudioClip[] soundFile;
    public AudioClip[] soundEffect;

    private bool volumeCh = false;
    private float tempVolume;
    private bool tempMute;

    GameObject _soundWalk;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        myaudio = GetComponent<AudioSource>();
        SceneManager.LoadSceneAsync("scStage");

        PlayBackground(0);
    }

    void Start()
    {
        LoadData();

        soundVolume = sl.value;
        isSoundMute = tg.isOn;

        btnPlaySound.SetActive(true);
        AudioSet();
    }

    private void Update()
    {
        if (volumeCh)
        {
            volumeCh = false;
            AudioTempSet();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }
        }
    }

    public void SaveSetSound()
    {
        soundVolume = sl.value;
        isSoundMute = tg.isOn;

        tempVolume = soundVolume;
        tempMute = isSoundMute;

        AudioSet();

        OnClickBtnSound();

        pnlSetting.SetActive(false);
        btnPlaySound.SetActive(true);
    }

    void AudioSet()
    {
        myaudio.volume = soundVolume;
        myaudio.mute = isSoundMute;
        SaveData();
    }

    void AudioTempSet()
    {
        myaudio.volume = sl.value;
        myaudio.mute = tg.isOn;
    }

    public void OnChangeVolume()
    {
        volumeCh = true;
    }


    public void PlayEffct(Vector3 pos, int sfx, bool _loop)
    {
        if (isSoundMute)
        {
            return;
        }

        if (sfx != 0)
        {
            GameObject _soundObj = new GameObject("sfx");
            _soundObj.transform.position = pos;
            AudioSource _audioSource = _soundObj.AddComponent<AudioSource>();

            _audioSource.clip = soundEffect[sfx];
            _audioSource.volume = soundVolume;
            _audioSource.minDistance = 15.0f;
            _audioSource.maxDistance = 30.0f;
            _audioSource.loop = _loop;
            _audioSource.Play();

            Destroy(_soundObj, _audioSource.clip.length);
        }
    }

    //public void PlayWalk()
    //{
    //    AudioSource _audioSource;

    //    if (_soundWalk == null)
    //    {
    //        _soundWalk = new GameObject("walk_sound");
    //        _soundWalk.transform.position = this.transform.position;
    //        _audioSource = _soundWalk.AddComponent<AudioSource>();
    //    }
    //    else
    //    {
    //        _audioSource = _soundWalk.GetComponent<AudioSource>();
    //    }        

    //    _audioSource.Stop();
    //    _audioSource.clip = soundEffect[0];
    //    _audioSource.volume = soundVolume;
    //    _audioSource.minDistance = 15.0f;
    //    _audioSource.maxDistance = 30.0f;
    //    _audioSource.loop = true;
    //    _audioSource.Play();
    //}

    //public void StopWalk()
    //{
    //    if (_soundWalk == null)
    //    {
    //        return;
    //    }

    //    AudioSource _audioSource = _soundWalk.GetComponent<AudioSource>();
    //    _audioSource.Stop();
    //}

    //버튼 클릭 사운드
    public void OnClickBtnSound()
    {
        if (isSoundMute)
        {
            return;
        }

        GameObject _soundObj = new GameObject("sfx");
        AudioSource _audioSource = _soundObj.AddComponent<AudioSource>();

        _audioSource.clip = soundEffect[0];
        _audioSource.volume = soundVolume;
        _audioSource.Play();

        Destroy(_soundObj, soundEffect[0].length);
    }

    IEnumerator BtnSounds()
    {
        GameObject _soundObj = new GameObject("sfx");
        AudioSource _audioSource = _soundObj.AddComponent<AudioSource>();

        _audioSource.clip = soundEffect[0];
        _audioSource.volume = soundVolume;
        _audioSource.Play();

        Destroy(_soundObj, soundEffect[0].length);

        yield return null;
    }

    //백그라운드 사운드
    public void PlayBackground(int stage)
    {
        myaudio.clip = soundFile[stage];
        AudioSet();
        myaudio.Play();
    }

    //사운드 UI 오픈 클로즈
    public void SoundUiOpen()
    {
        OnClickBtnSound();

        pnlSetting.SetActive(true);
        btnPlaySound.SetActive(false);

        tempVolume = soundVolume;
        tempMute = isSoundMute;
    }

    public void SoundUiClose()
    {
        pnlSetting.SetActive(false);
        btnPlaySound.SetActive(true);

        soundVolume = tempVolume;
        isSoundMute = tempMute;

        sl.value = soundVolume;
        tg.isOn = isSoundMute;

        OnClickBtnSound();
    }

    //사운드 정보 저장 및 불러오기
    public void SaveData()
    {
        PlayerPrefs.SetFloat("SOUNDVOLUME", soundVolume);
        PlayerPrefs.SetInt("ISSOUNDMUTE", System.Convert.ToInt32(isSoundMute));
    }

    public void LoadData()
    {
        sl.value = PlayerPrefs.GetFloat("SOUNDVOLUME");
        tg.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("ISSOUNDMUTE"));

        int isSave = PlayerPrefs.GetInt("ISSAVE");

        if (isSave == 0)
        {
            sl.value = 0.5f;
            tg.isOn = false;

            SaveData();
            PlayerPrefs.SetInt("ISSAVE", 1);
        }
    }
}
