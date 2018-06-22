using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Root : MonoBehaviour {
	// use this for global code/timer stuff - nick
	public float gameTime = 90f;
	public static float timeRemaining;

	public static bool gameRunning = false;

	public static Text timeRemainingLabel;

	public static Text timeToShuffleLabel;

	public static RectTransform gameTimer;
	public static RectTransform shuffleTimer;

	public Button startGameButton;

	// Use this for initialization
	void Start () {
		//timeRemainingLabel = GameObject.Find("TimeRemainingLabel").GetComponent<Text>();
		//timeToShuffleLabel = GameObject.Find("TimeToShuffleLabel").GetComponent<Text>();
		gameTimer = GameObject.Find("GameTimer").GetComponent<RectTransform>();
		shuffleTimer = GameObject.Find("ShuffleTimer").GetComponent<RectTransform>();

		startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();

		startGameButton.onClick.AddListener(OnStartButtonClicked);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			timeRemaining -= Time.deltaTime;

			Vector2 vec = gameTimer.anchorMax;
			vec.x = timeRemaining / gameTime;
			gameTimer.anchorMax = vec;

			//timeRemainingLabel.text = "Time Remaining: " + Convert.ToInt32(timeRemaining).ToString();

			if (timeRemaining <= 0) {
				EndGame();
			}
		}
	}
	
	void BeginGame() {
		timeRemaining = gameTime;
		gameRunning = true;
	}

	void EndGame() {
		gameRunning = false;
	}

	void OnStartButtonClicked() {
		if (!gameRunning) {
			BeginGame();
		}
	}
}
