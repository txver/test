using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffects : MonoBehaviour {

	public enum effectTypes{TextPop}

	public effectTypes effectOption;

	//for "TextPop"
	public int popMagnitude;
	public AnimationCurve popBehaviour;
	int originalSize;
	public Text popText;


	void Start(){
		if (effectOption == effectTypes.TextPop) {
			originalSize = popText.fontSize;
			StartCoroutine (PopText ());
		}
	}

	IEnumerator PopText(){
		for (float i = 0; i <= 1; i += Time.deltaTime * 2) {
			popText.fontSize = (originalSize+popMagnitude) - (int)Mathf.Lerp (0, popMagnitude*2, popBehaviour.Evaluate (i));
			yield return null;
		}
		for (float i = 0; i <= 1; i += Time.deltaTime * 2) {
			popText.fontSize = (originalSize-popMagnitude) +  (int)Mathf.Lerp (0, popMagnitude*2, popBehaviour.Evaluate (i));
			yield return null;
		}

		StartCoroutine (PopText ());
	}

}
