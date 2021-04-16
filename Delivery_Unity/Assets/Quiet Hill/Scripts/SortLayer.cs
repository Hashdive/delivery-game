using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		sortLayer ();	
	}

	void sortLayer(){
		float depth = gameObject.transform.position.y * 10;
		int sort = (int)depth * -1;

		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = sort;
		//Debug.Log (sort);
	}
}
