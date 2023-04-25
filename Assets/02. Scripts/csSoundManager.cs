using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class csSoundManager : MonoBehaviour
{
    public float soundVolume = 1f;
    public bool isSoundMute = false;
    [Space(10)]
    public AudioClip[] bgmClip;
    [Space(10)]
    public AudioClip[] kahoClip;
    [Space(10)]
    public AudioClip[] effectClip;

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
