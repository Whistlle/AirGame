using UnityEngine;
using System.Collections;
using Sov.AVGPart;

public class SceneCtrl : MonoBehaviour 
{
    public string ScenarioName;
    Scene s = null;
	// Use this for initialization
    void Awake()
    {
       ScenarioName = "story";
      //  ScenarioManager.Instance.RunLoadedScenario(ScenarioName);
        s = new Scene("story1");
        s.Name = name;
        s.LoadScript();
        
    }
	void Start () 
    {
        ScriptEngine.Instance.Run(s);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
