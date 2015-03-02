using UnityEngine;
using System.Collections;

public class DisplayStageSelectObjects : MonoBehaviour {
	GameObject fireCoin;
	GameObject iceCoin;
	GameObject endCrystal;
	public GameObject greeting;


	void Start () {
		fireCoin = GameObject.Find ("Fire Coin");
		iceCoin = GameObject.Find ("Ice Coin");
		endCrystal = GameObject.Find ("End Crystal");
		fireCoin.SetActive(false);
		iceCoin.SetActive (false);
		endCrystal.SetActive (false);
		checkCoins ();
		checkTutorial ();
	}
	

	void Update () {
	
	}

	void checkCoins() {
		int planet1Complete = PlayerPrefs.GetInt ("completedPlanet1", 0);
		int planet2Complete = PlayerPrefs.GetInt ("completedPlanet2", 0);
		int planet3Complete = PlayerPrefs.GetInt ("completedPlanet3", 0);
		
		if (planet1Complete == 1) {
			fireCoin.SetActive(true);
		} 
		
		if (planet2Complete == 1) {
			iceCoin.SetActive(true);
		}

		if (planet3Complete == 1) {
			endCrystal.SetActive(true);
		}
	}

	void checkTutorial(){
		int tutorialComplete = PlayerPrefs.GetInt ("tutorialComplete", 0);
		if (tutorialComplete == 1) {
			greeting.SetActive (false);
		}
	}
}


