using UnityEngine;
using System.Collections;

public class SmoothBackgroundFollow : MonoBehaviour {

	private float distanceToTarget;
	public Transform target;

	void Start () {
		distanceToTarget = transform.position.x - target.transform.position.x;
	}
	void FixedUpdate () {
		float targetObjectX = target.transform.position.x;
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX + distanceToTarget;
		transform.position = Vector3.Lerp (transform.position, newCameraPosition, Time.fixedDeltaTime * 3f);
		//transform.position = newCameraPosition;
	}
}
