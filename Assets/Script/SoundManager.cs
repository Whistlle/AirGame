using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    enum State
    {
        Play,
        Stop
    }
    State _state = State.Play;

    public static SoundManager Instance
    {
        get
        {
            return _sharedSoundManager;
        }
    }

    static SoundManager _sharedSoundManager;

    public AudioSource SoundPlayer;

    public AudioSource BGM;
    

    public float BGMVolume;

    public GUIButton[] SoundDelegateButtons;

    void RegisterAudioButton()
    {

    }
    void Awake()
    {
        _sharedSoundManager = this;
    }

    public void StartBGM()
    {
        _state = State.Play;
        BGM.Play();
        UpdateButtonsImage();
        
    }
    public void PauseBGM()
    {
        _state = State.Stop;
        BGM.Pause();
        UpdateButtonsImage();
    }

    // Use this for initialization
    void Start()
    {
        //SoundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayOneShot(AudioClip sound)
    {
        if(_state == State.Play)
        {
            SoundPlayer.PlayOneShot(sound);
        }
    }

    void PlayAfter(AudioClip sound)
    {
        if(_state == State.Play)
        {
            SoundPlayer.clip = PlayerManager.Instance.RunSound;
            SoundPlayer.Play();
        }
    }

    void UpdateButtonsImage()
    {
        switch(_state)
        {
            case State.Play:
            {
                foreach(GUIButton b in SoundDelegateButtons)
                {
                    b.ChangeImageEnable();
                }
                break;
            }
            case State.Stop:
            {
                foreach (GUIButton b in SoundDelegateButtons)
                {
                    b.ChangeImageUnable();
                }
                break;
            }
        }
    }
}
