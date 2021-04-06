using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public float fadeTime;
	private Image blackscreen;

	// Use this for initialization
	void Start () {
		blackscreen = GetComponent<Image> ();	
	}
	
	// Update is called once per frame
	void Update () {
		blackscreen.CrossFadeAlpha (0f, fadeTime, false);	

		if(blackscreen.color.a == 0){
			gameObject.SetActive (false);
		}
	}
}
