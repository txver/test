using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems : Editor {

	[MenuItem("Tools/Clear PlayerPrefs")]
	private static void NewMenuOption(){
		PlayerPrefs.DeleteAll ();
	}
}
