using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetSelect : MonoBehaviour {

	private bool selected = false;
	private string selectedPlanet;
	private Animator animator;
	private int fishCount;
	private GameObject lockedText;
	string planet1Text;
	string planet2Text;
	string planet3Text;


	void Start () {
		selectedPlanet = this.name;
		planet1Text = "You need to complete the tutorial before you can visit this planet";
		planet2Text = "You need at least 3 Gold Fish and the Fire Token to visit this planet";
		planet3Text = "You need at least 8 Gold Fish and the Ice Token to visit this planet";

	}

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

	void planet1Start(Vector3 pos) {
		int tutorialComplete = PlayerPrefs.GetInt ("tutorialComplete", 0);
		if (tutorialComplete == 1) {
			Application.LoadLevel ("Planet 1");
		} else {
			lockedText.transform.position = new Vector3(520.0f, 340.0f, 0);
			Text planetText = lockedText.gameObject.GetComponent<Text> ();
			planetText.text = planet1Text;
		}
	}

	void planet2Start(Vector3 pos) {
		int fishCount = PlayerPrefs.GetInt("fishCount");
		if (fishCount >= 1) {
			Application.LoadLevel ("Planet 2");
		} else {
			lockedText.transform.position = new Vector3(580.0f, 130.0f, 0);
			Text planetText = lockedText.gameObject.GetComponent<Text> ();
			planetText.text = planet2Text;
		}
	}

	void planet3Start(Vector3 pos) {
		int fishCount = PlayerPrefs.GetInt("fishCount");
		if (fishCount >= 8) {
			Application.LoadLevel ("Planet 3");
		} else {
			lockedText.transform.position = new Vector3(690.0f, 170.0f, 0);
			Text planetText = lockedText.gameObject.GetComponent<Text> ();
			planetText.text = planet3Text;
		}
	}

	void planetTutorialStart() {
		Application.LoadLevel ("Tutorial");
	}

	public void setLocked(GameObject text) {
		lockedText = text;
	}


	
	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);

		if (hit.gameObject.name == "Planet 1" && hit && phase == "began") {
			planet1Start (pos);
		} else if (hit.gameObject.name == "Planet 2" && hit && phase == "began") {
			planet2Start (pos);
		} else if (hit.gameObject.name == "Planet 3" && hit && phase == "began") {
			planet3Start (pos);
		} else if (hit.gameObject.name == "Spaceship" && hit && phase == "began") {
			planetTutorialStart();
		}
	}
}
