using UnityEngine;
using System.Collections;

public class DisplayHint : MonoBehaviour {

	public GameObject hint;

	void Start () {
		GameObject currentHint = (GameObject)Instantiate(hint);
		currentHint.transform.SetParent (parent:transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
