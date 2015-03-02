using UnityEngine;
using System.Collections;

public class RemoveBarrier : MonoBehaviour {

	CheckFloating checkFloating;
	private GameObject floatTrigger;

	void Start () {
		floatTrigger = GameObject.Find("FloatTrigger");
		checkFloating = (CheckFloating) floatTrigger.GetComponent (typeof(CheckFloating));
	}
	

	void Update () {
		if (checkFloating.getFloated()) {
			DestroyObject (gameObject);
		}

	}
}
