using UnityEngine;
using System.Collections;

public class PlayerInventoryScript : MonoBehaviour {

    public ToolScript currentTool;



	// Use this for initialization
	void Start () {
	    if (currentTool!=null)
        {
            ToolScript toolScript = currentTool.GetComponent<ToolScript>();
            if (toolScript!=null)
            {
                toolScript.interact(gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void pickUpTool(ToolScript tool)
    {
        if (currentTool == null)
            currentTool = tool;
        else
        {
            currentTool.dropTool();
            currentTool = tool;
        }
    }
}
