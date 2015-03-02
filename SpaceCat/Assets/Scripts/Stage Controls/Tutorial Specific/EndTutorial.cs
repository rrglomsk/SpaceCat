using UnityEngine;
using System.Collections;

public class EndTutorial : MonoBehaviour {

	private GameObject spaceCat;
	CatController catController;
	Animator animator;
	

	void Start () {
		spaceCat = GameObject.Find("Cat");
		catController = (CatController) spaceCat.GetComponent (typeof(CatController));
	}

	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			DestroyObject (gameObject, 2.0f);
			int fishCount = PlayerPrefs.GetInt("fishCount", 0);
			fishCount += catController.getFish ();
			PlayerPrefs.SetInt("fishCount", fishCount);
			PlayerPrefs.SetInt ("tutorialComplete", 1);
			PlayerPrefs.Save();
		}
	}
}
