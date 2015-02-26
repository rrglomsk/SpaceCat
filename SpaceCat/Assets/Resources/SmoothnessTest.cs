//SmoothMovementTest

using UnityEngine;
using System.Collections;

public class SmoothnessTest : MonoBehaviour {
	
	public Transform cube;
	public Transform cubeRigid;
	private GameObject[] staticCubes;
	private Transform thisTransform;
	private float speed = 8F;
	private float fixedStepSlider = 50F;
	private Vector3 orPos;
	private bool followRigidbody = false;
	private bool followNormal = true;
	private bool fromLeft = true;
	private bool rigidbodyMovement = true;
	private bool rigidbodyRotating = false;
	private bool normalMovement = true;
	private bool manualMovement = false;
	private bool extrapolateNormal = false;
	private bool interpolateNormal = false;
	
	private bool unityExtrapolation = false;
	private bool unityInterpolation = false;
			
	private bool normalMovementSmooth = false;
	private float frameRateSlider = 60F;
		
	public Camera cam;
	
	private float ortSize = 3.1F;
	
	private bool useManualFixedTime = false;
	private float manualFixedTime = 0F;
	
	private bool disableShenanigans = false;
	
	private bool syncCube = false;
	
	private bool displayNotes = false;
	
	private Font font;
	
	// Use this for initialization
	void Start () {
		thisTransform = transform;
		orPos = thisTransform.position;
				
		font	= Resources.Load("bank-gothic-light-bt")  as Font;
		
		//intit fps average counter with zeros
		fpsAvgArr = new float[avg];
		for(int i = 0; i<avg; i++){
			fpsAvgArr[i] = 0;
		}
		
		//sets vsync to 1
		vsyncT2 = SetVsync();
		
		staticCubes = GameObject.FindGameObjectsWithTag("StaticCube");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!followRigidbody){
				thisTransform.position = orPos;
		}
		
		//normal deltaTime Movement
		if(!extrapolateNormal && !interpolateNormal){
			Vector3 direction = Vector3.left;
			if(fromLeft){ direction = Vector3.right; }
			
			if(normalMovement){	
				cube.transform.position += direction * Time.deltaTime		* speed;
			}else if(normalMovementSmooth){
				cube.transform.position += direction * Time.smoothDeltaTime	* speed;
			}
			KeepInBounds(cubeRigid);
			KeepInBounds(cube);
		}
		
//MANUAL INTER/EXTRA-POLATION:
		
//USAGE:-cube refers to any visible parts of the object that needs rigidbody movement but smooth camera appearance
//		-(that also includes attached bullet instancing objects, audio sources and listeners)
//		-cube has no rigidbody Component
//		
//		-cubeRigid is a rigidbody that moves in FixedUpdate()
//		-cubeRigid has no visible objects attached (no renderer)
//
//		-cube may be a child of cube rigid as its position is set every frame in Update()
//		-if using extrapolation only, lerp factor can be shortened (see below)
		
		if(extrapolateNormal || interpolateNormal){
			float lerpFactor = ((Time.time-Time.fixedTime)/Time.fixedDeltaTime);
			if(useManualFixedTime){
				lerpFactor = ((Time.time-manualFixedTime)/Time.fixedDeltaTime);
			}
			if (extrapolateNormal){
				//extrapolation
				cube.transform.position = (cubeRigid.transform.position + (cubeRigid.rigidbody.velocity*lerpFactor*Time.fixedDeltaTime));
				cube.transform.rotation = cubeRigid.transform.rotation * Quaternion.Euler(cubeRigid.rigidbody.angularVelocity*lerpFactor*Time.fixedDeltaTime*60F);
				//quaternions (rotations) are aggregated by multiplying them. I have no idea why i need 60F at the end, but it seems to work with any frame rate
			}else{
				//interpolation
				cube.transform.position = Vector3.Lerp(previousPosition, cubeRigid.position, lerpFactor);
				cube.transform.rotation = Quaternion.Lerp(previousRotation, cubeRigid.rotation, lerpFactor);
			}
			if(!rigidbodyRotating){
				cube.transform.position += Vector3.forward*1F; //offset to keep cube above rigidcube when not in rotation mode
			}
		}
//MANUAL INTER/EXTRA-POLATION END	

