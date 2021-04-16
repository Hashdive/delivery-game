using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public float scrollSpeed;
	public float currentSpeed;
	public float tileWidth;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		currentSpeed = scrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * currentSpeed, tileWidth);
		transform.position = startPosition + Vector3.left * newPosition;
	}
}
