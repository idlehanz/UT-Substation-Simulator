/*this script is to moniter the squirrel and take appropriate action when it should....pass on*/

using UnityEngine;
using System.Collections;

public class SquirrelDeathScript : MonoBehaviour {
    //TODO
    //the idea will be to have 2 prefabs, one for an alive squirrel that will run around and one for a dead squirrel that....well...is dead
    public GameObject electrocutedSquirrel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //check if we should kill the squirrel.
        if (other.gameObject.tag == "Transformer")
        {
            Debug.Log("killing squirrel");
            Destroy(gameObject);
        }

        //spawn dead squirrel. TODO
    }




}
