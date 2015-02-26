using UnityEngine;
using System.Collections;

public class VerticalWallMovement : MonoBehaviour {

	public Vector3 movingSpeed = new Vector3();
	private float distanceTraveled = 0;
	public float distanceToTravel;
	public bool movingDown;
	
	void Start () {
	
	}

	void Update () {
		if (distanceTraveled < distanceToTravel && movingDown) {
			transform.position += movingSpeed;
			distanceTraveled += movingSpeed.y;
		} else if (distanceTraveled < distanceToTravel && !movingDown) {
			transform.position -= movingSpeed;
			distanceTraveled += movingSpeed.y;
		} else {
			distanceTraveled = 0;
			movingDown = (movingDown) ? false : true;
		}
	}
}
