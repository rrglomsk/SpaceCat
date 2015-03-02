using UnityEngine;
using System.Collections;

public class CheckFloating : MonoBehaviour {

	private GameObject spaceCat;
	private bool hasFloat = false;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			hasFloat = true;
		}
	}

	public bool getFloated() {
		return hasFloat;
	}
}
