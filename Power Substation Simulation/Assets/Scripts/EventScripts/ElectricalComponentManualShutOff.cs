using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

class ElectricalComponentManualShutOff : SimulationEvent
{
    public ElectricalComponentScript component;
    public int transNum;

    public void Start()
    {
        canCancel = true;
    }
    public void Update()
    {
		if (eventTriggered != component.isDisabled) {
			eventTriggered = component.isDisabled;
		}
    }
    public override void beginEvent()
    {
        component.setIsDisabled(true);
    }
    

    public override void displayMessage()
    {
        if (eventTriggered)
        {

            GUI.color = Color.white;
            GUI.Box(new Rect((Screen.width / 2) - 100, 20, 200, 55), "Press e to enable " + component.tag + " " + transNum);
        }
        else
        {

            GUI.color = Color.white;
            GUI.Box(new Rect((Screen.width / 2) - 100, 20, 200, 55), "Press e to disable " + component.tag + " " + transNum);
        }
    }

    public override void endEvent()
    {

        component.setIsDisabled(false);
    }
}

