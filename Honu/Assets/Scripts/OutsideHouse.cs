﻿using UnityEngine;
using System.Collections;

public class OutsideHouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Application.LoadLevel(1);
    }
}
