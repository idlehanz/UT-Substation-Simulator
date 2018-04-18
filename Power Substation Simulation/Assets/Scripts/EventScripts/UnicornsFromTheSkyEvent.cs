using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class UnicornsFromTheSkyEvent : SimulationEvent
{
    //a reference to our squirrel prefab game object
    public GameObject unicornContainerPrefab;

    
    

    protected GameObject unicornContainer;
    
    LeverScript lever;

    public void Start()
    {
        canCancel = true;
        lever = GetComponent<LeverScript>();
        if (lever == null)
            Debug.Log("did not find lever");
        if (unicornContainerPrefab == null)
            Debug.Log("ERROR: could not find unicorns :-(");
        

    }
    public void Update()
    {
        if (eventTriggered == true)
        {
            if (unicornContainerPrefab == null)
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
        if (eventTriggered == false && unicornContainerPrefab != null)
        {
            eventTriggered = true;

            unicornContainer = Instantiate(unicornContainerPrefab);
            
        }

    }



    public override void endEvent()
    {
        if (unicornContainer!=null)
        {
            Destroy(unicornContainer);
        }
        eventTriggered = false;
    }
    public override void displayMessage()
    {
        GUI.color = Color.white;
        if (!eventTriggered)
            GUI.Box(new Rect(20, 20, 200, 55), "Press e for unicorns.");
        else
            GUI.Box(new Rect(20, 20, 200, 55), "press e to remove unicorns");

    }
}

