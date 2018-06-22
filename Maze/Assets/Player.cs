using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Vector2 acceleration = new Vector2(10f, 10f);
	private Rigidbody2D playerRigidbody;


	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
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
	}

	void OnCollisionEnter2D(Collision2D col) {
		Debug.Log("COLLISION");
	}
}
