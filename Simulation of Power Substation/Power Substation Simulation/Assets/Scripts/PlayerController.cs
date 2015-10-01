using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();//connect rigid body
	}

	void FixedUpdate()//called before physics calculations, physics code here
	{
		//movement code
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVeritical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVeritical);
		rb.AddForce (movement * speed);
	}

}
