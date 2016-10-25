﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


class FloodLightScript: ElectricalComponentScript
{

    protected Light light;

    public override void uniqueStart()
    {
        light = GetComponentsInChildren<Light>()[0];

    }
    public override void uniqueUpdate()
    {
        light.intensity = 2.0f* input.voltage / 19;
    }
    //this function will be overridden by subclasses.
    //as the component gets input it will change the input in a certain manner,
    public override void updateOutput()
    {
        output = input;
    }


    //this function will be overriden by subclasses,
    //this way each object can draw a box containing it's information when the player is in range.
    public override void playerRayCastCollisionResponse()
    {
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Powerline Voltage: " + output.voltage.ToString() +
            "\nPowerline Frequency: " + output.frequency.ToString() + "\nPowerline Current: " + output.current.ToString());

    }


}
