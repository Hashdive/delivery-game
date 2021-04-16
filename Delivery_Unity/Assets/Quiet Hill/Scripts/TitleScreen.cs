using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public float count;
	private float currentPos;

	private int titleState;

	public GameObject logo;
	public GameObject canvas;
	public GameObject enterText;
	public GameObject about;

	// Use this for initialization
	void Start () {
		titleState = 1;
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) ){
			if (titleState == 1) {
				titleState = 2;
				about.SetActive (true);
				logo.SetActive (false);
				enterText.SetActive (false);
				canvas.SetActive (false);
			} else {
				SceneManager.LoadScene("scene-1");
			}
		}

	}
}
