/*TransformerScript.cs
 this script controls the transformer objects in our simulation.
 it inherits from ElectricalComponentScript, */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class TransformerScript : ElectricalComponentScript
{
	public float step = 3.6315f;
	public bool damaged = false;
	AudioSource explosion;
	public Light lt;
	public float c;


    protected SquirrelScript squirrel = null;

    public override void uniqueStart()
	{

	}
	public override void uniqueUpdate()
	{

	}

	//update the output.
	public override void updateOutput()
	{
        if(damaged == false)
        {
            GameObject tmpasadf = GameObject.Find("LightColorShift");
            lt = tmpasadf.GetComponent<Light>();
            c = 1;
            output.current = input.current;
            output.voltage = input.voltage / step;
            output.frequency = input.frequency;
            lt.color = Color.red;


        }
        else
        {

            output.current = 0;
            output.voltage = 0;
            output.frequency = 0;
            c = output.voltage;
            lt.color = Color.green;
        }
    }



    public void triggerElectricalDamage()
    {
        if (damaged == false)
        {
            damaged = true;
            if (explosion == null)
            {
                explosion = GetComponent<AudioSource>();

            }
            explosion.Play();
            if (electricalExplosionParticles != null)
            {
                electricalExplosionParticles.Play();
            }
            Debug.Log("transformer damaged");
        }

    }





    public bool isDestroyed()
    {
        return damaged;
    }




    public override void onInteract(GameObject interactor)
    {
        if (interactor.tag == "squirrel")
        {
            triggerElectricalDamage();
            squirrel = interactor.GetComponent<SquirrelScript>();
        }
        else
            if (squirrel.isPinned() == false)
				damaged = false;

    }
    public override void onDisplayInteractionMessage(GameObject interactor)
    {
        //draw a box containing relevant information about the transformer.
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Transformer Voltage: " + output.voltage.ToString() + "\n" +
                "Transformer Frequency: " + output.frequency.ToString() + "\nTransformer Current: " + output.current.ToString());



    }
}
