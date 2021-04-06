using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public float count;
	private float currentPos;

	private int titleState;

	public GameObject instructionsWindow;
	public GameObject logo;

	// Use this for initialization
	void Start () {
		titleState = 1;	
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Return) ){
			if (titleState == 1) {
				titleState = 2;
				instructionsWindow.SetActive (true);
				logo.SetActive (false);
			} else {
				SceneManager.LoadScene("scene-1");
			}
		}

	}
}
