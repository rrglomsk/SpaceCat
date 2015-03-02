using UnityEngine;
using System.Collections;

public class EndStage : MonoBehaviour {

	private GameObject spaceCat;
	private GameObject spaceShip;
	CatController catController;
	Animator animator;
	public GameObject endMessage;


	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("Cat");
		spaceShip = GameObject.Find("Spaceship");

		catController = (CatController) spaceCat.GetComponent (typeof(CatController));
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			animator.SetBool("Collected", true);
			DestroyObject (gameObject, 2.0f);
			Vector3 messagePosition = spaceShip.transform.position;
			messagePosition.y += 250.0f;
			messagePosition.x += 380.0f;
			endMessage.transform.position = messagePosition;
			int fishCount = PlayerPrefs.GetInt("fishCount");
			fishCount += catController.getFish ();
			PlayerPrefs.SetInt("fishCount", fishCount);

			if (Application.loadedLevelName == "Planet 1") {
				PlayerPrefs.SetInt ("completedPlanet1", 1);
			} else if (Application.loadedLevelName == "Planet 2") {
				PlayerPrefs.SetInt ("completedPlanet2", 1);
			} else if (Application.loadedLevelName == "Planet 3") {
				PlayerPrefs.SetInt ("completedPlanet3", 1);
			}
			PlayerPrefs.Save();
		}
	}
}
