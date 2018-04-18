using UnityEngine;
using System.Collections;

public class MoveablePhysicObjectScript : MonoBehaviour, Interactable
{

    GameObject heldBy = null;
    bool held = false;
    public float vel = 1.0f;
    Rigidbody rigidBody;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (held == true)
        {
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;


            float horizontalComponent = Input.GetAxisRaw("Horizontal");//should we move forward?
            float verticleComponent = Input.GetAxisRaw("Vertical");//how about strafing?

            Vector3 horizontalVelocity = Vector3.Cross(transform.up, heldBy.transform.forward).normalized * vel;
            Vector3 verticalVelocity = Vector3.Cross(transform.up, -heldBy.transform.right).normalized * vel;
            Vector3 velocity = verticalVelocity;// + horizontalVelocity;

            //rigidBody.MovePosition(heldBy.transform.position+ velocity);
            transform.position = heldBy.transform.position + velocity;
            transform.rotation = heldBy.transform.rotation;

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
            Transform t = Camera.main.transform;
            heldBy = t.gameObject;
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