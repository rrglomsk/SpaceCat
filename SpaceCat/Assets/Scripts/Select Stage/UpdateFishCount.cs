using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateFishCount : MonoBehaviour {

	Text fishScore;


	void Start () {
		fishScore = gameObject.GetComponent<Text> ();
		int fishCount = PlayerPrefs.GetInt("fishCount");
		fishScore.text = "x" + fishCount;
	}
	

	void Update () {
		int fishCount = PlayerPrefs.GetInt("fishCount");
		fishScore.text = "x" + fishCount;
	}
}
