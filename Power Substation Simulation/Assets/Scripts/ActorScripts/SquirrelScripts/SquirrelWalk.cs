using UnityEngine;
using System.Collections;

public class SquirrelWalk : MonoBehaviour {

	public float speed = 1.5f;
	public float distance = 40f;
    Animator animator;
    public bool ragdollState = false;

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();
        animator.SetBool("Walking", false);
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
        }
	}





    void setRagdollState(bool newValue)
    {
        Debug.Log("Setting ragdoll to: " + newValue);
        ragdollState = newValue;
        animator.enabled = !newValue;
        //Get an array of components that are of type Rigidbody
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
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
                
                rb.detectCollisions = !newValue;
            }
        }
    }
}
