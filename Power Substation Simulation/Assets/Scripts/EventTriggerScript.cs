using UnityEngine;
using System.Collections;

public class EventTriggerScript : MonoBehaviour {
    public GameObject squirrelGameObject;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            Instantiate(squirrelGameObject);
        }
	   
	}
}
