using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public abstract class SimulationEvent: MonoBehaviour
{
    protected bool eventTriggered;
    protected bool canCancel=false;
    public abstract void beginEvent();
    public abstract void cancelEvent();
    public abstract void endEvent();
    public abstract void displayMessage();
    public bool canCancelEvent() { return canCancel; }
    public bool isEventTriggered() { return eventTriggered; }
}

