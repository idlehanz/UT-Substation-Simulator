using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;

	float verticleRotation = 0;
	public float upDownRange = 60.0f;

	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
		//Cursor.lockState = true;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;//yaw
		transform.Rotate (0, rotLeftRight, 0);

		verticleRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticleRotation = Mathf.Clamp (verticleRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticleRotation, 0, 0);

		/*
		float rotUpDown = Input.GetAxis ("Mouse Y") * mouseSensitivity;//pitch
		float currentUpDown = Camera.main.transform.rotation.eulerAngles.x;
		float desiredUpDown = currentUpDown - rotUpDown;
		desiredUpDown = Mathf.Clamp (desiredUpDown, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (desiredUpDown, 0, 0);
		*/

		//Movement
		float forwardSpeed = Input.GetAxis ("Vertical");
		float sideSpeed = Input.GetAxis ("Horizontal");
		Vector3 speed = new Vector3 (sideSpeed,0, forwardSpeed);
		speed = transform.rotation * speed;

		CharacterController cc = GetComponent<CharacterController> ();
		cc.SimpleMove (speed * movementSpeed);

	}
}
