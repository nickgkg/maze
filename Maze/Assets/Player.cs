using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float acceleration = 10f;
	private Rigidbody2D playerRigidbody;

	public GameObject splat;

	public float locationGranularity = 0.5f;
	private bool[,] locations;

	private int score = 0;

	public int getScore () {
		return score;
	}

	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		locations = new bool[30, 30];
	}
	
	// Update is called once per frame
	void Update () {
		if (Root.gameRunning) {
			if (Input.GetKey(KeyCode.UpArrow)) {
				playerRigidbody.AddForce(acceleration * new Vector2(0, 1));
			}

			if (Input.GetKey(KeyCode.DownArrow)) {
				playerRigidbody.AddForce(acceleration * new Vector2(0, -1));
			}

			if (Input.GetKey(KeyCode.LeftArrow)) {
				playerRigidbody.AddForce(acceleration * new Vector2(-1, 0));
			}

			if (Input.GetKey(KeyCode.RightArrow)) {
				playerRigidbody.AddForce(acceleration * new Vector2(1, 0));
			}

			transform.up = playerRigidbody.velocity.normalized;
		}

		int i = (int)Mathf.Floor (transform.position.x / locationGranularity) + 15;
		int j = (int)Mathf.Floor (transform.position.y / locationGranularity) + 15;
		if (!locations [i, j]) {
			locations [i, j] = true;
			score++;
			GameObject go = Instantiate (
				splat,
				new Vector3 (transform.position.x, transform.position.y, .1f),
				//TODO add randomness to splat position
				Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))
			) as GameObject;
			Vector3 scale = go.transform.localScale;
			scale.x = 0.2f;
			scale.y = 0.2f;
		}

	}

	void OnCollisionEnter2D(Collision2D col) {
		
	}
}
