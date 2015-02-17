using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundRepeat : MonoBehaviour {

	public GameObject availableGround;
	
	public List<GameObject> currentGround;
	
	private float screenWidth;

	// Use this for initialization
	void Start () {
		float screenHeight = 2.0f * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		GenerateGround ();
	}

	void AddGround(float groundEndX)
	{
		int randomGroundIndex = 0; //Random.Range(0, availableGround.Length);
		GameObject ground = (GameObject)Instantiate(availableGround);
		float groundWidth = ground.transform.localScale.x;
		float groundCenter = groundEndX + groundWidth * 0.5f;
		ground.transform.position = new Vector3((float)(groundEndX + 13.85), (float)(-3.7), 0);
		currentGround.Add(ground);         
	}

	void GenerateGround()
	{
		List<GameObject> groundToRemove = new List<GameObject>();

		bool addGround = true;        
		float catX = transform.position.x;
		float removeGroundX = catX - screenWidth;        
		float addGroundX = catX + screenWidth;
		float farGroundEndX = 0;
		
		foreach(var ground in currentGround)
		{
			//7
			float groundWidth = ground.transform.localScale.x;
			float groundStartX = ground.transform.position.x - (groundWidth * 0.5f);    
			float groundEndX = groundStartX + groundWidth;                            
			
			//8
			if (groundStartX > addGroundX)
				addGround = false;
			
			//9
			if (groundEndX < removeGroundX)
				groundToRemove.Add(ground);
			
			//10
			farGroundEndX = Mathf.Max(farGroundEndX, groundEndX);
		}
		
		//11
		foreach(var ground in groundToRemove)
		{
			currentGround.Remove(ground);
			Destroy(ground);            
		}
		
		//12
		if (addGround)
			AddGround(farGroundEndX);
	}
}
