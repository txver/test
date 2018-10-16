using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationManager : MonoBehaviour {
	public enum Orientations{Potrait, LandScapeLeft,Free};

	public Orientations phoneOrientation;

	// Use this for initialization
	void Awake () {
		switch (phoneOrientation) {
			case Orientations.Potrait:
				Screen.orientation = ScreenOrientation.Portrait;
				break;
			case Orientations.Free:
				Screen.orientation = ScreenOrientation.AutoRotation;
				break;
			case Orientations.LandScapeLeft:
				Screen.orientation = ScreenOrientation.LandscapeLeft;
				break;

		}
	}

}
