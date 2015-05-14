using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIButton : MonoBehaviour
{
    //两张图片
    public Sprite EnableImage;

    public Sprite UnableImage;

    //两个触发的事件
    public Action EnableFunc;

    public Action UnableFunc;

    enum State
    {
        Enbale,
        Unable
    }

    Image _image;

    State _state;
    // Use this for initialization
    void Start()
    {
        _state = State.Enbale;
        _image = GetComponent<Image>();

        EnableFunc = SoundManager.Instance.StartBGM;
        UnableFunc = SoundManager.Instance.PauseBGM;

    }


    public void OnButtonClick()
    {
        switch(_state)
        {
            case State.Enbale:
                OnButtonUnable();
                break;
            case State.Unable:
                OnButtonEnable();
                break;
        }
    }
    public void OnButtonEnable()
    {
        if(_state == State.Unable)
        {
            _state = State.Enbale;

            _image.sprite = EnableImage;

            EnableFunc();
        }
    }

    public void OnButtonUnable()
    {
        if(_state == State.Enbale)
        {
            _state = State.Unable;

            _image.sprite = UnableImage;

            UnableFunc();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeImageEnable()
    {
        _image.sprite = EnableImage;
    }

    public void ChangeImageUnable()
    {
        _image.sprite = UnableImage;
    }
}
