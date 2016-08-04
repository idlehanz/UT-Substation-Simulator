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

    //update the output.
    public override void updateOutput()
    {
        output.current = input.current ;
        output.voltage = input.voltage / step;
        output.frequency = input.frequency;
    }

    /*if the players ray cast hits us. this function will be called*/
    public override void playerRayCastCollisionResponse()
    {
        //draw a box containing relevant information about the transformer.
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Transformer Voltage: " + output.voltage.ToString() + "\n" +
                "Transformer Frequency: " + output.frequency.ToString() + "\nTransformer Current: " + output.current.ToString());


    }
}

