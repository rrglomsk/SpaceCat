using UnityEngine;
using System.Collections;

public class PlanetSelect : MonoBehaviour {

	private bool selected = false;
	private string selectedPlanet;
	private Animator animator;


	
	// Use this for initialization
	void Start () {
		selectedPlanet = this.name;
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					CheckTouch (Input.GetTouch (0).position, "began");
				}  else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					CheckTouch (Input.GetTouch (0).position, "ended");
				}
			}
		}
	}
	
	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);

		if (hit.gameObject.name == "Planet 1" && hit && phase == "began") {

		}

		if (hit.gameObject.name == "Planet 1" && hit && phase == "ended") {
			Application.LoadLevel ("Planet 1");
		}
	}
}
