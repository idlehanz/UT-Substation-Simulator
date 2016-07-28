/*
 This script defines the player movement in the game/simulation.*/
using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {



    protected Rigidbody rigidBody;//easy access to the rigid body in our player.
    public bool lockMouseCursor = false;//if we want to lock the mouse we can change it in the editor, but for now it's set to false by default.
    public Transform LookTransform;

    public string noClipKey = "n";//this is the key we will bind the no clip to,


    public bool noClip = false;
    protected bool clipChanged = false;


    //the following variables are used for the old method of movement,
    //should we decide to keep the new method these may be removed.
    public Vector3 Gravity = Vector3.down * 9.81f;
    public float RotationRate = 0.1f;
    public float Velocity = 8;
    public float GroundControl = 1.0f;
    public float AirControl = 0.2f;
    public float JumpVelocity = 5;
    public float GroundHeight = 1.1f;
    private bool jump;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();//get the rigid body for the character
        rigidBody.freezeRotation = true;//freeze rotation so we don't go rolling off

        //set the clipping settings
        if (noClip)
        {
            rigidBody.isKinematic = true;
            rigidBody.detectCollisions = false;
        }
        else
        {
            rigidBody.isKinematic = false;
            rigidBody.detectCollisions = true;
        }

        //set the cursor lock state
        if (lockMouseCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }


	}
	
	void Update() {
        
		jump = jump || Input.GetButtonDown("Jump");
	}
	
	void FixedUpdate() {

        updateMovement();//update our movement
       
        updateNoClipStatus();//update our no clip status,
	}

    /*function to update the no clip status for the player*/
    protected void updateNoClipStatus()
    {
        //check if the key we bound the no clip to was pressed
        if (Input.GetKeyDown(noClipKey))
        {
            //make sure we only change the state once per press
            if (clipChanged == false)
            {
                //swap the clip values.
                noClip = !noClip;
                clipChanged = true;
                rigidBody.isKinematic = !rigidBody.isKinematic;
                rigidBody.detectCollisions = !rigidBody.detectCollisions;
            }
        }
        else
            clipChanged = false;
    }

    //this function will define the movement for the player.
    //for this simulation we will keep the player on a flat plane so the movement is fairly simple,
    //it uses a constant movement speed so the controls feel tigher and we don't feel like we're walking on ice.
    void updateMovement()
    {
      
        //tell the rigid body to move to our new position.
        rigidBody.MovePosition(transform.position + calculateVelocity());
    }

    
    //function to get the velocity for a given update.
    protected Vector3 calculateVelocity()
    {
        //we will use the default key bindings from unity,
        //w and s are locked to the tag Horizontal, and a and d to verticle,
        //teh get Axis raw method returns either a 1, 0, or -1 depending on if a button with that tag is pressed or not
        float horizontalComponent = Input.GetAxisRaw("Horizontal");//should we move forward?
        float verticleComponent = Input.GetAxisRaw("Vertical");//how about strafing?

        //get our horizontal and vertical velocity
        Vector3 horizontalVelocity = Vector3.Cross(transform.up, LookTransform.forward).normalized * horizontalComponent * Time.deltaTime * Velocity;
        Vector3 verticalVelocity = Vector3.Cross(transform.up, -LookTransform.right).normalized * verticleComponent * Time.deltaTime * Velocity;
        return horizontalVelocity + verticalVelocity;//return the velocity
    }


    //this function is the original movement method as defined by the previous teams.
    //kept for prosperity just in case we need to restore it easily.
    void previousMovementMethod()
    {
        // Cast a ray towards the ground to see if the Walker is grounded
        bool grounded = Physics.Raycast(transform.position, Gravity.normalized, GroundHeight);

        // Rotate the body to stay upright
        Vector3 gravityForward = Vector3.Cross(Gravity, transform.right);
        Quaternion targetRotation = Quaternion.LookRotation(gravityForward, -Gravity);
        GetComponent<Rigidbody>().rotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, RotationRate);

        // Add velocity change for movement on the local horizontal plane
        Vector3 forward = Vector3.Cross(transform.up, -LookTransform.right).normalized;
        Vector3 right = Vector3.Cross(transform.up, LookTransform.forward).normalized;
        Vector3 targetVelocity = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * Velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity) - localVelocity;

        // The velocity change is clamped to the control velocity
        // The vertical component is either removed or set to result in the absolute jump velocity
        velocityChange = Vector3.ClampMagnitude(velocityChange, grounded ? GroundControl : AirControl);
        velocityChange.y = jump && grounded ? -localVelocity.y + JumpVelocity : 0;
        velocityChange = transform.TransformDirection(velocityChange);
        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

        // Add gravity
        GetComponent<Rigidbody>().AddForce(Gravity * GetComponent<Rigidbody>().mass);

        jump = false;
    }
	
}