﻿using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerManager.Instance.OnTrap();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
