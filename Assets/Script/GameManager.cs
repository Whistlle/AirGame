using UnityEngine;

using System.Collections;

public enum GameState
{
    Null,
    Pause,
    Start,
    GameOver
}

public enum GameOverType
{
    //被障碍物阻挡 超出左边界
    OutOfEdge,

    //遇上障碍物 
    Obstacle
}

public class GameManager : MonoBehaviour
{
    
    [HideInInspector]
    public GameState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    GameState _state = GameState.Null;
    int _collections = 0;
    int _collectionsTotal = 0;
    int _collectionsScore = 0;

    public int  Collections
    {
        get
        {
            return _collections;
        }
        set
        {
            _collections = value;
        }
    }

    //收集多少奖励后天空变化一次
    public int BonusCollectionsMount;

    bool _inAnimation = false;

    int _collectionsWaitToAdd = 0;

    //每次获得奖励时天空变蓝的程度
    public float BlueMountDuringEachBonus = 0.1f;
    
    //变灰的程度（每帧）
    public float GrayMountEachFrame = 0.001f;

    public GameObject LandEdge;
    public int Distance
    {
        get;
        set;
    }

    //distance and collection player collect
    public void UpdateScore()
    {
        
        Distance = (int)PlayerManager.Instance.TotalDistance;
        float speed = PlayerManager.Instance.Speed;
        
        int score = Distance + _collectionsScore*100; 
        GUIManager.Instance.UpdateScore(score.ToString());
        if (_inAnimation)
        {
            ++Collections;
        }
        else
        {
            GUIManager.Instance.UpdateCollectionsText(Collections.ToString());
        }
    }
    static GameManager _sharedGameManager;

    public static GameManager Instance
    {
        get
        {
            return _sharedGameManager;
        }
    }

    public UFO Ufo;

    PlayerManager _playerManager;
    GUIManager _guiManager;

    void Update()
    {
        UpdateScore();
        CheckBonusCondition();
        GrayTheSky();
    }

    void CheckBonusCondition()
    {
        if(Collections >= BonusCollectionsMount)
        {
            StartCoroutine(MinusScoreAnimation());
            BlueTheSky();
        }
    }

    IEnumerator MinusScoreAnimation()
    {
        while(true)
        {
            if (Collections > 0)
            {
                //--Collections;
                DecreaseACollection();
                UpdateCollectionsText();
            }
            else
            {
                break;
            }
            yield return 0;
        }

        
        while(true)
        {
            if ( _collectionsWaitToAdd-- > 0)
            {
               // ++Collections;
                IncreaseACollection();
                UpdateCollectionsText();
            }
            else
            {
                break;
            }
            yield return 0;
        }
    }

    void BlueTheSky()
    {
        StartCoroutine(BlueSkyAnimation());
    }

    public float TurnBlueTime;     //数值变化速度

    public RenderImage GameBackground;

    IEnumerator BlueSkyAnimation()
    {
        float startTime = Time.time;
        float x = 0;

        //Slow down to 0 in time
        float rate = 1.0f / TurnBlueTime;
        float t = 0.0f;
        float scale = GameBackground.grayScaleAmount;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            GameBackground.grayScaleAmount =
                Mathf.Lerp(scale,
                scale - BlueMountDuringEachBonus,
                t);
           // GameBackground.grayScaleAmount -= x;
            yield return 0;
        }

    }
   
    void GrayTheSky()
    {
        if (GameBackground.grayScaleAmount <= 0)
            return;
        else
            GameBackground.grayScaleAmount += GrayMountEachFrame;
    }
    void UpdateCollectionsText()
    {
        GUIManager.Instance.UpdateCollectionsText(Collections.ToString());
    }

    void UpdateDistanceText()
    {
        GUIManager.Instance.UpdateDistanceText(Distance.ToString());
    }
    // Use this for initialization
    void Awake()
    {
        _sharedGameManager = this;
    }

    void Start()
    {
        _playerManager = PlayerManager.Instance;
        _guiManager = GUIManager.Instance;

        GameBackground = GameObject.Find("BGCamera").GetComponent<RenderImage>();
    }
    //Called when the level is started
    public void StartLevel()
    {
        State = GameState.Start;
        
        GameGenerator.Instance.Restart(true);	
        Ufo.EnterScene();
     //    GameGenerator.Instance.InitGeneration();						

    }

    public void DropPlayerAndStart()
    {
        PlayerManager.Instance.ResetStatus(true);	//Reset player status, and move the submarine to the starting position
      //  GameGenerator.Instance.Restart(true);
      //  StartCoroutine(GameGenerator.Instance.StartToGenerate(1.25f, 3));	//Start the level generator
    }
    //Called when the game is paused
    public void Pause()
    {
        State = GameState.Pause;
     //   Time.timeScale = 0;
        PlayerManager.Instance.Pause(); //Disable sub controls
        GameGenerator.Instance.Pause();							//Pause the level generator
    }

    //Called then the game is resumed
    public void Resume()
    {
     //   Time.timeScale = 1;
        State = GameState.Start;
        PlayerManager.Instance.Resume();				//Enable the sub controls
        GameGenerator.Instance.Resume();						//Resume level generation
    }

    //Called when a coin has been collected
    public void IncreaseACollection()
    {
        _collections++;
        _collectionsTotal++;
        //update score
        int speed = (int)PlayerManager.Instance.Speed;
        _collectionsScore += speed;
        //Increase coin number
        //MissionManager.Instance.CoinEvent(coins);				//Notify the mission manager
    }
    public void DecreaseACollection()
    {
        _collections--;
    }
    	//Called when the level is restarting
	public void Restart()
	{
        State = GameState.Start;
		_collections = 0;										//Reset coin numbers
        PlayerManager.Instance.gameObject.SetActive(false);
        GameManager.Instance.LandEdge.GetComponent<Collider2D>().enabled = true;
        GameGenerator.Instance.Restart(true);	
        Ufo.EnterScene();

        GameBackground.grayScaleAmount = 1;
      //  PlayerManager.Instance.ResetStatus(false);				//Reset player status	
      //  GameGenerator.Instance.Restart(true);					//Restart level generator
        					

       // GUIManager.Instance.ShowStartPowerUps();					//Show the power up activation GUI
       // GUIManager.Instance.ActivateMainGUI();					//Activate main GUI
       // GUIManager.Instance.UpdateBestDistance();				//Update best distance at the hangar
	}

    public void GameOver(GameOverType type)
    {
        State = GameState.GameOver;
        GUIManager.Instance.UnactivatePauseButton();
        _playerManager.OnGameOver();
        //播放一些动画
        switch (type)
        {
            case GameOverType.Obstacle:
            {
               
                break;
            }
            case GameOverType.OutOfEdge:
            {
                break; 
            }
        }
        ScoreManager.Instance.AddScoreToRecord(Distance);
        //停止滚动然后显示得分菜单
       StartCoroutine(GameGenerator.Instance.StopScrollingAndShowEnd(2.0f));
    }



}
