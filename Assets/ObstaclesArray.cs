using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObstaclesArray : MonoBehaviour {

    public List<GameObject> List;
	// Use this for initialization
	void Start () {
	   foreach(Transform t in transform)
       {
           List.Add(t.gameObject);
       }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
