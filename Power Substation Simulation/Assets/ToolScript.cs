using UnityEngine;
using System.Collections;
using System;

public class ToolScript : MonoBehaviour, Interactable {
    
    public string toolName="tool";
    public Vector3 hudPosition = Vector3.zero;
    public Vector3 hudRotation;
    public GameObject parentObject;
    protected Rigidbody rigidBody;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        if (parentObject != null)
        {
            PlayerInteractionScript inventory = parentObject.GetComponent<PlayerInteractionScript>(); ;
            if (inventory != null)
            {
                pickUpTool(parentObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	   if (Input.GetKeyDown("u"))
        {
            dropTool();
        }
	}

    void LateUpdate()
    {
        
    }

    public void interactWith(Interactable interactee)
    {
        interactee.interact(transform.gameObject);

    }

    public void dropTool()
    {
        parentObject = null;
        transform.parent = null;
        rigidBody.useGravity = true;
        rigidBody.detectCollisions = true;
        rigidBody.isKinematic = false;
    }
    public void pickUpTool(GameObject interactor)
    {
        //bad bad bad, but for some reason if I search for the camera in the player it returns null.
        Transform cameraTransform = Camera.main.transform;



        rigidBody.detectCollisions = false;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        parentObject = interactor;
        //transform.position = Vector3.zero;
        transform.position = cameraTransform.position + hudPosition;
        transform.Rotate(cameraTransform.rotation.eulerAngles + hudRotation);
        transform.parent = cameraTransform;




    }



    public void displayInteractionMessage(GameObject interactor)
    {
        if (parentObject == null)
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Press e to pick up");
        }
    }

    public void interact(GameObject interactor)
    {
        if (parentObject == null)
        {
            PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>(); ;
            if (inventory != null)
            {
                pickUpTool(interactor);
                inventory.pickUpTool(this);
            }
            else
            {
                Debug.Log("warning: player inventory null");
            }
        }
        else
        {
            Debug.Log("warning, parent object not null");
        }
        

    }
}
