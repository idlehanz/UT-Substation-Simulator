/*This script is responsable for spawning the actors/objects neccissary for certain events to happen in the simulation.*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//delegate to act as a function pointer for events
delegate void EventFunction();
//this struct holds a key to activate an event and a function to call when that event is triggered.
struct Event
{
    public Event(KeyCode newActivateKey, EventFunction newEventFunction)
    {
        activateKey = newActivateKey;
        eventFunction = newEventFunction;
    }
    public void checkForTrigger()
    {
        if (Input.GetKeyDown(activateKey))
        {
            eventFunction();
        }
    }
    KeyCode activateKey;
    EventFunction eventFunction;

}

class EventTriggerScript : MonoBehaviour {


    List<Event> events;//list of events

    //a reference to our squirrel prefab game object
    public GameObject squirrelGameObject;
    public GameObject squirrelPath;
    // Use this for initialization
	void Start () {
        events = new List<Event>();

        //add the squirrel incident event
        events.Add(new Event(KeyCode.Q, squirrelIncident));
	}
	
	// Update is called once per frame
	void Update () {
        
        foreach (Event e in events)
        {
            e.checkForTrigger();
        }
	   
	}

    //this function is to trigger the squirrel incident.
    void squirrelIncident()
    {

        GameObject newSquirrel = Instantiate(squirrelGameObject);
        //turn off the random movement, turn on the follow path.
        newSquirrel.GetComponent<SquirrelMovement>().enabled = false;
        newSquirrel.GetComponent<SquirrelFollowSetPath>().setNewPathContrainer(squirrelPath);
        
    }
}
