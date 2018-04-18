using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class CrispyCritterMenuTrigger : MonoBehaviour {




    //a reference to our squirrel prefab game object
    public GameObject squirrelPrefab;

    public int m_Score;
    public bool eventTriggered;
    public List<GameObject> squirrelPaths;

    protected int currentPath = 1;
    protected int maxPath = 0;

    protected GameObject squirrelObject;

    protected SquirrelController squirrelScript;

    void SetText()
    {
        //Fetch the score from the PlayerPrefs (set these Playerprefs in another script). If no Int of this name exists, the default is 0.
        m_Score = PlayerPrefs.GetInt("Event", 0);
    }


    public void Start()
    {
        SetText();
        eventTriggered = false;
        if (squirrelPrefab == null)
            Debug.Log("ERROR: squirrel prefab not found for squirrel incident event");
        if (squirrelPaths == null)
            Debug.Log("ERROR: squirrel path not found for squirrel incident event");
        maxPath = squirrelPaths.Count();


        if (m_Score == 2)
        {
            eventTriggered = true;
            if (eventTriggered == true)
            {
                if (squirrelObject == null)
                {
                    eventTriggered = false;
                }
                if (squirrelScript != null && squirrelScript.isAlive() == false && squirrelScript.isPinned() == false && squirrelScript.isTransformeredRepaired() == true)
                {
                    Destroy(squirrelObject);
                    squirrelScript = null;
                    eventTriggered = true;
                }
                Debug.Log("Event about to run");
                beginEvent();
            }
        }
    }
    public void Update()
    {

    }

    public void beginEvent()
    {
        if (squirrelPrefab != null && squirrelPaths != null)
        {
            Debug.Log("HERE");

            squirrelObject = Instantiate(squirrelPrefab);


            squirrelObject.GetComponent<SquirrelController>().setNewPath(squirrelPaths[currentPath]);
            currentPath = (currentPath + 1) % maxPath;
            squirrelScript = squirrelObject.GetComponent<SquirrelController>();
        }

    }


}
