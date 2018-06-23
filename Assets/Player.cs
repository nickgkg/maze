using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float acceleration = 10f;
	private Rigidbody2D playerRigidbody;

	public GameObject splat;
	private GameObject redFlash;

	public float locationGranularity = 0.5f;
	private static bool[,] locations;

	private static int score;

	public static int getScore () {
		return score * 1000;
	}

	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
		redFlash = GameObject.Find("RedFlash");
		redFlash.SetActive (false);

		Player.reset ();
	}

	public static void reset(){
		locations = new bool[30, 30];
		score = -1;
	}

	private float damageCooldown = 0;
	public float damageResetCooldown = 0.3f;
	public float damageFlashTime = 0.2f;
	public float damageAmount = 5f;
	
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
				new Vector3 (transform.position.x, transform.position.y, 0f),
				//TODO add randomness to splat position
				Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))
			) as GameObject;
			Vector3 scale = go.transform.localScale;
			scale.x = 0.2f;
			scale.y = 0.2f;
		}
		damageCooldown -= Time.deltaTime;
		if (damageCooldown < damageResetCooldown - damageFlashTime) {
			redFlash.SetActive (false);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.sharedMaterial.name == "Jellyfish") {
			if (damageCooldown < 0) {
				Root.timeRemaining -= damageAmount;
				damageCooldown = damageResetCooldown;
				redFlash.SetActive (true);
			}
		}
	}
}
