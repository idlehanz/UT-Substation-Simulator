using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CityInputScript : ElectricalComponentScript
{
    public override void onDisplayInteractionMessage(GameObject interactor)
    {
    }

    public override void onInteract(GameObject interactor)
    {
    }

    public override void uniqueStart()
    {
    }

    public override void uniqueUpdate()
    {
    }

    public override void updateOutput()
    {
        output = input;
    }
}

