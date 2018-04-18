using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;


public class LightningIncidentEvent : MonoBehaviour
{
    public GameObject SimpleLightningBoltPrefab;
    public GameObject transformerObject;
    protected GameObject lightningContainer;
    LeverScript lever;
    public int m_Score;
    bool eventTriggered;


    void SetText()
    {
        //Fetch the score from the PlayerPrefs (set these Playerprefs in another script). If no Int of this name exists, the default is 0.
        m_Score = PlayerPrefs.GetInt("Event", 0);
    }



    public void Start()
    {

        //canCancel = true;
        eventTriggered = false;
        lever = GetComponent<LeverScript>();
        SetText();

        if (m_Score == 1)
        {

            eventTriggered = true;
            beginEvent();

        }
        if (SimpleLightningBoltPrefab == null)
            Debug.Log("ERROR: could not find lightning :(");
    }

    public void Update()
    {
        if (eventTriggered == true)
        {
            if (SimpleLightningBoltPrefab == null)
            {
                eventTriggered = false;
            }
        }
    }

    public void OnGUI()
    {

    }

    public void beginEvent()
    {
        if (eventTriggered == true && SimpleLightningBoltPrefab != null)
        {
            eventTriggered = true;
            StartCoroutine(processTask());

        }

    }

    IEnumerator processTask()
    {
        yield return new WaitForSeconds(1);
        lightningContainer = Instantiate(SimpleLightningBoltPrefab);
        TransformerScript tScript = transformerObject.GetComponent<TransformerScript>();

        tScript.triggerElectricalDamage();
        tScript.startSmoking();
        Destroy(lightningContainer, 2);
        yield return new WaitForSeconds(2);
        eventTriggered = false;
    }

    public void endEvent()
    {
        if (lightningContainer != null)
        {
            Destroy(lightningContainer);
        }
        eventTriggered = false;
    }


}

