using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeLogic : MonoBehaviour {
	public GameObject[] swipeObjects;
	public GameObject mainObj;

	public bool canSwipe = true;

	public float sensitivity;
	float UISize;
	float reachedVel;

	public int current = 0;

	public Sprite[] indicators;
	public Image indicator;

	// Use this for initialization
	void Start () {
		UISize = -swipeObjects [0].GetComponent <RectTransform> ().rect.width;
		ReArrange (current);
	}
	
	// Update is called once per frame
	void Update () {
		MouseTest ();
		indicator.sprite = indicators [current];
	}

	//for testing of script with keyboard and mouse
	void MouseTest(){
		
		if (Input.GetMouseButton (0) && canSwipe) {
			
			Move (Input.GetAxis ("Mouse X")*sensitivity);
		}
		if (Input.GetMouseButtonUp (0))
			Return ();
	}

	//check for user swiping on touch screen device
	void SwipeCheck(){
		
	}

	//to move the swiping UI
	void Move(float velocity){
		//Debug.Log (velocity);
		mainObj.transform.localPosition += Vector3.right * velocity;
		reachedVel = velocity;
	}

	//when user no longer is pressing the screen, check for speed of swipe to determine UI to scroll to
	void Return(){
		if (Mathf.Abs (reachedVel) > 20) {
			if(reachedVel<0)
				StartCoroutine (CompleteReturn (current+1));
			else if(reachedVel>0)
				StartCoroutine (CompleteReturn (current-1));
			return;
		}
		// for checking of closest swipe-able UI if swipe velocity does not reach treachhold
		float mainPos = mainObj.transform.localPosition.x;
		float closest = Mathf.Abs(-mainPos - swipeObjects[0].transform.localPosition.x);
		int closestObj = 0;
		for (int i = 0; i < swipeObjects.Length; i++) {
			if (Mathf.Abs(-mainPos - swipeObjects[i].transform.localPosition.x) < closest) {
				closest = Mathf.Abs(-mainPos - swipeObjects[i].transform.localPosition.x);
				closestObj = i;
			}
			if (i == swipeObjects.Length - 1) {
				StartCoroutine (CompleteReturn (closestObj));
			}
		}

	}
	//the actual scrolling effect of UI
	IEnumerator CompleteReturn(int index){
		if (index < 0)
			index = swipeObjects.Length - 1;
		if (index == swipeObjects.Length)
			index = 0;

		current = index;

		canSwipe = false;

		Vector3 origin = mainObj.transform.localPosition;
		Vector3 end =(-swipeObjects [index].transform.localPosition.x) * Vector3.right;
		Vector3 direction = Vector3.right*(end.x-origin.x);
		direction = direction.normalized;
		for (float f = 0; f <= 1; f += Time.deltaTime * 4) {
			mainObj.transform.localPosition = Vector3.Lerp (origin, end, f);
			yield return null;
		}	
		reachedVel = 0;
		mainObj.transform.localPosition = Vector3.right * UISize;
		ReArrange (current);

		canSwipe = true;

	}

	//rearrangement of UI for infinite swiping in a direction
	void ReArrange(int index){
		List<GameObject> arrangement = new List<GameObject> ();
		if (index - 1 < 0) {
			arrangement.Add (swipeObjects [swipeObjects.Length - 1]);
			for (int i = index; i < swipeObjects.Length - 1; i++) {
				arrangement.Add (swipeObjects [i]);
			}
		} else if (index + 1 == swipeObjects.Length) {
			arrangement.Add (swipeObjects [index - 1]);
			arrangement.Add (swipeObjects [swipeObjects.Length - 1]);
			for (int i = 0; i < swipeObjects.Length - 2; i++) {
				arrangement.Add (swipeObjects [i]);
			}
		} else {
			arrangement.Add (swipeObjects [index - 1]);
			for (int i = index; i < swipeObjects.Length; i++) {
				arrangement.Add (swipeObjects [i]);
			}
			for (int i = 0; i < index - 1; i++) {
				arrangement.Add (swipeObjects[i]);
			}
		}

		for (int i = 0; i < arrangement.Count; i++) {
			arrangement [i].transform.SetSiblingIndex (i);
		}
	}

}
