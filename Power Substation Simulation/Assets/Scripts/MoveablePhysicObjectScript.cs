using UnityEngine;
using System.Collections;

public class MoveablePhysicObjectScript : MonoBehaviour,Interactable {

    GameObject heldBy = null;
    bool held = false;
    public float horizontalDistance = .5f;
    public float verticalDistance = 1;
    Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
	    rigidBody = GetComponent<Rigidbody>(); ;
    }
	
	// Update is called once per frame
	void Update () {
	    if (held ==true)
        {//bad bad bad, but for some reason if I search for the camera in the player it returns null.
            Transform cameraTransform = Camera.main.transform;



            rigidBody.detectCollisions = false;
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;



            Vector3 horizontalVelocity = Vector3.Cross(cameraTransform.up, cameraTransform.transform.forward).normalized * horizontalDistance;
            Vector3 verticalVelocity = Vector3.Cross(cameraTransform.up, -cameraTransform.transform.right).normalized * verticalDistance;
            Vector3 velocity = verticalVelocity + horizontalVelocity;

            Vector3 hudPosition = cameraTransform.position + velocity;
            transform.position = hudPosition;





            //transform.Rotate(cameraTransform.rotation.eulerAngles + hudRotation);
            //transform.rotation.SetEulerAngles(cameraTransform.rotation.eulerAngles + hudRotation);
            transform.rotation = cameraTransform.rotation;
            
            transform.parent = cameraTransform;


        }
        else
        {
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
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
        GUI.color = Color.white;
        GUI.Box(new Rect(20, 20, 200, 55), "Press e to pick up");
    }
}
