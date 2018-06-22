using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Root : MonoBehaviour {
	// use this for global code/timer stuff - nick
	private float timeRemaining = 90f;

	public static bool gameRunning = false;

	public static Text timeRemainingLabel;

	public static Text timeToShuffleLabel;

	public Button startGameButton;

	// Use this for initialization
	void Start () {
		timeRemainingLabel = GameObject.Find("TimeRemainingLabel").GetComponent<Text>();
		timeToShuffleLabel = GameObject.Find("TimeToShuffleLabel").GetComponent<Text>();

		startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();

		startGameButton.onClick.AddListener(OnStartButtonClicked);
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

	void OnStartButtonClicked() {
		if (!gameRunning) {
			BeginGame();
		}
	}
}
