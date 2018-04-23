using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVScript : MonoBehaviour {

	public GameObject[] nodes;
	public GameObject[] propellers;
	public GameObject destination;
	Vector3 moveVector, aimVector;
	int i = 0;
	public float speed, targetSpeed;
	public float rotateSpeed;
	Vector3 targetPosition;
	bool destReached = false; // aka destination reached
	public GameObject transformer;

	// Use this for initialization
	void Start () {
		targetPosition = transform.position;
		destination = nodes[0];
		nodes[0].GetComponent<Collider>().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if ( destReached == false ) {
			moveVector = (destination.transform.position - targetPosition).normalized;
			targetPosition += moveVector * targetSpeed * Time.deltaTime;
			aimVector = destination.transform.position - transform.position;
			//transform.rotation = Quaternion.RotateTowards ( Quaternion.LookRotation ( aimVector, transform.up ), Quaternion.LookRotation ( transform.forward, transform.up ), Mathf.Rad2Deg * rotateSpeed * Time.deltaTime );
			if ( Vector3.Distance(targetPosition, destination.transform.position) < 2.0f) {
				i++;
				if ( i >= nodes.Length ) {
					destReached = true;
					TransformerScript transformerscript = transformer.GetComponent<TransformerScript>();
					transformerscript.interact(gameObject);
					Rigidbody rbody = gameObject.GetComponent<Rigidbody>();
					rbody.useGravity = true;
					rbody.freezeRotation = false;
				}
				destination = nodes[i]; 
			}	
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed ); 
			//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimVector), Time.deltaTime * rotateSpeed);
		}
	}

}
