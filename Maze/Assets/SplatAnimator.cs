using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatAnimator : MonoBehaviour {

	private SpriteRenderer spr;
	public List<Sprite> sprites;

	public float lifespan = 0.5f;
	public float lifeRemaining;

	// Use this for initialization
	void Start () {

		spr = GetComponent<SpriteRenderer> ();
		lifeRemaining = lifespan;
	}
	
	// Update is called once per frame
	void Update () {
		lifeRemaining -= Time.deltaTime;
		int index = (int)(lifeRemaining / lifespan * sprites.Count);
		index = sprites.Count - 1 - index;
		if (index < sprites.Count) {
			spr.sprite = sprites [index];
		}
	}
}
