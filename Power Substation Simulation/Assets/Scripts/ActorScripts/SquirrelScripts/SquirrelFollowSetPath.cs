/*SquirrelFollowSetPath.cs
 script to allow squirrel to follow a set path,
 it takes in a GameObject and extracts any gameObject children from it, it then uses their position vectors as waypoints to follow. */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquirrelFollowSetPath : MonoBehaviour {

    //pass in a gameobject with multiple gameobjects as children,
    //it will extract position data from children.
    public GameObject pathContainer = null;
    public float speed = 10f;

    protected List<Vector3> path;
    protected int currentPathNode=0;

    protected Rigidbody rigidBody;



	// Use this for initialization
	void Start () {
        extractPathVectors();
        rigidBody = GetComponent<Rigidbody>();//get the rigid body for the character
        rigidBody.freezeRotation = true;//freeze rotation so we don't go rolling off
        if (pathContainer == null)
        {
            Debug.Log("Squirrel path null");
        }

    }

    // Update is called once per frame
    void Update () {
        if (pathContainer == null)
        {
            
        }
        else
        {
            Vector3 direction = path[currentPathNode] - transform.position;
            Vector3 velocity = direction.normalized*speed*Time.deltaTime;
            

            Vector3 newPosition = transform.position + velocity;

            
            if (Vector3.Distance(newPosition, path[currentPathNode]) < 1.5f)
            {
                if (currentPathNode < path.Count - 1)
                    currentPathNode++;

            }
            rigidBody.MovePosition(newPosition);

        }



    }


    public void setNewPathContrainer(GameObject newPathContainer)
    {
        pathContainer = newPathContainer;
        extractPathVectors();
        Debug.Log("setting new path");
        if (pathContainer == null)
        {
            Debug.Log("Squirrel path null after reset");
        }
    }

    void extractPathVectors()
    {
        path = new List<Vector3>();
        if (pathContainer != null)
        {
            foreach (Transform child in pathContainer.transform)
            {
                //child is your child transform
                path.Add(child.transform.position);
            }
        }
    }




}
