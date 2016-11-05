using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class SquirrelIncidentEvent : SimulationEvent
{
    //a reference to our squirrel prefab game object
    public GameObject squirrelPrefab;
    public GameObject squirrelPath;
    protected GameObject squirrelObject;
    protected SquirrelScript squirrelScript;
    LeverScript lever;

    public void Start()
    {
        lever = GetComponent<LeverScript>();
        if (lever == null)
            Debug.Log("did not find lever");
        if (squirrelPrefab == null)
            Debug.Log("ERROR: squirrel prefab not found for squirrel incident event");
        if (squirrelPath == null)
            Debug.Log("ERROR: squirrel path not found for squirrel incident event");
    }
    public void Update()
    {
        if (eventTriggered ==true)
        {
            if (squirrelObject==null)
            {
                eventTriggered = false;
            }
            if (squirrelScript != null && squirrelScript.isAlive()==false && squirrelScript.isPinned() ==false && squirrelScript.isTransformeredDestroyed()==false)
            {
                Destroy(squirrelObject);
                squirrelScript = null;
            }
            
        }
    }

    

    public void OnGUI()
    {

    }



    public override void beginEvent()
    {
        if (eventTriggered == false&&squirrelPrefab !=null && squirrelPath!=null)
        {
            eventTriggered = true;
            squirrelObject = Instantiate(squirrelPrefab);
            squirrelObject.GetComponent<SquirrelScript>().setNewPath(squirrelPath);
            squirrelScript = squirrelObject.GetComponent<SquirrelScript>();
        }

    }
    public override void endEvent()
    {
        
    }
    public override void displayMessage()
    {
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Press e to trigger squirrel event.");

    }
}