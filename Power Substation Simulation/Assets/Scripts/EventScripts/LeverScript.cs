using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public interface interactable
{
    void interact();
    void displayMessageBox();
}


public class LeverScript : MonoBehaviour
{
    public UnityEvent onTriggerEvent;
    public Transform armTransform;
    float x = 0;

    public SquirrelIncidentEvent e;

    void Start()
    {
        onTriggerEvent.Invoke();

    }
    void Update()
    {
        x = .5f;
        armTransform.Rotate(new Vector3(x, 0, 0));

    }

    public void armUpEvent()
    {
        Debug.Log("Arm up");
    }
    public void armDownEvent()
    {
        Debug.Log("Arm Down");
    }
}
