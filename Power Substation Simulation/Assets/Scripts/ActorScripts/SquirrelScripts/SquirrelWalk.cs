using UnityEngine;
using System.Collections;

public class SquirrelWalk : MonoBehaviour {

	public float speed = 1.5f;
	public float distance = 40f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (distance > 0) {
			Vector3 velocity = transform.forward * Time.deltaTime * speed;
			distance -= velocity.magnitude;
			transform.position += velocity;
		}
	}
}
