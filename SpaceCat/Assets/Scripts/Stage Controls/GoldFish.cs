using UnityEngine;
using System.Collections;

public class GoldFish : MonoBehaviour {

	private GameObject spaceCat;
	private Animator animator;

	// Use this for initialization
	void Start () {
		spaceCat = GameObject.Find("cat");
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spaceCat) {
			animator.SetBool("Collected", true);
			DestroyObject (this);
		}
	}
}