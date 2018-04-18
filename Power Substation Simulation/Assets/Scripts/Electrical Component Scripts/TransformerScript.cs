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
    public bool shorting = false;
    public float shortTime = 10;
    protected float startShortTime = -1;
	AudioSource explosion;
	AudioSource buzz;
	public Light lt;
	public float c;
    public float pushBackForce = 150;

    public GameObject positiveLead;
    public GameObject negativeLead;
    

    protected SquirrelController squirrel = null;

    public override void uniqueStart()
	{
	}

	public override void uniqueUpdate()
	{
		if (damaged) {
			isDisabled = true;
		}

        TransformerScript []scripts  = GetComponents<TransformerScript>();
        bool badTransformer = false;

        foreach (TransformerScript s in scripts)
        {
            if (s.output.voltage == 0)
            {
                badTransformer = true;
                break;
            }
        }

        if (badTransformer)
        {
            GameObject tmpasadf = GameObject.Find("LightColorShift");
            if (tmpasadf != null)
                lt = tmpasadf.GetComponent<Light>();
            else
                Debug.Log("could not find light object");
            if (lt != null)
                lt.color = Color.green;
            lt.color = new Color(0, 1, 0);
        }

        if (output.voltage != 0)
        {
            GameObject tmpasadf = GameObject.Find("LightColorShift");
            if (tmpasadf != null)
                lt = tmpasadf.GetComponent<Light>();
            else
                Debug.Log("could not find light object");

            lt.color = new Color(1, 0, 0);
        }
    }

	//update the output.
	public override void updateOutput()
	{
       
        output.current = input.current;
        output.voltage = input.voltage / step;
        output.frequency = input.frequency;
		if (shorting == true) {
			if (startShortTime < 0) {
				startShortTime = Time.realtimeSinceStartup;
			}
			output.voltage += UnityEngine.Random.value * 8;

			if (Time.realtimeSinceStartup > startShortTime + shortTime) {
				shorting = false;

				triggerElectricalDamage ();
				Debug.Log ("electrical damage");
			}
		} else if (damaged == false) {
			c = 1;
			output.current = input.current;
			output.voltage = input.voltage / step;
			output.frequency = input.frequency;   
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
				//GameObject.Find ("TransformerBuzz").GetComponent<AudioSource>().Pause();
            }
            
            Debug.Log("transformer damaged");
        }

    }

    public void startSmoking()
    {
        if (smokeParticles != null)
        {
            smokeParticles.Play();
        }
    }

    public bool isDestroyed()
    {
        return damaged;
    }


    public GameObject getPositiveLead() { return positiveLead;}
    public GameObject getNegativeLead() { return negativeLead; }


    public override void onInteract(GameObject interactor)
    {
        if (interactor.tag == "squirrel")
        {
            startSmoking();
            shorting = true;
            squirrel = interactor.GetComponent<SquirrelController>();
        }
        else if (interactor.tag == "Player")
        {
            //get the currrent held item
            PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
			if (squirrel != null && squirrel.isPinned () == false || damaged == true ) {
				damaged = false;
				smokeParticles.Stop ();
			}
            else if (output.voltage != 0)
            {
                Vector3 velocity = interactor.transform.position - transform.position;

                velocity.y += 1.5f;
                velocity *= pushBackForce;
                PlayerMovement pm = interactor.GetComponent<PlayerMovement>();
                pm.addForce(velocity);
            }
            //if user is holding the bucket
            else if (inventory.currentTool.toolName == "bucket" && output.voltage == 0)
            {
                //change bucket color to black
                Renderer r = inventory.currentTool.rigidBody.GetComponent<Renderer>();
                r.material.color = Color.black;
            }

        }
    }

    public override void onDisplayInteractionMessage(GameObject interactor)
    {
        PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
        //draw a box containing relevant information about the transformer.
        if (squirrel != null && squirrel.isPinned())
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "remove squirrel");
        }
        else if (damaged)
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Press 'e' to repair");
        }
        else if (inventory.currentTool != null)
        {
            if (inventory.currentTool.toolName == "bucket" && output.voltage == 0)
            {
                GUI.color = Color.white;
                GUI.Box(new Rect(20, 20, 200, 55), "Press e to extract oil");
            }
            else
            {
                GUI.color = Color.white;
                GUI.Box(new Rect(20, 20, 200, 55), "Transformer Voltage: " + output.voltage.ToString() + "\n" +
                        "Transformer Frequency: " + output.frequency.ToString() + "\nTransformer Current: " + output.current.ToString());
            }
        }
        else
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Transformer Voltage: " + output.voltage.ToString() + "\n" +
                    "Transformer Frequency: " + output.frequency.ToString() + "\nTransformer Current: " + output.current.ToString());
        }
    }
}
