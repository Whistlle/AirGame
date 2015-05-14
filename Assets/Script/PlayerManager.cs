using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class PlayerManager : MonoBehaviour 
{
    //遇到障碍物以后恢复的时间
    public bool IsBlockedByObstacles = false;
    public bool IsStartRecovering = false;
    public float RecoverTimeAfterBlock = 2;
    public float RecoveringSpeed = 2;

    //已跑过的距离
    public float TotalDistance = 0f;

	float newSpeed 							= 0.0f;						
    
    //The first obstacle generated
    bool firstObstacleGenerated = false;	
    bool paused = false;					                
    bool controllEnabled = false;					

    //float _recoverTimePast = 0f;

	Transform thisTransform;											

    public int _jumpState = 4;              
    int _lastJumpState = -1;
    bool _fellOff = false; 

    float _turnDamping = 0f; 

    float _jumpChange = 1f; 
    
    float _jumpChangeTime = 0f; 

    float _gravityScale = 0;

    [HideInInspector]
    public float Speed = 0f; 
    public float MinSpeed = 0.2f;
    public float MaxSpeed = 0.3f; 
    public float Acceleration = 0.001f; 

    public float JumpPower = 10f; 

    public AnimationClip RunAnimation;
    public AnimationClip JumpAnimation;
    public AnimationClip FallAnimation;

    public AudioClip RunSound;
    public AudioClip JumpCound;

   // public EdgeCollider2D TopEdge;
    string _runAnimationName;
    string _jumpAnimationName;
    string _fallAnimationName;

    GameObject _lastCollection = null;
    bool _fallAtStart = true;
    Rigidbody2D _rigidBody;

    Vector3 _playerDefaultPosition;
    //Retursn the instance
    static PlayerManager _playerManagerInstance;
    public static PlayerManager Instance
    {
        get
        {
            return _playerManagerInstance;
        }
    }

    void Awake()
    {
        _playerManagerInstance = this;
        _playerDefaultPosition = this.transform.position;
        
        
    }

	//Called at the beginning the game
	void Start()
	{
        this.gameObject.SetActive(false);

		thisTransform = this.GetComponent<Transform>();


        //init player animation name
        _jumpAnimationName = JumpAnimation.name;
        _runAnimationName = RunAnimation.name;
        _fallAnimationName = FallAnimation.name;

        _rigidBody = GetComponent<Rigidbody2D>();

        
        //GetComponent<Collider2D>().enabled = false;
        _gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().gravityScale = 0;

	}
	//Called at every frame
	void Update()
	{
        Speed = GameGenerator.Instance.ScrollSpeed;
		//If the control are enabled
        if (GameManager.Instance.State == GameState.Start)
        {
            if (_fellOff == false) 
            {
                //Increase speed based on acceleration
                if (Speed < MaxSpeed)
                {
                    Speed += Acceleration;
                }

                //Add to the distance value in the game controller
                TotalDistance += Speed / 10;

                //If turning speed is being damped by the value of TurnDamping, gradually taking it back to normal ( 1 )
                if (_turnDamping < 1)
                    _turnDamping += 0.5f * Time.deltaTime;

                if (_jumpChangeTime > 0) 
                    _jumpChangeTime -= Time.deltaTime; //reduce the change time
                else if (_jumpChange != 1) 
                    _jumpChange = 1;

                if (JumpButtonDown() && _jumpState == 0) 
                {
                    _jumpState = 1; //Set the jump state to 1, Jumping
                    //  _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpPower * _jumpChange); //Give the player an up velocity
                    _rigidBody.velocity = new Vector2(0, JumpPower * _jumpChange);
                    //GetComponent<AudioSource>().PlayOneShot(JumpSound); //Play a jump sound
                }
                else if (JumpButtonDown() && _rigidBody.velocity.y > 0 && _jumpState == 1) 
                {
                    _jumpState = 2; //fall after jump
                    // _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * 0.3f);
                    _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y * 0.3f);
                }

                if (_rigidBody.velocity.y < 0 && _jumpState == 1) 
                {
                    _jumpState = 2; //fall after jump
                }

                if (JumpButtonDown() && _jumpState == 2) 
                {
                    _jumpState = 3; //double jump
                    //_rigidBody.velocity = new Vector2(_rigidBody.velocity.y, JumpPower * 0.7f * _jumpChange);
                    _rigidBody.velocity = new Vector2(0, JumpPower * 0.7f * _jumpChange);
                    // GetComponent<AudioSource>().PlayOneShot(JumpSound); //Play a jump sound
                }
                else if (JumpButtonDown() && _rigidBody.velocity.y > -1 && _jumpState == 3) 
                {
                    // _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * 0.3f); 
                    _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y * 0.3f);
                    _jumpState = 4; //fall after double jump
                }

                if (_rigidBody.velocity.y < 0 && _jumpState == 3) 
                {
                    _jumpState = 4; //fall after double jump
                }


                if (_jumpState == 0)
                {
                    if (_jumpState == _lastJumpState)
                    {
                        //do nothing
                    }
                    else if (Speed > 0.5)
                    {
                        GetComponent<Animator>().SetTrigger(_runAnimationName);

                        _lastJumpState = _jumpState;
                    }
                    else //otherwise, play the normal run animation
                    {
                        GetComponent<Animator>().SetTrigger(_runAnimationName);
                        _lastJumpState = _jumpState;
                    }
                }
                else if (_jumpState == 1 || _jumpState == 3)
                {
                    GetComponent<Animator>().SetTrigger(_jumpAnimationName);
                    _lastJumpState = _jumpState;

                }
                else if (_jumpState == 2 || _jumpState == 4)
                {
                    GetComponent<Animator>().SetTrigger(_fallAnimationName);
                    _lastJumpState = _jumpState;

                }
                if (this.transform.position.x < _playerDefaultPosition.x)
                {
                    if (IsStartRecovering == false)
                    {
                        IsStartRecovering = true;
                        //开始计时
                        StartCoroutine(RecoverPlayerPos());

                    }
                }
            }
        }



            
	}

    IEnumerator RecoverPlayerPos()
    {
        yield return new WaitForSeconds(RecoverTimeAfterBlock);
        
        //慢慢开始移动
        //Slow down to 0 in time
      //  var rate = 1.0f / RecoveringTime;
       // var t = 0.0f;
        float x;

        while (this.transform.position.x < _playerDefaultPosition.x)
        {
            x = this.transform.position.x;
            if (IsBlockedByObstacles)
                break;
            Vector3 pos = this.transform.position;
            /*
            Vector3 pos = this.transform.position;
            float xx = pos.x;
            t += Time.deltaTime * rate;
            x = Mathf.Lerp(xx, _playerDefaultPosition.x, t);
            this.transform.position = new Vector3(x, pos.y, pos.z);*/
            this.transform.position = new Vector3(x + RecoveringSpeed, pos.y, pos.z);
            yield return new WaitForEndOfFrame();
        }
        IsStartRecovering = false;
        IsBlockedByObstacles = false;
    }
	//Called when the submarine trigger with something
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.transform.tag == "Collections")
        {
            GameManager.Instance.IncreaseACollection();
            other.transform.parent.gameObject.SetActive(false);
        }
        else if(other.transform.name == "TopEdge")
        {
            GameManager.Instance.LandEdge.GetComponent<Collider2D>().enabled = false;
            //强行向下位移然后播放动画
            transform.position -= new Vector3(0, 0.3f, 0);
            GetComponent<Animator>().SetTrigger("Help");
            _fellOff = true;
        }
	}
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.name == "TopEdge")
        {
            GameManager.Instance.LandEdge.GetComponent<Collider2D>().enabled = true;
            _fellOff = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (_jumpState == 2 || _jumpState == 4)
        {
            _jumpState = 0;
        }

        if(collision.gameObject.tag == "Land")
        {
            if (_fallAtStart)
            {
                _fallAtStart = false;
                GetComponent<Rigidbody2D>().drag = 0;
                GameGenerator.Instance.GenerateObstacles();
            }
        }
        
       
    }




 
    bool JumpButtonDown()
    {
    #if UNITY_ANDROID
        return Input.GetButtonDown("Jump")
             && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        //return Input.GetTouch(0).phase == TouchPhase.Ended;
    #endif
 
    #if UNITY_IPHONE
        return Input.GetTouch(0).phase == TouchPhase.Ended;
    #endif

    #if UNITY_STANDALONE_WIN
        return Input.GetButtonDown("Jump");
    #endif
    }

    //Enables/disables the object with childs based on platform
    void EnableDisable(GameObject what, bool state)
    {
        #if UNITY_3_5
            what.SetActiveRecursively(state);
        #else
            what.SetActive(state);
        #endif
    }

    
	//Enalbe submarine controls
	public void EnableControls()
	{
        controllEnabled = true;
	}
	//Reset submarine status
	public void ResetStatus(bool moveToStart)
	{
		//Stop the coroutines
		StopAllCoroutines();

		//Reset variables
		//speed = 0;
//		crashed = false;
		paused = false;
//		movingUpward = false;
 //       canSink = true;
        _fallAtStart = true;
     //   _fellOff = true;
		firstObstacleGenerated = false;
        _fellOff = false;
        EnableControls();
        _jumpState = 4;

        TotalDistance = 0;
       // Time.timeScale = 1;
        transform.position = _playerDefaultPosition;
        gameObject.SetActive(true);

        GetComponent<Animator>().speed = 1;

        GetComponent<Animator>().SetTrigger("Fall");
        GetComponent<Rigidbody2D>().drag = 18.87f;
        GetComponent<Rigidbody2D>().gravityScale = _gravityScale;
        _fallAtStart = true;
	}
	//Disable submarine controls
	public void DisableControls()
	{
        controllEnabled = false;
	}

    public void OnGameOver()
    {
        DisableControls();
        this.gameObject.SetActive(false);
    }

    public void Pause()
    {
        paused = true;
        DisableControls();
        GetComponent<Animator>().speed = 0;
        _gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void Resume()
    {
        paused = false;
        EnableControls();
        GetComponent<Animator>().speed = 1;
        GetComponent<Rigidbody2D>().gravityScale = _gravityScale;
    }

    public void OnTrap()
    {
       // GetComponent<Animator>().SetTrigger("Help");

    }

    public void OnCursh()
    {
        GetComponent<Animator>().SetTrigger("Crush");
    }
}