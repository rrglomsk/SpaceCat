using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {

	//RETURN TO THIS FILE?
	public Transform [] backgrounds;    
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
		parallaxScales = new float[backgrounds.Length]; 
		
		for (int i = 0; i < backgrounds.Length; i++)  
		{
			parallaxScales[i] = backgrounds[i].position.z * -1;		
		}
	}
	
	void Update () 
	{
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCameraPos.x - camera.position.x) * parallaxScales[i];

			float previousBackgroundPos = backgrounds[i].position.x;
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;   
			//print (backgroundTargetPosX - previousBackgroundPos);

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		
		previousCameraPos = camera.position; 
	}
}
