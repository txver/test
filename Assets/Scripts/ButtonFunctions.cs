using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {

	public GameObject canvasUI;
	public Button captureButton;


	//for moving to different scene
	public void ToSelection(){
		SceneManager.LoadScene (1);
	}
	public void ToARPhoto(){
		SceneManager.LoadScene (1);
	}

	public void ToVuforia(){
		SceneManager.LoadScene (2);
	}

	public void ToCredits(){
		SceneManager.LoadScene (3);
	}
	public void ToMain(){
		SceneManager.LoadScene (0);
	}
	//----------

	//for bringing user to the desired website
	public void ToWeb(){
		Application.OpenURL ("http://www.spgwineclub.com/");
	}

	public void ToFaceBook(){
		Application.OpenURL ("https://www.facebook.com/SPG-Wine-Club-1991026871159600/");
	}

	public void Close(GameObject toInactive){
		toInactive.SetActive (false);
	}

	public void Open(GameObject toInactive){
		toInactive.SetActive (true);
	}

	public void Toggle(GameObject toToggle){
		toToggle.SetActive (!toToggle.activeInHierarchy);
	}

	public void Quit(){
		Application.Quit ();
	}

	//----------

	public void CaptureScreen(){
		StartCoroutine (TakeScreenCap ());	
	}


	//to take screenshot
	IEnumerator TakeScreenCap(){
		captureButton.interactable = false;
		canvasUI.SetActive (false);
		yield return new WaitForEndOfFrame ();
		Texture2D screenImage = new Texture2D (Screen.width, Screen.height);
		screenImage.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		screenImage.Apply ();
		byte[] imageBytes = screenImage.EncodeToPNG ();

		string timeDate = System.DateTime.Now.ToString ("yy-MM-dd");
		timeDate = timeDate + "_" + Directory.GetFiles ("/storage/emulated/0/AR_PB_Photos").Length;
		System.IO.File.WriteAllBytes ("/storage/emulated/0/AR_PB_Photos/" + timeDate + ".png", imageBytes);

		yield return new WaitForEndOfFrame ();
		captureButton.interactable = true;
		canvasUI.SetActive (true);

		AndroidJavaObject classPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
		AndroidJavaClass classUri = new AndroidJavaClass ("android.net.Uri");

		AndroidJavaObject objIntent = new AndroidJavaObject ("android.content.Intent", new object[2] {
			"android.intent.action.MEDIA_SCANNER_SCAN_FILE",
			classUri.CallStatic <AndroidJavaObject> ("parse", "/storage/emulated/0/AR_PB_Photos/" + timeDate + ".png")
		});
		objActivity.Call ("sendBroadcast",objIntent);


	}
}
