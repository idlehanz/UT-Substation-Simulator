using UnityEngine;
using System.Collections;

public class SquirrelWalk : MonoBehaviour {

	public float speed = 1.5f;
	public float distance = 40f;
    Animator animator;
    public bool ragdollState = false;
    public bool handsLocked = false;

    Rigidbody[] bodies;

    public GameObject leftHand;
    public GameObject rightHand;
    

    // Use this for initialization
    void Start () {
        bodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("Walking", true);
        animator.SetBool("Climbing", false);
        animator.SetBool("Dead", false);
        animator.SetBool("Grabbing", false);
        setRagdollState(ragdollState);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("y"))
        {
            setRagdollState(!ragdollState);
            setHandLocked(!handsLocked);
            
        }
	}


    void setHandLocked(bool newHandLockedValue)
    {
        handsLocked = newHandLockedValue;
        Rigidbody leftHandBody = leftHand.GetComponent<Rigidbody>();
        leftHandBody.isKinematic = newHandLockedValue;

        Rigidbody rightHandBody = rightHand.GetComponent<Rigidbody>();
        rightHandBody.isKinematic = newHandLockedValue;

    }
    

    void setRagdollState(bool newValue)
    {
        Debug.Log("Setting ragdoll to: " + newValue);
        ragdollState = newValue;
        animator.enabled = !newValue;


        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {
            if (rb.tag == "squirrel")
            {
                rb.isKinematic = !newValue;
                rb.detectCollisions = newValue;
            }
            else
            {
                rb.isKinematic = newValue;
                rb.detectCollisions = !newValue;
            }
        }
    }
}
