using UnityEngine;
using System.Collections;

public class CollectionTrigger : MonoBehaviour {

    public GameObject face;

	// Use this for initialization
	void Start () {
        face.SetActive(false);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            face.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            face.SetActive(false);
        }
    }
}
