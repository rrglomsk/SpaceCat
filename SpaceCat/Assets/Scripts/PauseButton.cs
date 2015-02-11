using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	private bool moving = false;
	private GameObject spaceCat;
	private GameObject backdrop;
	private GameObject ground;
	public Vector3 backSpeed = new Vector3();
	public Vector3 moveSpeed = new Vector3();

	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("cat");
		backdrop = GameObject.Find("Backdrop");
		ground = GameObject.Find ("Ground");
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

		if (moving) //&& backdrop.transform.position.x > -4.8f)
		{
			spaceCat.transform.position += moveSpeed;
			backdrop.transform.position -= backSpeed;
			ground.transform.position -= backSpeed;

		}	
	}

	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit.gameObject.name == "Movement" && hit && phase == "began")
		{
			moving = true;
		}
		
		if (hit.gameObject.name == "Movement" && hit && phase == "ended")
		{
			moving = false;
		}
	}
}



