using UnityEngine;
using System.Collections;

public class DisplayHint : MonoBehaviour {

	public GameObject hintMove;
	public GameObject hintJump;
	public GameObject hintObstacle;
	public GameObject hintFish;
	public GameObject hintFinal;
	public GameObject spaceCat;
	public GameObject obstacle;
	public GameObject stageBarrierFloat;
	public GameObject floatTrigger;
	public GameObject obstacleTrigger;
	public GameObject fishTrigger;
	public GameObject fishTriggerEnd;
	public GameObject finalTrigger;

	FakeObstacle fakeObstacle;
	TriggerFloatHint triggerFloatHint;
	CheckFloating checkFloating;
	TriggerObstacleHintEnd triggerObstacleHintEnd;
	TriggerFishHint triggerFishHint;
	TriggerObstacleFishEnd triggerObstacleFishEnd;
	TriggerFinalHint triggerFinalHint;

	Animator animator;

	private bool moveClicked = false;
	
	void Start () {
		fakeObstacle = (FakeObstacle) obstacle.GetComponent (typeof(FakeObstacle));
		triggerFloatHint = (TriggerFloatHint)stageBarrierFloat.GetComponent (typeof(TriggerFloatHint));
		checkFloating = (CheckFloating)floatTrigger.GetComponent (typeof(CheckFloating));
		triggerObstacleHintEnd = (TriggerObstacleHintEnd)obstacleTrigger.GetComponent (typeof(TriggerObstacleHintEnd));
		triggerFishHint = (TriggerFishHint)fishTrigger.GetComponent (typeof(TriggerFishHint));
		triggerObstacleFishEnd = (TriggerObstacleFishEnd)fishTriggerEnd.GetComponent (typeof(TriggerObstacleFishEnd));
		triggerFinalHint = (TriggerFinalHint)finalTrigger.GetComponent (typeof(TriggerFinalHint));
	}
	
	// Update is called once per frame
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

		if (moveClicked && hintMove) {
			animator = hintMove.GetComponent<Animator>();
			animator.SetBool("tipSeen", true);
			DestroyObject (hintMove, 4.0f);
		}

		if (triggerFloatHint.getFloatHint () && hintJump) {
			hintJump.transform.position = getHintPosition ();
		}

		if (triggerFloatHint.getFloatHint () && checkFloating.getFloated () && hintJump) {
			animator = hintJump.GetComponent<Animator>();
			animator.SetBool("tipSeen", true);
			DestroyObject (hintJump, 4.0f);
		}

		if (fakeObstacle.getHit() && hintObstacle) {
			hintObstacle.transform.position = getHintPosition ();
		}

		if (triggerObstacleHintEnd.getObstacleEndHint() && hintObstacle) {
			animator = hintObstacle.GetComponent<Animator>();
			animator.SetBool("tipSeen", true);
			DestroyObject (hintObstacle, 4.0f);
		}

		if (triggerFishHint.getFishHint () && hintFish) {
			hintFish.transform.position = getHintPosition ();
		}

		if (triggerObstacleFishEnd.getFishEndHint() && hintFish) {
			animator = hintFish.GetComponent<Animator>();
			animator.SetBool("tipSeen", true);
			DestroyObject (hintFish, 4.0f);
		}

		if (triggerFinalHint.getFinalHint ()) {
			hintFinal.transform.position = getHintPosition ();
		}
	}
	

	Vector3 getHintPosition() {
		Vector3 pos = spaceCat.transform.position;
		pos.y -= 45.0f;
		return pos;
	}

	void CheckTouch(Vector3 pos, string phase) {
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit.gameObject.name == "Move Button" && hit && phase == "began") {
			moveClicked = true;
		}
	}
}
