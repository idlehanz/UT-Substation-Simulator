using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;


public class SquirrelScript:MonoBehaviour
{
    public Animator animator;

    Rigidbody[] bodies;
    public float speed = 1.0f;


    protected Rigidbody rigidBody;
    public bool ragdollState = false;

    List<Collision> collisionList = new List<Collision>();


    List<Plane> collisionPlanes = new List<Plane>();




    //SortedDictionary<Collider, Collision> collisionDictionary = new SortedDictionary<Collider, Collision>();
    Dictionary<Collider, Collision> collisionDictionary = new Dictionary<Collider, Collision>();

    public void Start()
    {
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
        Vector3 direction = transform.position+new Vector3(0,0,speed);
        Vector3 velocity = new Vector3(0,0,speed)*Time.deltaTime;


        if (ragdollState == false)
             move(velocity);



        //collision.contacts[0];


    }


    public void move(Vector3 velocity)
    {
        //get all registered collisions
        List<Collision> collisions = new List<Collision>();
        collisions.AddRange(collisionDictionary.Values);

        List<Plane> velocityIntersectsPlane = new List<Plane>();
        bool intersected = false;
        foreach( Collision c in collisions)
        {
            //for simplicity, and speed we'll only check the first registered collision point per collision,
            //since for complex objects this 
            Plane collisionPlane = new Plane(c.contacts[0].normal, c.contacts[0].point);
            Ray velocityRay = new Ray(transform.position, velocity.normalized);
            float distance = 0;
            if (collisionPlane.Raycast(velocityRay, out distance))
            {
                if (distance >= .8f)
                    continue;
                
                velocity.y += .5f;
                Vector3 vQ = transform.position + velocity;
                Vector3 vPQ =vQ- c.contacts[0].point;
                float dot = Vector3.Dot(vPQ, collisionPlane.normal.normalized);
                Vector3 vQN = collisionPlane.normal.normalized * dot;

                Vector3 vAnswer = vQ - vQN;
                velocity = vAnswer - transform.position;
                
                intersected = true;
                break;
            }
        }
        


        Vector3 newPosition = transform.position + velocity;
        rigidBody.position = newPosition;
       // rigidBody.MovePosition(newPosition);


    }






    void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint cp = collision.contacts[0];
        collisionDictionary.Add(collision.collider, collision);
        foreach (Rigidbody rb in bodies)
        {
            
        }



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
        setRagdollState(true);
        foreach (Rigidbody rb in bodies)
        {

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


        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !newValue;
            rb.detectCollisions = newValue;

        }


        rigidBody.isKinematic = newValue;
        rigidBody.detectCollisions = !newValue;
    }


}

