using UnityEngine;
using System.Collections;

public class SquirrelWalk : MonoBehaviour {

	public float speed = 1.5f;
	public float distance = 40f;
    Animator animator;
    public bool ragdollState = false;

    // Use this for initialization
    void Start () {
        setRagdollState(ragdollState);

        animator = GetComponent<Animator>();
        animator.SetBool("Walking", false);
        animator.SetBool("Climbing", false);
        animator.SetBool("Dead", true);
        animator.SetBool("Grabbing", false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}





    void setRagdollState(bool newValue)
    {
        ragdollState = newValue;
        //Get an array of components that are of type Rigidbody
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !newValue;
        }
    }
}