		if			(Input.GetAxis("Mouse ScrollWheel") < 0){	// back, zoom out
			ortSize *= 1.25F;
		}else if	(Input.GetAxis("Mouse ScrollWheel") > 0){	// forward, zoom in
			ortSize *= 0.8F;
		}
		ortSize = Mathf.Clamp(ortSize,3.1F,18.5F);
		cam.orthographicSize = ortSize;
		
		if(!disableShenanigans){				
			//frameRate
			if(lastFPS != frameRateSlider){
				if(frameRateSlider==201F)	{ Application.targetFrameRate = -1;}
				else						{ Application.targetFrameRate = (int)frameRateSlider;}
				lastFPS = frameRateSlider;
				//Debug.Log("frameRateSetCall");
			}
						
			//Rigidbody Interpolation
			if		(unityInterpolation){cubeRigid.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;}
			else if	(unityExtrapolation){cubeRigid.rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;}
			else						{cubeRigid.rigidbody.interpolation = RigidbodyInterpolation.None;}
		
			AddToFPSArray(Time.deltaTime);
		}				
	}
	
	void LateUpdate(){	
		if(followNormal){
			thisTransform.position = new Vector3(	  cube.position.x,10F,0.45F);
		}else if(followRigidbody){
			thisTransform.position = new Vector3(cubeRigid.position.x,10F,0.45F);
		}
	}
	
	private Vector3 previousPosition = Vector3.zero;
	private Quaternion previousRotation = Quaternion.identity;
	private bool rotEnter = true;
	
	void FixedUpdate(){
		
		previousPosition = cubeRigid.position; //save the Rigidbodys Position
		previousRotation = cubeRigid.rotation;
		
		//testing if difference, seems there is none
		manualFixedTime = Time.time;
		
		//movement
		if(!manualMovement){
			if(rigidbodyMovement){
				Vector3 direction = Vector3.left;
				if(fromLeft){ direction = Vector3.right; }
				
				cubeRigid.rigidbody.velocity = direction * speed;
				KeepInBounds(cubeRigid);
				KeepInBounds(cube);
			}
		}else{
			if(Input.GetKey("a")){
				cubeRigid.rigidbody.AddForce(Vector3.left* speed);
			}else if(Input.GetKey("d")){
				cubeRigid.rigidbody.AddForce(Vector3.right* speed);
			}
			KeepInBounds(cubeRigid);
			KeepInBounds(cube);
		}
		
		if(!manualMovement && !rigidbodyMovement){
			cubeRigid.rigidbody.velocity = Vector3.zero;
		}
		
		//Rotation
		if(rigidbodyRotating){
			Vector3 direction = Vector3.down;
			if(fromLeft){ direction = Vector3.up; }
			cubeRigid.rigidbody.AddTorque(direction * speed,ForceMode.Force);
			
			if(rotEnter){
				foreach(GameObject sc in staticCubes){
					sc.renderer.enabled = false;
				}
				cube.position = cubeRigid.position = Vector3.zero;
				cubeRigid.localScale = cube.localScale =  new Vector3(0.2F,1F,4F);
				rotEnter = false;
			}
		}else if(rotEnter == false){
			cubeRigid.rigidbody.angularVelocity = Vector3.zero;
			cubeRigid.rotation = cube.rotation = Quaternion.Euler(Vector3.zero);
			
			foreach(GameObject sc in staticCubes){
				sc.renderer.enabled = true;
			}
			cube.position		= new Vector3(5F,0F, 0.5F);
			cubeRigid.position	= new Vector3(5F,0F,-0.5F);
			cubeRigid.localScale = cube.localScale = new Vector3(1F,1F,1F);
			rotEnter = true;
			normalMovement = true;
			rigidbodyMovement = true;
		}
			
		if(!disableShenanigans){
			if(syncCube){cube.position = new Vector3( cubeRigid.position.x,0F,0.5F ); syncCube = false;}
		}
	}
	
	private float range = 12F;
	private void KeepInBounds(Transform cube){
		if(cube.position.x > range){		//right border
			cube.position = new Vector3(-range,0F,cube.position.z);
		}else if(cube.position.x < -range){	//left  border
			cube.position = new Vector3( range,0F,cube.position.z);
		}
	}
	
	private int avg = 200; //number of samples
	private float fpsAvg;
	private float[] fpsAvgArr;
	private void AddToFpsAvg(){
		
	}
	private void AddToFPSArray(float deltaTime){
		int l = fpsAvgArr.Length;
		fpsAvg = 0F;
		
		//shift array right, drops last value)
		for(int i = 0; i < l-1; i++){
			fpsAvgArr[l-i-1]	//last sample value
			=					//is now
			fpsAvgArr[l-i-2];	//last sample value -1
			fpsAvg += fpsAvgArr[l-i-1];
		}
		fpsAvgArr[0] = deltaTime;
		fpsAvg += deltaTime;
		
		//calcs the FPS and saves this fo acces in OnGUI
		fpsAvg = l/fpsAvg;
	}
	
	private int lastVsync = 0;	//remember setting, setting it every frame could be the caus of lag
	private float lastFPS = 0F;
	private void OnGUI(){
		GUI.skin.toggle.fontSize = 11;
		GUI.skin.label .fontSize = 12;
		
		disableShenanigans = (GUI.Toggle (new Rect(10,40, 200, 14),	disableShenanigans, "Disable UI Calculations"));
		GUI.Label (new Rect(25, 50, 200, 25),"(Settings are kept)");
		
		
		if(disableShenanigans){return;}
		
		GUI.skin.font = font;
		GUI.skin.toggle.font = font;
		//GUI.skin.box.font = ;
		
		GUI.skin.button.fontSize = 11;		
		int bh	= 22;
		int th	= 14;
		int	x	= 10;
		int y	= 10;
		int dy	= 15;
		int dy2	= 10;
		int bl	= 25;
		int bh2	= 13;
				
				
		//version and notes
		if(GUI.Button(new Rect(Screen.width-150,  8, 85, 15),"get source")){
			Application.OpenURL ("http://forum.unity3d.com/threads/162694-SmoothMovementTest-should-help-eliminate-Hiccup-sources");
		}
		displayNotes = (GUI.Toggle (new Rect(Screen.width-65,	5, 90, th),	displayNotes, "notes"));
		GUI.Label (new Rect(Screen.width-85,22,230,bh*4),"Version 1.2");
		if(displayNotes){
			GUI.skin.box.wordWrap = true;
			GUI.skin.box.alignment = TextAnchor.UpperLeft;
			GUI.Box(new Rect(Screen.width/2-400,Screen.height/2-100,800,200),"");//just lazy darkening of the box
			GUI.Box(new Rect(Screen.width/2-400,Screen.height/2-100,800,200),"");
			GUI.Box(new Rect(Screen.width/2-400,Screen.height/2-100,800,200),
				"This little topology was made to show the difference of moving:" +
				"\n-a Rigidbody with physics in FixedUpdate()," +
				"\n-a Non-Rigidbody object via transform.position in Update()" +
				"\nand the resulting movement jitters/hiccups caused by screen refresh rate interferences" +
				"\n" +
				"\nAdditionally the script contains an approach to inter/extra-polate any object's position/rotation to any rigidbody" +
				"\n" +
				"\nIf hiccups can not be eliminated in any setting, consider checking screen frequency-" +
				"\nscreen may report false value for vsync on a few devices (e.g. try 59 & 60Hz and watch for hiccups)" +
				"");
		}
		
		y+=dy;
		y+=dy;
		//Controls
		GUI.skin.label .fontSize = 22;
		GUI.Label (new Rect(Screen.width-145,y,230,bh*4),"Controls");
		GUI.skin.label .fontSize = 12;
		y+=dy2; y+=dy2;
		string controls = "Zoom:\tMouseWheel\n";
		if(manualMovement){controls += "Left:\tA\nRight:\tD";}
		GUI.Label (new Rect(Screen.width-140,y,230,bh*4),controls);
		
		y=0;
		
		//Speed
		if(!rigidbodyRotating){
			GUI.Label (new Rect(x,y,400,bh),"Speed: "+speed.ToString("F2")); y+=dy;
			speed	= GUI.HorizontalSlider	(new Rect(x, y, 150, th), speed, 0F, 50F);y+=dy2;
			speed	= Mathf.Round(speed * 10) / 10;
			fromLeft = (GUI.Toggle (new Rect(x,	y, 80, th),	fromLeft, "fromLeft"));
		}else{
			GUI.Label (new Rect(x,y,400,bh),"Rotational Speed: "+cubeRigid.rigidbody.angularVelocity.y.ToString("F2")); y+=dy;
			speed	= GUI.HorizontalSlider	(new Rect(x, y, 150, th), speed, 0F, 50F);y+=dy2;
			speed	= Mathf.Round(speed * 10) / 10;
			fromLeft = (GUI.Toggle (new Rect(x,	y, 80, th),	fromLeft, "clockwise"));
		}
				
		y=0;
		x+=180;
		//FrameRateControl
			string fpsDis = "";
			string frameRate = "FPS "+frameRateSlider;
			
			if(frameRateSlider==201F){frameRate = "FPS MAX";}
			GUI.Label(new Rect(x, y,  250, bh),""+frameRate);
			y+=dy2;
		
			if(vsync != 0){GUI.enabled = false; frameRateSlider = 60F; fpsDis ="(disabled due to VSYNC)";}
			
			GUI.Label(new Rect(x, y,  250, bh),""+fpsDis);
			frameRateSlider	= GUI.HorizontalSlider	(new Rect(x, y+5, 221, th), frameRateSlider, 6F, 201F);
			frameRateSlider	= Mathf.Round(frameRateSlider * 1) / 1;
						
			GUI.enabled = true;
		
			y+=dy;
		
			GUI.Label (new Rect(x,  y, 250, bh),"VSYNC:");
			x+=60;
			if(GUI.Toggle (new Rect(x, y, 40, th),	vsyncT1, "OFF"))		{vsyncT1 = SetVsync();}
			if(GUI.Toggle (new Rect(x+42, y, 25, th),		vsyncT2, "1"))	{vsyncT2 = SetVsync();}
			if(GUI.Toggle (new Rect(x+69, y, 25, th),	vsyncT3, "2"))		{vsyncT3 = SetVsync();}
			if 		(vsyncT1){vsync = 0;}
			else if (vsyncT2){vsync = 1;}
			else if (vsyncT3){vsync = 2;}
			if(lastVsync != vsync){
				QualitySettings.vSyncCount = vsync;
				lastVsync = vsync;
				//Debug.Log("vsyncSetCall");
			}
			y+=dy;
			
			//FPS
			GUI.Label (new Rect(x-60, y, 180, bh),"FPS (smoothDeltaTime):");
			GUI.Label (new Rect(x+110, y, 60, bh),""+(1F/Time.smoothDeltaTime).ToString("F2"));
			
			y+=dy2;
			GUI.Label (new Rect(x-60, y, 180, bh),"FPS (deltaTime):");
			GUI.Label (new Rect(x+110, y, 60, bh),""+(1F/Time.deltaTime).ToString("F2"));
		
			y+=dy;
			GUI.skin.label .fontSize = 32;
			GUI.Label (new Rect(x, y-5, 180, bh*2),"~"+fpsAvg.ToString("F2"));
			GUI.skin.label .fontSize = 12;
		
		
		
		y = 0;
		x+=165;
		//NonRigidCube		
			GUI.skin.label .fontSize = 28;
			GUI.Label (new Rect(x,  y, 220, bh*2),"→NoRigidbody");
			GUI.skin.label .fontSize = 12;
		x+=5;
		y+=dy;
		y+=dy;
			followNormal = (GUI.Toggle (new Rect(x,			y, 275, th),	followNormal, "Focus"));
			if(followNormal){followRigidbody = false;}
		y+=dy;
			if(extrapolateNormal || interpolateNormal || rigidbodyRotating){GUI.enabled = false;}
			normalMovement = (GUI.Toggle (new Rect(x,		y, 275, th),	normalMovement, "Move (using deltaTime)"));
			if(normalMovement){normalMovementSmooth = false;}
		y+=dy;
			normalMovementSmooth = (GUI.Toggle (new Rect(x,	y, 275, th),	normalMovementSmooth, "Move (using smoothDeltaTime)"));
			if(normalMovementSmooth){normalMovement = false;}
			GUI.enabled = true;
		y+=dy;
			if(unityExtrapolation || unityInterpolation){GUI.enabled = false;}
			extrapolateNormal = (GUI.Toggle (new Rect(x,	y, 275, th),	extrapolateNormal, "Extrapolate NoRigidb.Cube to Rigidbody"));
			if(extrapolateNormal){interpolateNormal = false;}
		y+=dy;
			interpolateNormal = (GUI.Toggle (new Rect(x,	y, 275, th),	interpolateNormal, "Interpolate NoRigidb.Cube to Rigidbody"));
			if(interpolateNormal){extrapolateNormal = false;}
		
			if(extrapolateNormal || interpolateNormal){normalMovement = false; normalMovementSmooth = false;}
			GUI.enabled = true;
		x+=150;
		
		
		y = 0;
			if(rigidbodyRotating){GUI.enabled = false;}
			if(GUI.Button(new Rect(x+85,  y+13, 55, 15),"→sync←")){syncCube = true;};
			GUI.enabled = true;
		x+=155;
		
		//RigidCube
			GUI.skin.label .fontSize = 28;
			GUI.Label (new Rect(x,  y, 180, bh*2),"Rigidbody←");
			GUI.skin.label .fontSize = 12;
		x+=5;
		y+=dy;
		y+=dy;
			followRigidbody = (GUI.Toggle (new Rect(x,		y, 275, th),	followRigidbody, "Focus"));
			if(followRigidbody){followNormal = false;}
		y+=dy;
			if(rigidbodyRotating){GUI.enabled = false;}
			rigidbodyMovement = (GUI.Toggle (new Rect(x,	y, 130, th),	rigidbodyMovement, "Move (FixedUpdate)"));
			if(rigidbodyMovement){manualMovement = false;}
			GUI.enabled = true;
			rigidbodyRotating = (GUI.Toggle (new Rect(x+135,y, 140, th),	rigidbodyRotating, "Rotate (FixedUpdate)"));
			if(rigidbodyRotating){rigidbodyMovement = false; normalMovement = false; normalMovementSmooth = false;}
		y+=dy;
			if(rigidbodyRotating){GUI.enabled = false;}
			manualMovement = (GUI.Toggle (new Rect(x,		y, 275, th),	manualMovement, "Move Manually (FixedUpdate)"));
			if(manualMovement){rigidbodyMovement = false;}
			GUI.enabled = true;
		y+=dy;
			if(extrapolateNormal || interpolateNormal){GUI.enabled = false;}
			unityExtrapolation = (GUI.Toggle (new Rect(x,	y, 275, th),	unityExtrapolation, "Unity's RigidbodyExtrapolation"));
			if(unityExtrapolation){unityInterpolation = false;}
		y+=dy;
			unityInterpolation = (GUI.Toggle (new Rect(x,	y, 275, th),	unityInterpolation, "Unity's RigidbodyInterpolation"));
			if(unityInterpolation){unityExtrapolation = false;}
			GUI.enabled = true;
		
		y = 0;
		x+=175;
		//TimeStep
		GUI.Label (new Rect(x,y,400,bh),"FixedUpdateRate: "+fixedStepSlider.ToString("F0")+" ("+(1F/fixedStepSlider).ToString("F2")+"s)"); y+=dy;
		fixedStepSlider	= GUI.HorizontalSlider	(new Rect(x, y, 150, th), fixedStepSlider, 5F, 120F);y+=dy2;
		fixedStepSlider	= Mathf.Round(fixedStepSlider * 1) / 1;
		Time.fixedDeltaTime = 1F/fixedStepSlider;
		
		y+=dy;
		y+=dy;
		y+=dy2;
			GUI.skin.label .fontSize = 32;
			GUI.Label (new Rect(x+60, y-5, 180, bh*2), fixedStepSlider+"");
			GUI.skin.label .fontSize = 12;
	}
	
	private	int  vsync				= 0;
	private	bool vsyncT1			= false;
	private	bool vsyncT2			= false;
	private	bool vsyncT3			= false;
	private bool SetVsync(){
		vsyncT1 = vsyncT2 = vsyncT3 = false;	
		return true;
	}
}
//←↖↑↗→↘↓↙