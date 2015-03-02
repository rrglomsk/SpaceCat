using UnityEngine;
using System.Collections;

public class TriggerObstacleHintEnd : MonoBehaviour {

	private GameObject spaceCat;
	private bool endObstacleHint = false;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
	}
	
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			endObstacleHint = true;
		}
	}
	
	public bool getObstacleEndHint() {
		return endObstacleHint;
	}
}