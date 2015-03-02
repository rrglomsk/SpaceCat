using UnityEngine;
using System.Collections;

public class TriggerFloatHint : MonoBehaviour {

	private GameObject spaceCat;
	private bool floatHint = false;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
	}
	
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			floatHint = true;
		}
	}
	
	public bool getFloatHint() {
		return floatHint;
	}
}