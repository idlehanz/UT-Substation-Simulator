using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SquirrelIncidentEvent: System.Object
{
    public float e;
    public static void trigger()
    {
        Debug.Log("test");
    }
}