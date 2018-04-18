using UnityEngine;
using System.Collections;

public class PlayerInventoryScript : MonoBehaviour {

    public ToolScript currentTool=null;



	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void interactItem(Interactable item)
    {
        if (currentTool!=null)
        {
            currentTool.interactWith(item);
        }
    }



    public void pickUpTool(ToolScript tool)
    {
        if (currentTool == null)
            currentTool = tool;
        else
        {
            currentTool = null;
        }
    }
}
