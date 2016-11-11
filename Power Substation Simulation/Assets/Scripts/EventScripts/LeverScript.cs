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

    public Light greenLight;
    public Light redLight;
    public float lightIntensity = 8;


    public bool greenOnActive = true;

    protected float x = 0;

    protected bool leverUp = true;

    protected Vector3 upArmRotation = new Vector3(0, 0, 0);
    protected Vector3 downArmRotation = new Vector3(73.434f, 2.9074f, -176.9f);
    protected Vector3 targetRotation;
    public float armSwitchSpeed = 5f;

    void Start()
    {
        redLight.intensity = 0;
        greenLight.intensity = lightIntensity;
        upArmRotation = transform.forward*2 ;
        downArmRotation = transform.forward * 2 + transform.up * 2;
        if (leverEvent == null)
        {
            leverEvent = GetComponent<SimulationEvent>();
        }
        targetRotation = upArmRotation;
    }
    void Update()
    {
        armTransform.localRotation=Quaternion.Lerp(armTransform.localRotation, Quaternion.LookRotation(targetRotation), armSwitchSpeed* Time.deltaTime);
        
        if (leverEvent.isEventTriggered())
        {
            targetRotation = downArmRotation;
            if (greenOnActive)
                activateGreenLight();
            else
                activateRedLight();

        }
        else
        {
            targetRotation = upArmRotation;
            if (!greenOnActive)
                activateGreenLight();
            else
                activateRedLight();
        }

    }
    void activateGreenLight()
    {
        greenLight.intensity = lightIntensity;
        redLight.intensity = 0;
    }
    void activateRedLight()
    {
        redLight.intensity = lightIntensity;
        greenLight.intensity = 0;
    }
    
    

    public void interact(GameObject interactor)
    {
        if (interactor.tag == "Player")
        {
            if (leverEvent.isEventTriggered() == false)
            {
                leverEvent.beginEvent();
                leverUp = false;
                
            }
            else if (leverEvent.canCancelEvent()==true)
            {
                leverEvent.endEvent();
                leverUp = true;
            }
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
