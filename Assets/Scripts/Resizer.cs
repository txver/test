using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resizer : MonoBehaviour {

	public Camera unityCamera;


	// Use this for initialization
	void Start () {

		float pos = unityCamera.nearClipPlane + 10f;
		transform.position = unityCamera.transform.position + unityCamera.transform.forward * pos;
		float height = Mathf.Tan (unityCamera.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2*1/10;
		transform.localScale = new Vector3 (height, 1, height * unityCamera.aspect);
		transform.localEulerAngles = new Vector3 (180, 90, -90);


		WebCamTexture camTexture = new WebCamTexture (2560,1440);

		GetComponent <Renderer> ().material.mainTexture = camTexture;
		if (!camTexture.isPlaying)
			camTexture.Play ();

	}
	
	// Update is called once per frame
	void Update () {
	}
}
