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
    protected GameObject squirrel;


    public void Start()
    {
        if (squirrelPrefab == null)
            Debug.Log("ERROR: squirrel prefab not found for squirrel incident event");
        if (squirrelPath == null)
            Debug.Log("ERROR: squirrel path not found for squirrel incident event");
    }
    public void Update()
    {
        if (eventTriggered ==true)
        {
            if (squirrel==null)
            {
                eventTriggered = false;
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
            squirrel = Instantiate(squirrelPrefab);
            squirrel.GetComponent<SquirrelScript>().setNewPath(squirrelPath);
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