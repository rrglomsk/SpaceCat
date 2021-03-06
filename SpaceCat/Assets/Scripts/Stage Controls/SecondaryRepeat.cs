﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecondaryRepeat : MonoBehaviour {

	public GameObject availableSecondary;
	private Camera backgroundCamera;
	private Transform camera;
	public List<GameObject> currentSecondary;
	private float screenWidth;
	public float secondaryLength;
	BackgroundParallax backgroundParallax;

	void Start () {
		float screenHeight = 2.0f * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;
		backgroundCamera = GameObject.Find("Background Camera").camera;
		camera = backgroundCamera.transform;
		backgroundParallax = (BackgroundParallax)camera.GetComponent (typeof(BackgroundParallax));
	}
	
	void Update () {
		GenerateSecondary ();
	}
	
	void AddSecondary(float secondaryEndX)
	{
		GameObject secondary = (GameObject)Instantiate(availableSecondary);
		secondary.layer = 10;
		float secondaryWidth = secondary.transform.localScale.x;
		float secondaryCenter = secondaryEndX + secondaryWidth * 0.5f;
		secondary.transform.position = new Vector3((secondaryEndX + secondaryLength), 0, 13.8f);
		currentSecondary.Add(secondary);
		backgroundParallax.addBackgrounds(secondary.transform);

	}
	
	void GenerateSecondary()
	{
		List<GameObject> secondaryToRemove = new List<GameObject>();
		
		bool addSecondary = true;        
		float catX = transform.position.x;
		float removeSecondaryX = catX - screenWidth;        
		float addSecondaryX = catX + screenWidth;
		float farSecondaryEndX = 0;
		
		foreach(var secondary in currentSecondary)
		{
			float secondaryWidth = secondary.transform.localScale.x;
			float secondaryStartX = secondary.transform.position.x - (secondaryWidth * 0.5f);    
			float secondaryEndX = secondaryStartX + secondaryWidth;                            

			if (secondaryStartX > addSecondaryX)
				addSecondary = false;

			if ((secondaryEndX + 7.0f) < removeSecondaryX)
				secondaryToRemove.Add(secondary);

			farSecondaryEndX = Mathf.Max(farSecondaryEndX, secondaryEndX);
		}

		foreach(var secondary in secondaryToRemove)
		{
			currentSecondary.Remove(secondary);
			backgroundParallax.removeBackgrounds(secondary.transform);
			Destroy(secondary);            
		}

		if (addSecondary)
			AddSecondary(farSecondaryEndX);
	}
}

