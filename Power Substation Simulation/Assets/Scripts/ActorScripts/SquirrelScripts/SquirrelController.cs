using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;


public class SquirrelController:MonoBehaviour, Interactable
{
    public Renderer rend;


    public Animator animator;



    //needed for ragdoll.
    protected Rigidbody rigidBody;
    public bool ragdollState = false;
    public GameObject leftHand;
    public GameObject rightHand;
    Rigidbody[] bodies;


    //default path variables.
    public GameObject pathContainer = null;
    protected List<Vector3> path;
    protected int currentPathNode = 0;



    //navigation parameters
    public float rotationSpeed = .01f;
    public float speed = 1.0f;
    public float climbingSpeed = 2.0f;


    protected bool pinned = false;
    public bool alive = true;
    protected bool grabbing = false;

    protected GameObject transformerGameObject=null;
    protected GameObject transformerLead = null;



    //status variables,
    protected bool atTransformer = false;
    protected bool atLead = false;
    protected bool grabbedLead = false;

    


    //SortedDictionary<Collider, Collision> collisionDictionary = new SortedDictionary<Collider, Collision>();
    Dictionary<Collider, Collision> collisionDictionary = new Dictionary<Collider, Collision>();

    public void Start()
    {

        extractPathVectors();
        rigidBody = GetComponent<Rigidbody>();//get the rigid body for the character
        bodies = GetComponentsInChildren<Rigidbody>();
        


        animator = GetComponent<Animator>();
        animator.SetBool("Walking", true);
        animator.SetBool("Climbing", false);
        animator.SetBool("Dead", false);
        animator.SetBool("Grabbing", false);


        

        setRagdollState(ragdollState);
        
    }



    public void Update()
    {
        
        //Debug.Log(rend.material.GetFloat("Charred Amount"));
        if (!atTransformer)
            followPath();
        else if (!atLead)
            findLead();
        else if (!grabbedLead)
        {
            grabbingLead();
        }
        else 
        {
            TransformerScript ts = transformerGameObject.GetComponent<TransformerScript>();
            if (ts.getOutput().voltage!=0)
            {
                Debug.Log("squirrel frying");
                float charAmount = rend.material.GetFloat("_CharAmount");
                charAmount += .05f * Time.deltaTime;
                rend.material.SetFloat("_CharAmount", charAmount);
            }
        }




    }

    public void followPath()
    {
        moveTo(path[currentPathNode]);

        if (Vector3.Distance(transform.position, path[currentPathNode]) < 1.5f)
        {
            if (currentPathNode < path.Count - 1)
                currentPathNode++;

        }



    }

    public void findLead()
    {
        moveTo(transformerLead.transform.position);
    }

