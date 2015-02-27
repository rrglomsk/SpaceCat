﻿using UnityEngine;
using System.Collections;

public class EndTutorial : MonoBehaviour {

	private GameObject spaceCat;
	CatController catController;
	Animator animator;
	
	
	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("Cat");
		catController = (CatController) spaceCat.GetComponent (typeof(CatController));
		//animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			//animator.SetBool("Collected", true);
			DestroyObject (gameObject, 2.0f);
			int fishCount = PlayerPrefs.GetInt("fishCount", 0);
			fishCount += catController.getFish ();
			PlayerPrefs.SetInt("fishCount", fishCount);
			PlayerPrefs.SetInt ("tutorialComplete", 1);
			PlayerPrefs.Save();
		}
	}
}
