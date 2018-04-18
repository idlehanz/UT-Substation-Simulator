/*BreakerScript.cs
 this script controls the breaker object in the substation simulation.
 it inherits from electricalComponent script, 

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class BreakerScript : ElectricalComponentScript
{

    public float threshhold = 0f;
    public float transmissionBus = 69f;
    protected bool breakerTripped = false;

    public override void uniqueStart()
    {

    }
    public override void uniqueUpdate()
    {

    }


    public override void updateOutput()
    {
        if (!breakerTripped)
        {
            output.voltage = input.voltage;
            output.current = input.current;
            output.frequency = input.frequency;

            //ensure voltage is within 5% of accepted transmissionBus range
            if ((output.voltage >= 1.05 * transmissionBus) || (output.voltage <= .95 * transmissionBus))
            {
                Debug.Log("voltage tripped " + output.voltage);
                tripBreaker();
            }

            //ensure frequency is within .01% of 60 hertz
            else if ((output.frequency >= 60.0001) || (output.frequency <= 59.9999))
            {
                Debug.Log("frequency trip " + output.frequency);
                tripBreaker();
            }

            //ensure current is within threshold
            else if (output.current > threshhold)
            {
                //BreakerTrip ();
            }
        }
        else
        {
            zeroOuput();
        }
        
    }

    //this function handles what happens when a breaker is tripped.
    //
    public void tripBreaker()
    {
        Debug.Log("Breaker Tripped");
        //set the breaker to trip so we always send out zero output in future updates, then zero the ouput
        breakerTripped = true;
		GetComponentInChildren<AudioSource> ().Pause();
        zeroOuput();
    }
   
    //this function handles the resetting of the breaker,
    //when the user is close enough they could be prompted to reset the breaker.
    public void resetBreaker()
    {
        breakerTripped = false;
    }

    


    public override void onInteract(GameObject interactor)
    {
        Debug.Log("interacting with breaker");
        if (breakerTripped)
            resetBreaker();
        else
            tripBreaker();
    }
    public override void onDisplayInteractionMessage(GameObject interactor)
    {
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Powerline Voltage: " + output.voltage.ToString() +
            "\nPowerline Frequency: " + output.frequency.ToString() + "\nPowerline Current: " + output.current.ToString());


    }
}


