using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

	public GameObject lockedMessage;
	GameObject planet1;
	GameObject planet2;
	GameObject planet3;
	PlanetSelect planetSelect1;
	PlanetSelect planetSelect2;
	PlanetSelect planetSelect3;

	// Use this for initialization
	void Start () {
		GameObject messages = (GameObject)Instantiate(lockedMessage);
		messages.transform.SetParent (parent:transform);

		planet1 = GameObject.Find("Planet 1");
		planetSelect1 = (PlanetSelect) planet1.GetComponent (typeof(PlanetSelect));
		planetSelect1.setLocked (messages);

		planet2 = GameObject.Find("Planet 2");
		planetSelect2 = (PlanetSelect) planet2.GetComponent (typeof(PlanetSelect));
		planetSelect2.setLocked (messages);

		planet3 = GameObject.Find("Planet 3");
		planetSelect3 = (PlanetSelect) planet3.GetComponent (typeof(PlanetSelect));
		planetSelect3.setLocked (messages);
	}
	
	// Update is called once per frame
	void Update () {
	 
	}
}
