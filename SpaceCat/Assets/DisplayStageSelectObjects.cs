using UnityEngine;
using System.Collections;

public class DisplayStageSelectObjects : MonoBehaviour {
	GameObject fireCoin;
	GameObject iceCoin;
	public GameObject greeting;


	void Start () {
		fireCoin = GameObject.Find ("Fire Coin");
		iceCoin = GameObject.Find ("Ice Coin");
		fireCoin.SetActive(false);
		iceCoin.SetActive (false);
		checkCoins ();
		checkTutorial ();
	}
	

	void Update () {
	
	}

	void checkCoins() {
		int planet1Complete = PlayerPrefs.GetInt ("completedPlanet1", 0);
		int planet2Complete = PlayerPrefs.GetInt ("completedPlanet2", 0);
		
		if (planet1Complete == 1) {
			fireCoin.SetActive(true);
		} 
		
		if (planet2Complete == 1) {
			iceCoin.SetActive(true);
		}
	}

	void checkTutorial(){
		int tutorialComplete = PlayerPrefs.GetInt ("tutorialComplete", 0);
		if (tutorialComplete == 1) {
			greeting.SetActive (false);
		}
	}
}


