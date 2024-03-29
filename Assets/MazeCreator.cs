﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeCreator : MonoBehaviour {
	// these should be odd if we want 0,0 to be a space instead of a wall
	public int width = 15;
	public int height = 15;
	public float gridSize = 0.5f;

	public float shuffleTime = 10f;
	float timeUntilShuffle;

	public List<float> weights;

	public List<GameObject> wallStraight;
	public List<GameObject> wallCorner;
	public List<GameObject> wall1;
	public List<GameObject> wall3;
	public List<GameObject> wall4;

	private class MazePiece {
		public bool created = false;
		public bool right = false;
		public bool bottom = false;
	}

	private MazePiece[,] maze;

	// Use this for initialization
	void Start () {
		maze = new MazePiece[width,height];
		timeUntilShuffle = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Root.gameRunning) {
			timeUntilShuffle -= Time.deltaTime;

			if (timeUntilShuffle <= 0) {
				timeUntilShuffle = 10;

				ShuffleMaze();
			}

			//Root.timeToShuffleLabel.text = "Shuffle Timer: " + Convert.ToInt32(timeUntilShuffle).ToString();
			Vector2 vec = Root.shuffleTimer.anchorMax;
			vec.x = timeUntilShuffle / shuffleTime;
			Root.shuffleTimer.anchorMax = vec;
		}
	}

	void CreateMaze() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				maze [i, j] = new MazePiece ();
				maze[i,j].created = false;
				maze[i,j].right = true;
				maze[i,j].bottom = true;
			}
		}
		generateRecur (width / 2, height / 2);

		generateWalls ();
	}

	void generateRecur (int i, int j) {
		maze [i, j].created = true;
		while(true) {
			List<int> options = new List<int>();
			if (i > 0 && !maze[i - 1, j].created) {
				options.Add (0);
			}
			if (i < width - 1 && !maze [i + 1, j].created) {
				options.Add (1);
			}
			if (j > 0 && !maze[i, j - 1].created) {
				options.Add (2);
			}
			if (j < height - 1 && !maze [i, j + 1].created) {
				options.Add (3);
			}

			if (options.Count == 0) {
				break;
			}

			int random = UnityEngine.Random.Range (0, options.Count);
			int choice = options[random];

			switch (choice) {
			case 0:
				maze [i - 1, j].right = false;
				generateRecur (i - 1, j);
				break;
			case 1:
				maze [i, j].right = false;
				generateRecur (i + 1, j);
				break;
			case 2:
				maze [i, j - 1].bottom = false;
				generateRecur (i, j - 1);
				break;
			case 3:
				maze [i, j].bottom = false;
				generateRecur (i, j + 1);
				break;
			}
		}
	}

	bool hasRightWall(int i, int j) {
		return j != -1 && j != height && (i == -1 || i == width - 1 || maze [i, j].right);
	}

	bool hasBottomWall(int i, int j) {
		return i != -1 && i != width && (j == -1 || j == height - 1 || maze [i, j].bottom);
	}

	int getRandomWeight() {
		float weightSum = 0f;
		for (int i = 0; i < weights.Count; ++i)
		{
			weightSum += weights[i];
		}

		// Step through all the possibilities, one by one, checking to see if each one is selected.
		int index = 0;
		int lastIndex = weights.Count - 1;
		float random = UnityEngine.Random.Range (0, weightSum);
		float weightSum2 = 0f;
		while (index < lastIndex)
		{
			weightSum2 += weights [index];
			if (random < weightSum2) {
				return index;
			}

			weightSum -= weights[index++];
		}

		return index;
	}

	void addWall(List<GameObject> wallTypes, float x, float y, float angle) {
		int rand = getRandomWeight();
		GameObject wallType = wallTypes [rand];
		GameObject go = Instantiate (
			wallType,
			new Vector3 (x + gridSize * 1.5f, y + gridSize * 1.5f - 0.8f, 0),
			Quaternion.Euler(new Vector3(0, 0, angle))
		) as GameObject;
		go.transform.parent=transform;
		Vector3 scale = go.transform.localScale;
		scale.x = 0.2f * gridSize;
		scale.y = 0.2f * gridSize;
		go.transform.localScale = scale;
	}

	void generateWalls() {
		for (int i = -1; i < width; i++) {
			for (int j = -1; j < height; j++) {
				float x = (i - (width + 1) * .5f) * gridSize;
				float y = (j - (height + 1) * .5f) * gridSize;
				bool top = hasRightWall (i, j);
				bool bottom = hasRightWall (i, j + 1);
				bool left = hasBottomWall (i, j);
				bool right = hasBottomWall (i + 1, j);

				//4 way
				if (top && bottom && left && right) {
					addWall (wall4, x, y, 0);
				}

				//3 way
				else if (top && bottom && left) {
					addWall (wall3, x, y, 270);
				}
				else if (top && left && right) {
					addWall (wall3, x, y, 0);
				}
				else if (bottom && left && right) {
					addWall (wall3, x, y, 180);
				}
				else if (top && bottom && right) {
					addWall (wall3, x, y, 90);
				}

				//2 way
				else if (top && bottom) {
					addWall (wallStraight, x, y , 0);
				}
				else if (left && right) {
					addWall (wallStraight, x, y, 90);
				}
				else if (top && right) {
					addWall (wallCorner, x, y, 0);
				}
				else if (bottom && right) {
					addWall (wallCorner, x, y, 90);
				}
				else if (bottom && left) {
					addWall (wallCorner, x, y, 180);
				}
				else if (top && left) {
					addWall (wallCorner, x, y, 270);
				}

				//1 way
				else if (top) {
					addWall (wall1, x, y, 0);
				}
				else if (right) {
					addWall (wall1, x, y, 90);
				}
				else if (bottom) {
					addWall (wall1, x, y, 180);
				}
				else if (left) {
					addWall (wall1, x, y, 270);
				}

			}
		}
	}

	void PrintMaze() {
		string line = "";
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (maze [i, j].bottom) {
					line += "_";
				} else {
					line += " ";
				}
				if (maze[i,j].right) {
					line += "|";
				} else {
					line += " ";
				}
			}
			line += "\n";
		}
		// print (line);
	}

	void ShuffleMaze() {
		ClearMaze();
		CreateMaze();
		PrintMaze();
	}

	void HideMaze() {

	}

	void ClearMaze() {
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
