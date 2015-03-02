using UnityEngine;
using System.Collections;

public class TriggerFinalHint : MonoBehaviour {

	private GameObject spaceCat;
	private bool finalHint = false;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
	}
	
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			finalHint = true;
		}
	}
	
	public bool getFinalHint() {
		return finalHint;
	}
}