using UnityEngine;
using System.Collections;

public class HorizontalPlatformMove : MonoBehaviour {

	public Vector3 movingSpeed = new Vector3();
	private float distanceTraveled = 0;
	public float distanceToTravel;
	private bool movingRight = true;

	void Start () {
	
	}

	void Update () {
	
		if (distanceTraveled < distanceToTravel && movingRight) {
			transform.position += movingSpeed;
			distanceTraveled += movingSpeed.x;
		} else if (distanceTraveled < distanceToTravel && !movingRight) {
			transform.position -= movingSpeed;
			distanceTraveled += movingSpeed.x;
		} else {
			distanceTraveled = 0;
			movingRight = (movingRight) ? false : true;
		}


	}

}
