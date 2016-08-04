/*ElectricalComponent.cs
 * this file contains the base class for all electrical components for the electrical substation simulation.
 * it contains some basic functions that will be shared amongst all components and some abstract functions that
 * will require more specialized attention by each component. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

struct electricalFlow
{
    public float voltage;
    public float frequency;
    public float current;
}

abstract class ElectricalComponentScript : MonoBehaviour
{
    public static int id = 0;
    protected string newTag;

    protected electricalFlow output;
    protected electricalFlow input;
    protected GameObject player;
    protected RayCasting playerRaycast;


    public ElectricalComponentScript inputComponent;

    void Start()
    {
       

        //next find the player and store it's rayCast as a reference,
        //this way we don't have to continually look for it when we need it.
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerRaycast = player.GetComponent<RayCasting>();
        }

        //set some default values for the voltage. 
        input.voltage = 69;
        input.frequency = 60;
        input.current = 60;
        output.voltage = input.voltage;
        output.frequency = input.frequency;
        output.current = input.current;
    }

    void Update()
    {
        getInput();
        updateOutput();
    }

    //used to draw the gui objects, 
    //for each component we will draw a box containing information on the component if the player
    //is in range.
    void OnGUI()
    {
        //did the player hit an object, and is this object this component?
        if (playerRaycast != null &&
            playerRaycast.InReach == true &&
            playerRaycast.hitObject == gameObject)
        {
            //call the ray cast response function.
            playerRayCastCollisionResponse();
        }
    }

    //an easy function to zero the output
    public void zeroOuput()
    {
        output.current = 0;
        output.voltage = 0;
        output.frequency = 0;
    }

    //this function will be overridden by subclasses.
    //as the component gets input it will change the input in a certain manner,
    public abstract void updateOutput();


    //this function will be overriden by subclasses,
    //this way each object can draw a box containing it's information when the player is in range.
    public abstract void playerRayCastCollisionResponse();
    
    //this function will return the output for this component, 
    //to be used by the next component that uses this component as input.
    public  electricalFlow getOutput()
    {
        return output;
    }

    //gets the input from the input component, so long as it's not null
    public void getInput()
    {
        if (inputComponent != null)
             input =  inputComponent.getOutput();
        
    }
}

