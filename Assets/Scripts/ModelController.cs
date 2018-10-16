using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour {

	public GameObject currentModel;
	public GameObject UI;
	RaycastHit[] hits;

	public bool dragging;
	public bool scaling;

	Vector3 offset;
	Vector3 scaleOrigin;

	float rotOrigin;
	float fingerDist;
	float angleOrigin;
	public float angle =0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!UI.activeInHierarchy) {
			HoldDownCheck ();
			ScaleNDrag ();
		}
	}

	//checks for user holding 1 figner on screen and the dragging of it to make the model
	void HoldDownCheck(){
		if (Input.touchCount==1 && Input.GetTouch (0).phase == TouchPhase.Began) {
			hits = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition));
			foreach (RaycastHit hit in hits) {
				if (hit.collider.gameObject == currentModel) {
					offset = Input.GetTouch (0).position;
					offset.z = currentModel.transform.position.z;
					offset = Camera.main.ScreenToWorldPoint (offset);
					offset = new Vector3 (currentModel.transform.position.x, currentModel.transform.position.y,0) - offset;
					dragging = true;
		
				}
			}
		}
		if (Input.touchCount==1 && dragging) {
			
			Vector3 mousePos = Input.GetTouch (0).position;
			mousePos.z = currentModel.transform.position.z;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			Vector3 newPos = mousePos + offset;
			currentModel.transform.position = new Vector3(newPos.x,newPos.y,currentModel.transform.position.z);
		}
		if (Input.GetMouseButtonUp (0))
			dragging = false;
	}


	//to scale and rotate model base on the first 2 finger of user
	void ScaleNDrag(){
		if (Input.touchCount ==2 && Input.GetTouch (1).phase == TouchPhase.Began) {
			dragging = false;

			fingerDist = Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
			scaleOrigin = currentModel.transform.localScale;
			angleOrigin = Mathf.Atan2 (Input.GetTouch (0).position.y - Input.GetTouch (0).position.y +1, Input.GetTouch (0).position.x - Input.GetTouch (0).position.x) - Mathf.Atan2 (Input.GetTouch (0).position.y - Input.GetTouch (1).position.y, Input.GetTouch (0).position.x - Input.GetTouch (1).position.x);
			rotOrigin = currentModel.transform.localEulerAngles.y;
			scaling = true;
		}
		if (Input.touchCount ==2 && scaling) {
			dragging = false;
			float newScale = Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position) / fingerDist;
			currentModel.transform.localScale = scaleOrigin * newScale;
			float newRot = Mathf.Atan2 (Input.GetTouch (0).position.y - Input.GetTouch (0).position.y +1, Input.GetTouch (0).position.x - Input.GetTouch (0).position.x);
			newRot -= Mathf.Atan2 (Input.GetTouch (0).position.y - Input.GetTouch (1).position.y, Input.GetTouch (0).position.x - Input.GetTouch (1).position.x);
			angle = ((angleOrigin - newRot)*50);
			currentModel.transform.localEulerAngles = new Vector3 (0, rotOrigin-angle, 0);
			//angleOrigin = newRot;
		}
		if (Input.touchCount < 2) {
			scaling = false;
		}
	}



}
