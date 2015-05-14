using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {

    public void EnterScene()
    {
        GetComponent<Animator>().SetTrigger("Enter");
    }

    public void DropPlayerAndStart(float dt)
    {
        GameManager.Instance.DropPlayerAndStart();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
