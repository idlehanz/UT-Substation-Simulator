using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class SquirrelController : MonoBehaviour {

    public GameObject startWireNode = null;
    public List<Vector3> pathVectors = new List<Vector3>();
    public float speed = 3;
    protected int currentPathNode = 0;
    Rigidbody rigidBody;

    bool temp = true;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (temp)
        {
            temp = false;
            extractPathVectors();
        }
        Vector3 direction = pathVectors[currentPathNode] - transform.position;
        Vector3 velocity = direction.normalized * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + velocity;
        rigidBody.position = newPosition;
        if (Vector3.Distance(transform.position, pathVectors[currentPathNode])<speed)
        {
            currentPathNode++;
            Debug.Log(currentPathNode);
        }
    }


    public void extractPathVectors()
    {
        pathVectors = new List<Vector3>();
        Transform[] ts = startWireNode.GetComponentsInChildren<Transform>();
        pathVectors.Add(startWireNode.transform.position);
        foreach (Transform t in ts)
        {
            pathVectors.Add(t.position);
            Debug.Log(t.position);
        }
    }


}
