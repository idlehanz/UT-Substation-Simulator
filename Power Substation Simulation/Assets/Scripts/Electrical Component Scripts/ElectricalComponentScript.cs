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

abstract class ElectricalComponentScript : MonoBehaviour, Interactable
{
    public static int id = 0;
    protected string newTag;

    protected electricalFlow output;
    protected electricalFlow input;

    public ElectricalComponentScript inputComponent;

    public bool isDisabled = false;

    public ParticleSystem electricalExplosionParticles;
    public ParticleSystem smokeParticles;

    public WireNode inputNode;
    public WireNode outputNode;

    void Start()
    {
        if (inputComponent!=null && inputNode!=null && inputComponent.outputNode!=null)
        {
            inputNode.setNewNextNode(inputComponent.outputNode);
        }
        //set some default values for the voltage. 
        input.voltage = 69;
        input.frequency = 60;
        input.current = 60;
        output.voltage = input.voltage;
        output.frequency = input.frequency;
        output.current = input.current;
        uniqueStart();
        if (smokeParticles!=null)
        {
            smokeParticles.Stop();
        }
        if (electricalExplosionParticles!=null)
        {
            electricalExplosionParticles.Stop();
        }
    }

    public abstract void uniqueStart();
    public abstract void uniqueUpdate();

    void Update()
    {
        getInput();
        if (isDisabled == false)
        {
            updateOutput();
        }
        else
        {
            zeroOuput();
        }
        uniqueUpdate();
    }

    public void toggleDisabled()
    {
        isDisabled = !isDisabled;
    }
    public void setIsDisabled(bool newIsDisabled)
    {
        isDisabled = newIsDisabled;
    }

    //used to draw the gui objects, 
    //for each component we will draw a box containing information on the component if the player
    //is in range.
    void OnGUI()
    {
        
    }

    public abstract void onInteract(GameObject interactor);
    public abstract void onDisplayInteractionMessage(GameObject interactor);


    public void interact(GameObject interactor) { onInteract(interactor); }
    public void displayInteractionMessage(GameObject interactor) {
        onDisplayInteractionMessage(interactor);
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
        else
        {
            input.voltage = 0;
            input.frequency = 0;
            input.current = 0;
        }
        
    }
}

