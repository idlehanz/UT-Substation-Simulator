/*ElectricalInputModuleScript.cs
 this script will control the electrical input object,
 the object's purpose is to generate a constant stream of energy to be used to feed components.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class ElectricalInputModuleScript : ElectricalComponentScript
{

    
    //electrical values to be set in the editor,
    public float current = 60f;
    public float voltage = 69f;
    public float frequency = 60;


    public override void uniqueStart()
    {

    }
    public override void uniqueUpdate()
    {

    }
    //update the ouput, 
    public override void updateOutput()
    {
        output.current = current;
        output.voltage = voltage;
        output.frequency = frequency;
    }



    public override void onInteract(GameObject interactor)
    {
        Debug.Log("interacting with electrical input module");
    }
    public override void onDisplayInteractionMessage(GameObject interactor)
    {
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "input Voltage: " + output.voltage.ToString() +
            "\ninput Frequency: " + output.frequency.ToString() + "\ninput Current: " + output.current.ToString());


    }
}
