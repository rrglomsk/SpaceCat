using UnityEngine;
using System.Collections;

public class GoldFish : MonoBehaviour {

	private GameObject spaceCat;
	private Animator animator;
	CatController catController;
	
	void Start () {
		spaceCat = GameObject.Find("Cat");
		animator = GetComponent<Animator>();
		catController = (CatController) spaceCat.GetComponent (typeof(CatController));
	}

	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			animator.SetBool("Collected", true);
			audio.Play();
			DestroyObject (gameObject, 2.0f);
			catController.addFish();
		}
	}
}