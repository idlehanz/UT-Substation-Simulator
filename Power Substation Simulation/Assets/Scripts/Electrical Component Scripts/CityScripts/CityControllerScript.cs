using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CityControllerScript : MonoBehaviour
{

    protected CityInputScript[] inputs;
    public Renderer rend;
    protected Light[] lights;
    public Texture2D lightsOnTexture;
    public Texture2D lightsOffTexture;


    protected Renderer[] renderors;
    public bool powered = true;

    public void Start()
    {

        inputs = GetComponents<CityInputScript>();
        lights = GetComponentsInChildren<Light>();
        renderors = GetComponentsInChildren<Renderer>();

        if (inputs.Length ==0)
        {
            
        }

    }

    public void Update()
    {
        bool hasPower = true;
        foreach (CityInputScript cs in inputs) {
            if (cs.getOutput().voltage == 0) {
				hasPower = false;
				break;
            }
		}

		if (powered == false && hasPower == true) {
			lightsOn ();
			powered = true;
		} else if (powered == true && hasPower == false) {
			lightsOff ();
			powered = false;
		}
    }


    public void lightsOff()
    {
        foreach (Renderer r in renderors)
        {
            r.material.color = Color.black;
        }
        foreach (Light l in lights)
        {
            l.enabled = false;
        }
    }

    public void lightsOn()
    {
        foreach (Renderer r in renderors)
        {
            r.material.color = Color.white;
        }
        foreach (Light l in lights)
        {
            l.enabled = true;
        }
    }


}

