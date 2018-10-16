using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FolderAdder : MonoBehaviour {



	// Use this for initialization
	void Start () {
		//add the desired folder if it does not exist
		Directory.CreateDirectory ("/storage/emulated/0/AR_PB_Photos");

	}
	

}
