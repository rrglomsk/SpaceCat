using UnityEngine;
using System.Collections;

public class TriggerFishHint : MonoBehaviour {

	private GameObject spaceCat;
	private bool fishHint = false;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
	}
	
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			fishHint = true;
		}
	}
	
	public bool getFishHint() {
		return fishHint;
	}
}
