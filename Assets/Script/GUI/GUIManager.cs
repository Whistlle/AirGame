using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GUIManager : MonoBehaviour {
    
    //Score Text
    public Text Collections;
    public Text Distance;
    public Text Score;  
    //backgournd of the start scene
    public GUIMenu Background;

    //main menu
    public GUIMenu MainMenu;

    
    //pause manu
    public GUIMenu PauseMenu;

    //score menu
    public GUIScoreMenu ScoreMenu;

    //GameOver Menu
    public GUIGameOverMenu GameOverMenu;


    //pause in game
    public GameObject PauseButtonInGame;

    //overlay when pause
    public GUIMenu Overlay;

    public bool ShowOverlay;

    public static GUIManager Instance
    {
        get
        {
            return _runGUIManagerInstance;
        }
    }

    
    static GUIManager _runGUIManagerInstance;

    [HideInInspector]
    public GUIMenu CurrentShowMenu;
    [HideInInspector]
    public GUIMenu LastShowMenu;

    public float MenuAnimationDuration = 0.5f;
    
    void Awake()
    {
        _runGUIManagerInstance = this;
    }
	// Use this for initialization
	void Start () {
	    //unactive all
   //   MainMenu.gameObject.SetActive(true);
    //    PauseMenu.gameObject.SetActive(false);
   //   ScoreMenu.gameObject.SetActive(false);
        UnactivatePauseButton();
        CurrentShowMenu = MainMenu;
        LastShowMenu = null;
        Background.ShowInScene = false;
     
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ActivatePauseButton()
    {
        Debug.Log("Active PauseButton");
        PauseButtonInGame.SetActive(true);
    }

    public void UnactivatePauseButton()
    {
        Debug.Log("Unactive PauseButton");
        PauseButtonInGame.SetActive(false);
    }

    #region ButtonClickEvent

    public void OnBack()
    {
        if(!CurrentShowMenu || !LastShowMenu)
        {
            Debug.Log("do not have menu to show");
        }
        QuitAndEnterMenu(CurrentShowMenu, LastShowMenu);
    }

    public void OnHome()
    {
        if (CurrentShowMenu)
            CurrentShowMenu.Quit();
        StartCoroutine(WaitForSeconds(0.5f)); 
        if (!Overlay.ShowInScene)
            Overlay.Enter();
        MainMenu.Enter();
    }

    public void OnShowScore()
    {
        if (CurrentShowMenu)
            QuitAndEnterMenu(CurrentShowMenu, ScoreMenu);
        else
            ScoreMenu.Enter();
        
    }

    public void OnPause()
    {
        //  if (CheckState(GameState.Start))
        //  {
        // ChangeState(GameState.Pause);
        if(!Overlay.ShowInScene)
            Overlay.Enter();
        else
        {
            Debug.Log("Overlay is already showed in screen!");
        }

        PauseMenu.Enter();
        GameManager.Instance.Pause();
        UnactivatePauseButton();
        Time.timeScale = 0;
        //   }

    }
    public void OnStart()
    {
        Debug.Log("On Start");
        //quit menu and background
        /*
        if (CurrentShowMenu)
            CurrentShowMenu.Quit();
        else
            Debug.Log("no menu to quit");
        if(Background.ShowInScene)
            Background.Quit();

        if (Overlay.ShowInScene)
            Overlay.Quit();

        ActivatePauseButton();

        StartCoroutine(
            Util.DelayToInvoke(GameManager.Instance.StartLevel,
                                0.5f));
       */
        OnRestart();
    }
    public void OnResume()
    {
        
      //  if (CheckState(GameState.Pause))
     //   {
            //ChangeState(GameState.Start);
            //background enter
            PauseMenu.Quit();
            if(ShowOverlay)
                Overlay.Quit();

            ActivatePauseButton();

            GameManager.Instance.Resume();
            Time.timeScale = 1;
     //   }
    }

    public void OnGameOver()
    {
        UnactivatePauseButton();
        //show bg & menu
        Overlay.Enter();
        GameOverMenu.Enter();
        StartCoroutine(Util.DelayToInvoke(GameOverMenu.DisplayScore, 0.5f));
    }

    public void OnRestart()
    {
        Time.timeScale = 1;
        if (CurrentShowMenu)
            CurrentShowMenu.Quit();

        if (Overlay.ShowInScene)
            Overlay.Quit();

        GameManager.Instance.Restart();
        ActivatePauseButton();

    }


    public void OnQuit()
    {
        Application.Quit();
    }
    #endregion
    
    void ChangeState(GameState State)
    {
        GameManager.Instance.State = State;
    }

    bool CheckState(GameState State)
    {
        return GameManager.Instance.State == State;
    }
    
    public void UpdateDistanceText(string str)
    {
        Distance.text = str;
    }

    public void UpdateScore(string str)
    {
        Score.text = str;
    }
    public void UpdateCollectionsText(string str)
    {
        Collections.text = str;
    }

    void QuitAndEnterMenu(GUIMenu quit, GUIMenu enter)
    {
        quit.Quit();
        StartCoroutine(WaitForSeconds(0.5f));
        enter.Enter();
    }

    IEnumerator WaitForSeconds(float f)
    {
        yield return new WaitForSeconds(f);
    }
}
