using UnityEngine;
using System.Collections;

public class ScrollingImage : MonoBehaviour {

    public Collider2D LeftEdge;
    public Collider2D RigthEdge;

    public float LeftEdgePosX
    {
        get
        {
            return LeftEdge.transform.position.x;
        }
    }
    public float Width
    {
        get
        {
            return RightEdgePosX - LeftEdgePosX;
        }
    }
    public float RightEdgePosX
    {
        get
        {
            return RigthEdge.transform.position.x;
        }
    }
    public bool IsOutOfCamera()
    {
        return false;
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
