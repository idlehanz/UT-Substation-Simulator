using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;




public class LeverScript : MonoBehaviour, Interactable
{

    public SimulationEvent leverEvent;

    
    public Transform armTransform;
    protected float x = 0;

    protected bool leverUp = true;
    

    void Start()
    {
        if (leverEvent == null)
        {
            leverEvent = GetComponent<SimulationEvent>();
        }
    }
    void Update()
    {
        //up 332.9
        //down 
        if (!leverEvent.isEventTriggered())
        {
            if (armTransform.rotation.eulerAngles.x <25)
            {
                x = 1f;
                armTransform.Rotate(new Vector3(x, 0, 0));

            }
        }
        else
        {
            if (armTransform.rotation.eulerAngles.x >30)
            {
                x = -1f;
                armTransform.Rotate(new Vector3(x, 0, 0));

            }
        }

    }

    

    public void interact(GameObject interactor)
    {
        if (leverEvent.isEventTriggered()==false)
        {
            leverEvent.beginEvent();
            leverUp = false;
        }
    }

    public void displayInteractionMessage(GameObject interactor)
    {
        if (leverEvent != null)
            leverEvent.displayMessage();
        else
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Press e to........nothing.....");

        }
    }
}
