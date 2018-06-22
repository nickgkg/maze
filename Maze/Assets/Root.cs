using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Root : MonoBehaviour {
	// use this for global code/timer stuff - nick
	private float timeRemaining = 90f;

	public static bool gameRunning = false;

	public Text timeRemainingLabel;

	// Use this for initialization
	void Start () {
		timeRemainingLabel = GameObject.Find("TimeRemainingLabel").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			gameRunning = true;
		}

		if (gameRunning) {
			timeRemaining -= Time.deltaTime;

			timeRemainingLabel.text = "Time Remaining: " + Convert.ToInt32(timeRemaining).ToString();
		}
	}
	
	void BeginGame() {
		timeRemaining = 90f;
		gameRunning = true;
	}
}
