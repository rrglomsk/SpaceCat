﻿using UnityEngine;
using System.Collections;

public class GravityControl : MonoBehaviour {

	private GameObject spaceCat;
	public Vector3 jumpMovement = new Vector3();
	private Animator animator;
	private Camera controlsCamera;

	
	
	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("cat");
		animator = GameObject.Find ("cat").GetComponent<Animator>();
		controlsCamera = GameObject.Find("Controls Camera").camera;

	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount == 1) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					CheckTouch (Input.GetTouch (0).position, "began");
				} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					CheckTouch (Input.GetTouch (0).position, "ended");
				}
			} else if (Input.touchCount > 1) {
				if (Input.GetTouch (1).phase == TouchPhase.Began) {
					CheckTouch (Input.GetTouch (1).position, "began");
				} else if (Input.GetTouch (1).phase == TouchPhase.Ended) {
					CheckTouch (Input.GetTouch (1).position, "ended");
				}
			}
		}
	}
	
	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = controlsCamera.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit.gameObject.name == "GravityUp" && hit && phase == "began")
		{
			spaceCat.rigidbody2D.gravityScale = -0.09f;
			animator.SetBool("Floating", true);

			
		}
		
		if (hit.gameObject.name == "GravityDown" && hit && phase == "began")
		{
			spaceCat.rigidbody2D.gravityScale = 0.15f;
			animator.SetBool("Floating", false);
		}
	}


}
