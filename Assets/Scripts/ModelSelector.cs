using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelector : MonoBehaviour {

	public GameObject[] models;


	int currentModel;

	// Use this for initialization
	void Start () {
	}
	
	public void ChangeModel(int modelNo){
		foreach (GameObject model in models) {
			model.SetActive (false);
		}
		models [modelNo].SetActive (true);
	}
		

}
