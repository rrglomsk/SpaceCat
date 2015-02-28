using UnityEngine;
using System.Collections;

public class TriggerObstacleFishEnd : MonoBehaviour {
	
	private GameObject spaceCat;
	private bool endFishHint = false;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
	}
	
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			endFishHint = true;
		}
	}
	
	public bool getFishEndHint() {
		return endFishHint;
	}
}