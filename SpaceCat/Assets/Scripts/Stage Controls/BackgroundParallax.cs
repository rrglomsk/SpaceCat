using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundParallax : MonoBehaviour {

	//RETURN TO THIS FILE?
	public List<Transform> backgrounds;    
	private float [] parallaxScales;   
	public float smoothing;     
	
	private Transform camera;             
	private Vector3 previousCameraPos;    
	
	
	void Awake () 
		
	{
		camera = Camera.main.transform; 
	}
	
	void Start ()
		
	{
		previousCameraPos = camera.position;
		parallaxScales = new float[backgrounds.Count]; 
		
		for (int i = 0; i < backgrounds.Count; i++)  
		{
			parallaxScales[i] = backgrounds[i].position.z * -1;		
		}
	}
	
	void Update () 
	{
		for (int i = 0; i < backgrounds.Count; i++) {
			float parallax = (previousCameraPos.x - camera.position.x) * parallaxScales[i];

			float previousBackgroundPos = backgrounds[i].position.x;
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;   
			//print (backgroundTargetPosX - previousBackgroundPos);

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.smoothDeltaTime);
		}
		
		previousCameraPos = camera.position;
	}

	public void addBackgrounds(Transform newBackground) {
		backgrounds.Add (newBackground);
		parallaxScales = new float[backgrounds.Count]; 
		for (int i = 0; i < backgrounds.Count; i++) {
			parallaxScales[i] = backgrounds[i].position.z * -1;		
		}
	}

	public void removeBackgrounds(Transform removedBackground) {
		backgrounds.Remove (removedBackground);
		for (int i = 0; i < backgrounds.Count; i++) {
			parallaxScales[i] = backgrounds[i].position.z * -1;		
		}
	}
}
