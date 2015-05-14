using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour 
{
	public List<GameObject> elements		= new List<GameObject>();		//A list that holds the obstacle childs
    public Vector3 DefaultPosition
    {
        get
        {
            return _defaultPosition;
        }
    }
    Vector3 _defaultPosition;
	//Called at the beginning of the game
	void Start()
	{
        
		//Loop through children
		foreach (Transform child in transform)
		{
			//If the child is not a triggerer
			if (child.name != "SpawnTriggerer" && child.name != "ResetTriggerer")
			{
				//Add it to the elements list, and deactivate it
	            elements.Add(child.gameObject);
               // child.gameObject.SetActive(false);
			}
		}
        _defaultPosition = this.transform.localPosition;
        gameObject.SetActive(false);
	}
    //Enables/disables the object with childs based on platform
    void EnableDisable(GameObject what, bool activate)
    {
        #if UNITY_3_5
            what.SetActiveRecursively(activate);
        #else
            what.SetActive(activate);
        #endif
    }
	//Called when the obstacle is reseting
	public void DeactivateChild()
	{
        foreach (GameObject child in elements)
        {
            child.SetActive(false);
        }
        //gameObject.SetActive(false);
	}
	//Called when the obstacle is spawned
	public void ActivateChild()
	{

        foreach (GameObject child in elements)
        {
            child.SetActive(true);
        }
       gameObject.SetActive(true);
	}
}
