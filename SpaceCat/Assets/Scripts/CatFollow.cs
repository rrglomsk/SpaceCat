using UnityEngine;
using System.Collections;

public class CatFollow : MonoBehaviour {

	private GameObject spaceCat;
	
	void Start () {
		spaceCat = GameObject.Find ("Cat");
	}

	void FixedUpdate () {
		transform.position = spaceCat.transform.position;
	}
}
