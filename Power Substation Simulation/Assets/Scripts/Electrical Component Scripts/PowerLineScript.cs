/*PowerLineScript.cs
 overrides the electrical component script, all this does is act as a carrier between components,
 also takes into account line impedance, the value can be set in the editor */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class PowerLineScript: ElectricalComponentScript
{
    public float lineImpedance = 1;

    public override void uniqueStart()
    {

    }
    public override void uniqueUpdate()
    {

    }


    public override void updateOutput()
    {
        output.current = input.current;
        output.voltage = input.voltage;
        output.frequency = input.frequency;
    }

    
    public override void onInteract(GameObject interactor)
    {
        Debug.Log("interacting with powerLines");
    }
    public override void onDisplayInteractionMessage(GameObject interactor)
    {
        Debug.Log("in power line script");
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Powerline Voltage: " + output.voltage.ToString() +
            "\nPowerline Frequency: " + output.frequency.ToString() + "\nPowerline Current: " + output.current.ToString());


    }
}

