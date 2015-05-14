using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameGenerator: MonoBehaviour
{
    public GameObject player;

    //Starting scroll speed
    public float ScrollSpeed            = 0f;
    public float BuildingScrollSpeed        = 0.3f;
    //Maximum scroll speed
    public float MaxScrollSpeed             = 0.7f;

    //Maximum scroll speed at thid distance
    public float MaxScrollSpeedDist         = 1500;

    public ScrollController Land;
    public ScrollController Building;

    public List<GameObject> Obstacles;
    public ObstaclesArray ObstaclesList;
    static GameGenerator _gameGeneratorInstance;


    public List<GameObject> activeElements = new List<GameObject>();

    Vector2 scrolling = new Vector2(0, 0);

    //The default scrolling speed
    float defaultScroll;

						
    float scrollAtCrash;

    //Generation switct
    bool canGenerate = false;
			
    bool paused = true;

    bool canModifySpeed = true;

    Vector3 _obstaclesLocalPositionAtStart;

    
    public static GameGenerator Instance
    {
        get
        {
            if (!_gameGeneratorInstance)
            {
                Debug.Log("GameGenerator do not instance!");
            }
            return _gameGeneratorInstance;
        }
    }
    void Awake()
    {
        //instance
        _gameGeneratorInstance = this;
    }
    // Use this for initialization
    void Start()
    {
        Obstacles = ObstaclesList.List;
        ScrollSpeed = PlayerManager.Instance.Speed;
        //Sets the starting values
        defaultScroll = PlayerManager.Instance.MinSpeed;

      //  if (Obstacles[0])
        //    _obstaclesLocalPositionAtStart = Obstacles[0].transform.localPosition;
                                        
    }

    public enum Layer
    {
        FarLayer,
        NearLayer
    }

    // Update is called once per frame
    void Update()
    {
        //If the generation is enabled, and the game is not paused
        if (canGenerate && !paused)
        {
            ScrollLevel();
        }

        //If not paused
        if (!paused)
        {
            //Increase the distance, and notify the mission manager
          //  _distance += ScrollSpeed * Time.deltaTime * 25;
        }
    }

    public float ScorllOffset = 10;
    void ScrollLevel()
    {
        //If can modify speed
     //   if (canModifySpeed)
            //Calculate layer scrolling speed
     //       ScrollSpeed = defaultScroll + (((MaxScrollSpeedDist - (MaxScrollSpeedDist - _distance)) / MaxScrollSpeedDist) * (MaxScrollSpeed - defaultScroll));
        ScrollSpeed = PlayerManager.Instance.Speed;
        //Apply scroll speed to scroll vector
        scrolling.x = ScrollSpeed;
  
        //Scroll the sand
        Land.Scroll(ScrollSpeed);
       // Land.material.mainTextureOffset += scrolling * Time.deltaTime;

        //Scroll the elements in the list activeElements, with a speed maching their layer
        for (int i = 0; i < activeElements.Count; i++)
        {
            switch (activeElements[i].tag)
            {
                case "Obstacles":
                    activeElements[i].transform.position -= Vector3.right * ScrollSpeed * Time.deltaTime;
                break;
            }

        }
       // scrolling.x = BuildingScrollSpeed;
        //Scroll the building
        Building.Scroll(BuildingScrollSpeed);
        //Buildings.material.mainTextureOffset += scrolling * Time.deltaTime;
    }

    //Enables/disables the object with childs based on platform
    void EnableDisable(GameObject what, bool state)
    {
        what.SetActive(state);
    }

    //Clear the level, and empty the active elements list
    void ClearMap()
    {
        //Stop all coroutines
        StopAllCoroutines();

        //Go while there is an element in activeElements
        while (activeElements.Count > 0)
        {
            //Reset and remove the element based on their layer
            switch (activeElements[0].tag)
            {
                case "Obstacles":
                Obstacles.Add(activeElements[0]);
                activeElements[0].transform.localPosition = DefaultObstaclesLocalPosition(activeElements[0]);
                activeElements.Remove(activeElements[0]);
                break;
            }
        }
    }
    Vector3 DefaultObstaclesLocalPosition(GameObject go)
    {
        return go.GetComponent<Obstacles>().DefaultPosition;
    }
    //Generate an obstacle element
    public void GenerateObstacles()
    {
        //Get a random element from the obstacles
        int n = Random.Range(0, Obstacles.Count);
        GameObject go = (GameObject)Obstacles[n];

   
        Obstacles.Remove(go);
        activeElements.Add(go);

        //Activate the object
        go.GetComponent<Obstacles>().ActivateChild();
        //go.SetActive(true);

    }
    //Randomize the order of the obstacles in the obstacles list
    void RandomizeObstacles()
    {
        //Get the number of obstacles in the array
        int n = Obstacles.Count;
        GameObject temp;
        //Randomize them
        while (n >= 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            temp = (GameObject)Obstacles[k];
            Obstacles[k] = Obstacles[n];
            Obstacles[n] = temp;
        }
    }

    //Disable, remove and reset an object
    public void SleepGameObject(GameObject go)
    {
        //Disable and reset an object based on it's layer
        switch (go.tag)
        {
            case "Obstacles":
            activeElements.Remove(go);
            Obstacles.Add(go);
            go.transform.localPosition = go.GetComponent<Obstacles>().DefaultPosition;
            break;
        }

        if (go.tag != "Obstacles")
            EnableDisable(go, false);
    }

    //Restart the level generator
    public void Restart(bool startToScroll)
    {
        //Clear the map, and reset the hangar
        ClearMap();
      
        canModifySpeed = true;

        //Reset distance and scroll speed
     //   _distance = 0;
        ScrollSpeed = defaultScroll;

        StartCoroutine(StartToGenerate(1.25f, 3));
    }

    //Start the level generation system
    public IEnumerator StartToGenerate(float waitTime, float obstacleWaitTime)
    {
        //Randomize the obstalce list
        RandomizeObstacles();

        canGenerate = true;
        paused = false;

        //Wait for waitTime
        double waited = 0;
        while (waited <= waitTime)
        {
            //If the game is not paused, increase waited time
            if (!paused)
                waited += Time.deltaTime;
            //Wait for the end of the frame
            yield return 0;
        }
    }
    
    public void InitGeneration()
    {
         //Randomize the obstalce list
        RandomizeObstacles();
        
        //Enable generation
        canGenerate = true;
        paused = false;
    }
    //Stop scrolling speed after a crash
    public IEnumerator StopScrollingAndShowEnd(float time)
    {
        //Disable speed modification
        canModifySpeed = false;

        //Set startValue and scrollAtCrash
        float startValue = ScrollSpeed;
        scrollAtCrash = ScrollSpeed;

        //Slow down to 0 in time
        var rate = 1.0f / time;
        var t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            ScrollSpeed = Mathf.Lerp(startValue, 0, t);
            yield return new WaitForEndOfFrame();
        }

        //Disable generation
        canGenerate = false;
        paused = true;

        //Wait for a second
        yield return new WaitForSeconds(1);

        //show finish menu
        GUIManager.Instance.OnGameOver();
    }


    //Contiune to scroll the level
    public void ContinueScrolling()
    {
        //Reset scroll speed to before crash
        ScrollSpeed = scrollAtCrash;


        paused = false;
        canGenerate = true;
        canModifySpeed = true;
    }

    //Pause the level generator
    public void Pause()
    {
        //Pause the generator
        canGenerate = false;
        paused = true;

    }

    //Resume the level generator
    public void Resume()
    {
        //Resume the generator
        canGenerate = true;
        paused = false;

    }
}
