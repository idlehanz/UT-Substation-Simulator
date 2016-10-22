using UnityEngine;
using System.Collections;

public class SquirrelHandScript : MonoBehaviour {
    
    protected Rigidbody rigidBody;
    public SquirrelScript squirrelScript;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();


    }
	
	// Update is called once per frame
	void Update () {
	
	}




    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hand detected collision");
        //rigidBody.isKinematic = true;
        //rigidBody.detectCollisions = false;


    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {

    }


}
