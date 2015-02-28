using UnityEngine;
using System.Collections;

public class FakeObstacle : MonoBehaviour {

	private bool isHit = false;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			isHit = true;
		}
	}

	public bool getHit() {
		return isHit;
	}
}
