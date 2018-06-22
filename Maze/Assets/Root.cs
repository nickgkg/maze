using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Root : MonoBehaviour {
	// use this for global code/timer stuff - nick
	private float timeRemaining = 90f;

	public static bool gameRunning = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			gameRunning = true;
		}

		if (gameRunning) {
			timeRemaining -= Time.deltaTime;
		}
	}
	
	void BeginGame() {
		timeRemaining = 90f;
		gameRunning = true;
	}
}
