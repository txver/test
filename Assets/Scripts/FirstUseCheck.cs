using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUseCheck : MonoBehaviour {
	public GameObject helpUI;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("OnceUsed"))
			helpUI.SetActive (false);
		else {
			PlayerPrefs.SetInt ("OnceUsed", 0);
			PlayerPrefs.Save ();
		}


	}
	

}
