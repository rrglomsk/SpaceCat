using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	
	private GameObject spaceCat;
	public Vector3 jumpMovement = new Vector3();


	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("cat");
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
							spaceCat.rigidbody2D.gravityScale = -0.1f;

						}
				}
		}

	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit.gameObject.name == "Gravity" && hit && phase == "began")
		{
			spaceCat.rigidbody2D.gravityScale = -0.1f;
			//spaceCat.transform.position += jumpMovement;
			//Physics.gravity = new Vector3(0, -1.0F, 0);
		}
	}
}
