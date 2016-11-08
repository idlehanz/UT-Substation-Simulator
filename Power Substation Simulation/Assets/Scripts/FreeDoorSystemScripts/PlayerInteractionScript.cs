using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;


class PlayerInteractionScript : MonoBehaviour
{

    public float maxInteractionDistance = 5.0f;
    protected Interactable closestInteractable = null;
    protected PlayerInventoryScript inventory = null;

    protected InputManager inputManager = new InputManager();

    void Start()
    {
        inventory = GetComponent<PlayerInventoryScript>();

        


    }

    //UPDATE FUNCTION
    void Update()
    {
        getClosestInteractable();
        
        
        
        if (closestInteractable != null)
        {
            if (inputManager.isKeyEntryPressed("Player Interact"))
            {
                closestInteractable.interact(transform.gameObject);
            }
            if (Input.GetMouseButtonDown(0))
            {
                inventory.interactItem(closestInteractable);
            }

        }
    }

    void getClosestInteractable()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));

        RaycastHit hit; // Variable reading information about the collider hit.
        closestInteractable = null;
        // Cast a ray from the center of screen towards where the player is looking.
        if (Physics.Raycast(ray, out hit, maxInteractionDistance))
        {

            GameObject go = hit.transform.gameObject;
            closestInteractable = getInteractableScriptFromGameObject(go, ray);

            


        }
    }

    Interactable getInteractableScriptFromGameObject(GameObject go, Ray originalRayCast)
    {
        Interactable interactableScript = null;
        Interactable parentInteractable = go.GetComponent<Interactable>();
        if (parentInteractable != null)
        {
            interactableScript = parentInteractable;
        }
        else
        {
            foreach (Transform childrenTransform in go.transform)
            {
               //TODO: I want to check if a child of a gameobject has an interactable just in case a larger object does not
               //however I don't know the best way to do this, so I'm leaving it blank for now. as it is it will work so long as an object
               //only has a single interactable on it.


            }
        }
        go.GetComponentInChildren<Interactable>();

        return interactableScript;
    }

    void OnGUI()
    {
        if (closestInteractable!=null)
        {
            closestInteractable.displayInteractionMessage(transform.gameObject);
        }
    }
}
