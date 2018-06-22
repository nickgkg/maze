using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour {
	// these should be odd if we want 0,0 to be a space instead of a wall
	int width = 15;
	int height = 15;

	public GameObject wall;

	private class MazePiece {
		public bool created = false;
		public bool right = false;
		public bool bottom = false;
	}

	private MazePiece[,] maze;

	// Use this for initialization
	void Start () {
		maze = new MazePiece[width,height];
		CreateMaze ();
		PrintMaze ();
	}
	
	// Update is called once per frame
	void Update () {
		
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

			int random = Random.Range (0, options.Count);
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

	void generateWalls() {
		for (int i = -1; i < width; i++) {
			for (int j = -1; j < height; j++) {
				float x = (i - width * .5f) * .5f + .25f;
				float y = (j - height * .5f) * .5f + .25f;
				if (j != -1 && (i == -1 || i == width-1 || maze[i,j].right)) {
					GameObject go = Instantiate (
						wall,
						new Vector3 (x + .25f, y, 0),
						Quaternion.identity
					) as GameObject;
					go.transform.parent=transform;
				}
				if (i != -1 && (j == -1 || j == height-1 || maze[i,j].bottom)) {
					GameObject go = Instantiate (
						wall,
						new Vector3 (x, y + .25f, 0),
						Quaternion.Euler(new Vector3(0, 0, 90))
					) as GameObject;
					go.transform.parent=transform;
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
		print (line);
	}

	void ShuffleMaze() {
	
	}

	void HideMaze() {
		
	}
}
