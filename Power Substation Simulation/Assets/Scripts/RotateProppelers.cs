using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProppelers : MonoBehaviour {

	Vector3 newrotation;
	public float rotateSpeed = 5.0f;
	public bool rotate = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( rotate ) {
			newrotation = transform.eulerAngles;
			newrotation.y += rotateSpeed * Time.deltaTime;
			transform.eulerAngles = newrotation;
		}
	}
}
