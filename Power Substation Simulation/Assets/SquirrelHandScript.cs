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
        Debug.Log("Collided");
        if (other.tag == "Transformer")
        {
            squirrelScript.setRagdollState(true);
            rigidBody.isKinematic = true;
        }


    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {

    }


}
