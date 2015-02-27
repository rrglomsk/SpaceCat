using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateFishCount : MonoBehaviour {

	Text fishScore;

	// Use this for initialization
	void Start () {
		fishScore = gameObject.GetComponent<Text> ();
		int fishCount = PlayerPrefs.GetInt("fishCount");
		fishScore.text = "x" + fishCount;
	}
	
	// Update is called once per frame
	void Update () {
		int fishCount = PlayerPrefs.GetInt("fishCount");
		fishScore.text = "x" + fishCount;
	}
}
