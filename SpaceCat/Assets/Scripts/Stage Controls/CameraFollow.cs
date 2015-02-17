using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private float distanceToTarget;
	public Transform target;


	//Come back to this
	void Start () {
		distanceToTarget = transform.position.x - target.transform.position.x;
	}
	
	void Update () {
		float targetObjectX = target.transform.position.x;
		
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX + distanceToTarget;
		transform.position = newCameraPosition;
	}
}
