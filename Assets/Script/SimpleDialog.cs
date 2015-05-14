using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

//very simple
//just for demo
public class SimpleDialog : MonoBehaviour {

    //Two Actor
    public Image Alien;
    public Image Player;

    //Two Dialog Text
    public string[] TextString;
   // public string PlayerText;
    
    //TextBox
    public Text TextBox;

    //Scene to load
    public string SceneToLoad;
    //current step
    //step0: play alien text
    //step1: change to player image and display text
    //step2: change scene
    int _currentStep = 0;

    //current text
    //0: alien
    //1: player
    int _currentText = 0;

    AsyncOperation async;
	// Use this for initialization
	void Start () {
        Alien.gameObject.SetActive(true);
        Player.gameObject.SetActive(false);
      //  TextBox.text = "";
        TextBox.DOText(TextString[_currentText], 2f, false);
     //   Application.LoadLevelAsync(SceneToLoad);
	}
    /*
    IEnumerator loadScene()
    {
        //异步读取场景。
        //Globe.loadName 就是A场景中需要读取的C场景名称。
        async = Application.LoadLevelAsync(Globe.loadName);

        //读取完毕后返回， 系统会自动进入C场景
        yield return async;

    }*/
    public void OnTextBoxClick()
    {
        bool nextStep = false;
        if (TextBox.DOComplete() >= 0)
        {
            _currentStep++;
            nextStep = true;
        }
        else
        {
            TextBox.DOKill();
            TextBox.text = TextString[_currentText];
        }

        if(_currentStep == 1 && nextStep)
        {
            _currentText++;
            Alien.gameObject.SetActive(false);
            Player.gameObject.SetActive(true);
            TextBox.text = "";
            TextBox.DOText(TextString[_currentText], 1, false);
        }else
        if(_currentStep == 2)
        {
            Application.LoadLevel(SceneToLoad);
        }
    }
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            OnTextBoxClick();
        }
	}
}
