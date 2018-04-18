using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;


public class SquirrelScript:MonoBehaviour, Interactable
{
    public Animator animator;
    public float rotationSpeed = .01f;

    Rigidbody[] bodies;
    public float speed = 1.0f;
    public float climbingSpeed = 2.0f;


    protected Rigidbody rigidBody;
    public bool ragdollState = false;

    List<Collision> collisionList = new List<Collision>();


    List<Plane> collisionPlanes = new List<Plane>();

    public GameObject leftHand;
    public GameObject rightHand;


    public GameObject pathContainer = null;
    protected List<Vector3> path;
    protected int currentPathNode = 0;

    
    protected bool pinned = false;
    public bool alive = true;
    protected bool grabbing = false;

    protected GameObject transformerGameObject=null;


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
       // Vector3 direction = transform.position+new Vector3(0,0,speed);
        //Vector3 velocity = new Vector3(0,0,speed)*Time.deltaTime;

        Vector3 direction = path[currentPathNode] - transform.position;
        Vector3 velocity = direction.normalized * speed * Time.deltaTime;
        velocity.y = 0;
        if (grabbing == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("rig|Grab"))
            {
                
                Debug.Log("squirrel grabbing");
                grabbing = true;
            }
        }
        
        if (transformerGameObject == null)
        {
            
            move(velocity);

            if (Vector3.Distance(transform.position, path[currentPathNode]) < 1.5f)
            {
                if (currentPathNode < path.Count - 1)
                    currentPathNode++;

            }
        }
        else
        {

            transform.rotation = Quaternion.LookRotation(transformerGameObject.transform.position - transform.position);
            TransformerScript transformer = transformerGameObject.GetComponent<TransformerScript>();
            if (grabbing == true && animator.GetCurrentAnimatorStateInfo(0).IsName("rig|Grab") == false)
            {
               
                if (alive == true)
                {
                    transformer.interact(transform.gameObject);
                    setRagdollState(true);

                    setHandLocked(true);
                    pinned = true;
                    alive = false;
                }
            }
           
            
        }


    }


    public void move(Vector3 velocity)
    {
        //transform.rotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity), 1*Time.deltaTime);
        velocity =transform.forward *speed * Time.deltaTime;
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
        for (int i =0; i<3; i++)
        {
            rayP.y += i * .5f;
            Ray r = new Ray(rayP, rayR);
            Debug.DrawRay(rayP, rayR);
            if (Physics.Raycast(r, out hit, maxInteractionDistance))
            {
                velocity = Vector3.zero;
                velocity.y += climbingSpeed * Time.deltaTime;
                rigidBody.useGravity = false;
                if (i>0)
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
        
       // rigidBody.MovePosition(newPosition);


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
        
       if (other.tag == "Transformer")
        {

            transformerGameObject = other.gameObject;
            animator.SetBool("Walking", false);
            animator.SetBool("Climbing", false);
            animator.SetBool("Dead", false);
            animator.SetBool("Grabbing", true);

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
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "get the scrapper, press e to remove");
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

