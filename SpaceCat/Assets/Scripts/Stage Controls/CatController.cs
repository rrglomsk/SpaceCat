﻿using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	public Transform groundCheckTransform;
	private bool grounded;
	private int fishCount = 0;
	public LayerMask groundCheckLayer;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateGrounded();
	}

	void UpdateGrounded() {
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.1f, groundCheckLayer);
		animator.SetBool ("Grounded", grounded);
	}

	public void addFish(){
		fishCount += 1; 
	}

	public int getFish() {
		return fishCount;
	}

}
