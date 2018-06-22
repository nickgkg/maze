using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour {

	public List<UnityEngine.UI.Text> digits;

	private Player player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();

	}
	
	// Update is called once per frame
	void Update () {
		int score = player.getScore () * 1000;
		for (int i = 0; i < digits.Count; ++i) {
			string s = "" + (score % 10);
			digits [i].text = s;
			score /= 10;
		}
	}
}
