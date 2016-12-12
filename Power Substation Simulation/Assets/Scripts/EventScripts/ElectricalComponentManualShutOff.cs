using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

class ElectricalComponentManualShutOff : SimulationEvent
{
    public ElectricalComponentScript component;
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
            GUI.Box(new Rect(20, 20, 200, 55), "Press e to enable " + component.tag);
        }
        else
        {

            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Press e to disable " + component.tag);
        }
    }

    public override void endEvent()
    {

        component.setIsDisabled(false);
    }
}

