using UnityEngine;
using System.Collections;

public class BoxInteractionScript : MonoBehaviour,Interactable {

    GameObject heldBy = null;
    bool held = false;
    public float vel = 5.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (held ==true)
        {

            float horizontalComponent = Input.GetAxisRaw("Horizontal");//should we move forward?
            float verticleComponent = Input.GetAxisRaw("Vertical");//how about strafing?

            Vector3 horizontalVelocity = Vector3.Cross(transform.up, heldBy.transform.forward).normalized  *vel;
            Vector3 verticalVelocity = Vector3.Cross(new Vector3(0.0f,1.0f,0.0f), -heldBy.transform.right).normalized  * vel;
            Vector3 velocity =  verticalVelocity;

            transform.position = heldBy.transform.position+ velocity;
            transform.rotation = heldBy.transform.rotation;
        }
	}

    public void interact(GameObject interactor)
    {
        if (held == false)
        {
            heldBy = interactor;
            held = true;
        }
        else
        {
            held = false;
        }
    }
    public void displayInteractionMessage(GameObject interactor)
    {

    }
}
