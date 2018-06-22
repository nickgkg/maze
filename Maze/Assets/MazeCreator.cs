using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour {
	int width = 16;
	int height = 16;

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

	void ShuffleMaze() {
	
	}

	void HideMaze() {
		
	}
}
