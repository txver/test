using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			switch (SceneManager.GetActiveScene ().buildIndex) {
				case 0:
					Application.Quit ();
					break;
				case 1:
					SceneManager.LoadScene (0);
					break;
				case 2:
					SceneManager.LoadScene (1);
					break;
				case 3:
					SceneManager.LoadScene (1);
					break;
			}
		}
	}
}
