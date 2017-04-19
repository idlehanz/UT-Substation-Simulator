using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class OilTest : ElectricalComponentScript
{

    public override void uniqueStart()
    {
    }

    public override void updateOutput()
    {
    }

    public override void uniqueUpdate()
    {
    }

    public override void onInteract(GameObject interactor)
    {
        if (interactor.tag == "squirrel")
        {
        }
        else if (interactor.tag == "Player")
        {
            //get the currrent held item
            PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();

            //if user is holding the bucket
            if (inventory.currentTool.toolName == "bucket")
            {
                //change bucket color to black
                Renderer r = inventory.currentTool.rigidBody.GetComponent<Renderer>();
                r.material.color = Color.black;

            }
        }
    }

    public override void onDisplayInteractionMessage(GameObject interactor)
    {
        //draw a box containing relevant information about the transoferm
        //if player is holding the bucket, say extracting oil
        PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
        if (inventory.currentTool != null)
        {
            if (inventory.currentTool.toolName == "bucket") //holding bucket
            {
                    GUI.color = Color.white;
                    GUI.Box(new Rect(20, 20, 200, 55), "Press e to extract oil");
            }
            else //not holding the bucket
            {
                GUI.color = Color.white;
                GUI.Box(new Rect(20, 20, 200, 55), "Cannot extract oil.\n Not holding a bucket.");
            }
        }
        else
        {
            GUI.color = Color.white;
            GUI.Box(new Rect(20, 20, 200, 55), "Need the bucket to extract oil.");
        }
    }
}
