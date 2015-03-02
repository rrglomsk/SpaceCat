using UnityEngine;
using System.Collections;

public class UpwardsPlatform : MonoBehaviour {

	public Vector3 movingSpeed = new Vector3();
	private float distanceTraveled = 0;
	public float travelHeight;
	private Vector3 originalHeight;


	void Start () {
		originalHeight = transform.position;
	}
	

	void Update () {

		if (transform.position.y < travelHeight) {
			transform.position += movingSpeed;
		} else {
			transform.position = originalHeight;
		}
	
	}
}
