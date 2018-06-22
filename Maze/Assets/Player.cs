using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private float xAcceleration = 0.01f;
	private float yAcceleration = 0.01f;
	public Vector2 playerSpeed = Vector2.zero;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			playerSpeed.y += yAcceleration;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			playerSpeed.y -= yAcceleration;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			playerSpeed.x -= xAcceleration;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			playerSpeed.x += xAcceleration;
		}

		updatePosition();
	}

	void updatePosition() {
		transform.Translate(playerSpeed);
	}

	void OnCollisionEnter2D(Collision2D col) {
		Debug.Log("COLLISION");
	}
}
