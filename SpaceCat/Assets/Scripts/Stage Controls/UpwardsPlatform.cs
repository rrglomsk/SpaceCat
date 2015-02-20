using UnityEngine;
using System.Collections;

public class UpwardsPlatform : MonoBehaviour {

	public Vector3 movingSpeed = new Vector3();
	private float distanceTraveled = 0;
	public float travelHeight;
	private Vector3 originalHeight;

	// Use this for initialization
	void Start () {
		originalHeight = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y < travelHeight) {
			transform.position += movingSpeed;
		} else {
			transform.position = originalHeight;
		}
	
	}
}
