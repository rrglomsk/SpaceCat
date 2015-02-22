using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	private bool moving = false;
	private Animator animator;
	private Animator startAnimator;
	private GameObject spaceCat;
	private GameObject backdrop;
	private GameObject mountain;
	private Camera controlsCamera;

	public Vector3 backSpeed = new Vector3();
	public Vector3 mountainSpeed = new Vector3();

	public Vector3 moveSpeed = new Vector3();


	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("Cat");
		//backdrop = GameObject.Find("Backdrop");
		//mountain = GameObject.Find ("Mountains");
		animator = GameObject.Find ("Cat").GetComponent<Animator>();
		startAnimator = GetComponent<Animator>();
		controlsCamera = GameObject.Find("Controls Camera").camera;

	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
						if (Input.touchCount > 0) {
								if (Input.GetTouch (0).phase == TouchPhase.Began) {
										CheckTouch (Input.GetTouch (0).position, "began");
								} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
										CheckTouch (Input.GetTouch (0).position, "ended");
								}
						}
				} else if (Application.platform == RuntimePlatform.WindowsEditor) {
						if (Input.GetMouseButtonDown (0)) {
								spaceCat.transform.position += moveSpeed;
						}
				}

		if (moving) { //&& backdrop.transform.position.x > -4.8f)
			animator.SetBool ("Walking", true);
			//spaceCat.transform.position += moveSpeed;
			spaceCat.transform.Translate(0.05f, 0f,0f);
			//backdrop.transform.position += backSpeed;
			//mountain.transform.position += mountainSpeed;

		} else {
			animator.SetBool("Walking", false);
		}
		
	}

	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = controlsCamera.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit.gameObject.name == "Move Button" && hit && phase == "began")
		{
			moving = true;
			startAnimator.SetBool ("Pressed", true);
		}
		
		if (hit && phase == "ended")
		{
			moving = false;
			startAnimator.SetBool ("Pressed", false);
		}
	}

}