    public void grabbingLead()
    {
		Vector3 lookat = transformerLead.transform.position - transform.position;
		lookat.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookat), 1 * Time.deltaTime);
        if (grabbing == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("rig|Grab"))
            {

                Debug.Log("squirrel grabbing");
                grabbing = true;
            }
        }
        if (grabbing == true && animator.GetCurrentAnimatorStateInfo(0).IsName("rig|Grab") == false)
        {
            TransformerScript transformer = transformerGameObject.GetComponent<TransformerScript>();
            
            if (alive == true)
            {
                transformer.interact(transform.gameObject);
                setRagdollState(true);
                Debug.Log("BOOM");
                setHandLocked(true);
                pinned = true;
                alive = false;
                grabbedLead = true;
            }
        }
    }


    public void electrocuting()
    {

    }


    public void moveTo(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Vector3 velocity = direction.normalized * speed * Time.deltaTime;
        velocity.y = 0;

        //transform.rotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity), 1 * Time.deltaTime);
        velocity = transform.forward * speed * Time.deltaTime;
        velocity.y = 0;
        Vector3 rayR = transform.forward;
        Vector3 rayP = transform.position;



        float maxInteractionDistance = 1;
        RaycastHit hit; // Variable reading information about the collider hit.

        // Cast a ray from the center of screen towards where the player is looking.

        rigidBody.useGravity = true;


        animator.SetBool("Walking", true);
        animator.SetBool("Climbing", false);
        animator.SetBool("Dead", false);
        animator.SetBool("Grabbing", false);
        for (int i = 0; i < 3; i++)
        {
            rayP.y += i * .5f;
            Ray r = new Ray(rayP, rayR);
            Debug.DrawRay(rayP, rayR);
            if (Physics.Raycast(r, out hit, maxInteractionDistance))
            {
                velocity = Vector3.zero;
                velocity.y += climbingSpeed * Time.deltaTime;
                rigidBody.useGravity = false;
                if (i > 0)
                {

                    animator.SetBool("Walking", false);
                    animator.SetBool("Climbing", true);
                    animator.SetBool("Dead", false);
                    animator.SetBool("Grabbing", false);
                }

            }
        }







        Vector3 newPosition = transform.position + velocity;
        rigidBody.position = newPosition;
        // rigidBody.MovePosition(newPosition);

        float turnSpeed = 30;
        velocity.y = 0;
        Quaternion dirQ = Quaternion.LookRotation(velocity);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, velocity.magnitude * turnSpeed * Time.deltaTime);
        rigidBody.MoveRotation(slerp);
    }

    public void setCharAmount(float newCharAmount)
    {
        if (rend !=null)
        {
            rend.material.SetFloat("_CharAmount", newCharAmount);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint cp = collision.contacts[0];
        collisionDictionary.Add(collision.collider, collision);
       



    }
    void OnCollisionStay(Collision collision)
    {
        if (collisionDictionary.ContainsKey(collision.collider))
        {
            collisionDictionary[collision.collider] = collision;
        }
        else
            Debug.Log("Could not find in dictionary");
    }
    void OnCollisionExit(Collision collision)
    {

        if (collisionDictionary.ContainsKey(collision.collider))
        {
            collisionDictionary.Remove(collision.collider);
           



        }
        else
            Debug.Log("trying to remove non-existant key");

        
    }



    void OnTriggerEnter(Collider other)
    {
        if (!atTransformer)
        {
            if (other.tag == "Transformer")
            {
                atTransformer = true;
                transformerGameObject = other.gameObject;
                TransformerScript ts = transformerGameObject.GetComponent<TransformerScript>();
                transformerLead = ts.getPositiveLead();

            }
        }
        else if (!atLead)
        {
            
            if (other.gameObject == transformerLead)
            {
                atLead = true;
                
                animator.SetBool("Walking", false);
                animator.SetBool("Climbing", false);
                animator.SetBool("Dead", false);
                animator.SetBool("Grabbing", true);
            }
        }


    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {

    }


    public void setRagdollState(bool newValue)
    {
        Debug.Log("Setting ragdoll to: " + newValue);
        ragdollState = newValue;
        animator.enabled = !newValue;


        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        cc.isTrigger = newValue;


        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {

            rb.isKinematic = !newValue;
            rb.detectCollisions = newValue;
            
            

        }


        rigidBody.isKinematic = newValue;
        
        rigidBody.detectCollisions = true;
    }

    void setHandLocked(bool newHandLockedValue)
    {
        Rigidbody leftHandBody = leftHand.GetComponent<Rigidbody>();
        leftHandBody.isKinematic = newHandLockedValue;

        Rigidbody rightHandBody = rightHand.GetComponent<Rigidbody>();
        rightHandBody.isKinematic = newHandLockedValue;

    }


    void extractPathVectors()
    {
        path = new List<Vector3>();
        if (pathContainer != null)
        {
            foreach (Transform child in pathContainer.transform)
            {
                //child is your child transform
                path.Add(child.transform.position);
            }
        }
        currentPathNode = 1;
        transform.position = path[0];
        //transform.rotation = Quaternion.LookRotation(path[0] - transform.position, Vector3.up);
    }


    public void setNewPath(GameObject newPathContainer)
    {
        pathContainer = newPathContainer;
        extractPathVectors();
        currentPathNode = 1;
        transform.position = path[0];
    }

    public void interact(GameObject interactor)
    {
        if (pinned==true && interactor.tag == "Scraper")
        {
            setHandLocked(false);
            pinned = false;
        }
    }

    public void displayInteractionMessage(GameObject interactor)
    {
        if (pinned == true)
        {
            // GUI.color = Color.white;
            //GUI.Box(new Rect(20, 20, 200, 55), "get the scrapper, press e to remove");
            if (interactor.tag == "Player")
            {
                PlayerInventoryScript inv = interactor.GetComponent<PlayerInventoryScript>();
                if (inv.currentTool != null && inv.currentTool.toolName == "scraper tool")
                {
                    GUI.color = Color.white;
                    GUI.Box(new Rect(20, 20, 200, 55), "Left click to remove");
                }
                else
                {
                    GUI.color = Color.white;
                    GUI.Box(new Rect(20, 20, 200, 55), "get the scrapper tool");
                }
            }
        }
        
    }


    public bool isTransformeredRepaired()
    {
        TransformerScript transformerScript = transformerGameObject.GetComponent<TransformerScript>();
        return (!transformerScript.isDestroyed()&&transformerScript.isDisabled==false);
    }

    public bool isPinned()
    {
        return pinned;
    }
    public bool isAlive()
    {
        return alive;
    }
}

