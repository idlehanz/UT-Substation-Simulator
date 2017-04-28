using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ToolScript : MonoBehaviour, Interactable {
    
    public string toolName= "tool";
    public float horizontalDistance = .5f;
    public float verticalDistance = 1;
    public Vector3 hudPosition = new Vector3(.5f, 1, 1);

    public Vector3 hudRotation;
    public GameObject parentObject;
    public Rigidbody rigidBody;

	public bool isOilDelivered = false;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        if (parentObject != null)
        {
            PlayerInteractionScript inventory = parentObject.GetComponent<PlayerInteractionScript>();

            if (inventory != null)
            {
                pickUpTool(parentObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("u")) {
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

        //determines if the object needs to be removed (sent to the lab)
		if (rigidBody.GetComponent<Renderer>().material.color == Color.black)
        {
			isOilDelivered = true;
			Invoke ("displayMsg", 3);
        }
    }

	public void displayMsg() {
		isOilDelivered = false;
		rigidBody.gameObject.SetActive (false); // remove bucket of oil from scene
	}

	public void OnGUI() {
		if (isOilDelivered == true) {
			GUI.color = Color.white;
			GUI.Box (new Rect (20, 20, 360, 30), "You have successfully delivered the oil sample to the lab!");
		}
	}

    public void pickUpTool(GameObject interactor)
    {
        //bad bad bad, but for some reason if I search for the camera in the player it returns null.
        Transform cameraTransform = Camera.main.transform;



        rigidBody.detectCollisions = false;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        parentObject = interactor;

        

        Vector3 horizontalVelocity = Vector3.Cross(cameraTransform.up, cameraTransform.transform.forward).normalized * hudPosition.x;
        Vector3 verticalVelocity = Vector3.Cross(cameraTransform.up, -cameraTransform.transform.right).normalized * hudPosition.y;
        Vector3 velocity = verticalVelocity+ horizontalVelocity;
        Debug.Log(velocity);

        Vector3 hp = cameraTransform.position+velocity;
        transform.position = hp;


        //transform.Rotate(cameraTransform.rotation.eulerAngles + hudRotation);
        //transform.rotation.SetEulerAngles(cameraTransform.rotation.eulerAngles + hudRotation);
        transform.rotation = cameraTransform.rotation;
        transform.Rotate(hudRotation);
        transform.parent = cameraTransform;
    }

    public void displayInteractionMessage(GameObject interactor)
    {
        if (parentObject == null)
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Press e to pick up " + toolName);
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
