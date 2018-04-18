using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuscript : MonoBehaviour {

    public void selectLightningEvent()
    {
        Debug.Log("HERE");
        SceneManager.LoadScene("Substation");
        PlayerPrefs.SetInt("Event", 1);
    }
    public void selectCritterEvent()
    {
        SceneManager.LoadScene("Substation");
        PlayerPrefs.SetInt("Event", 2);
    }
    public void selectOilEvent()
    {
        SceneManager.LoadScene("Substation");
        PlayerPrefs.SetInt("Event", 3);
    }
    public void selectUAVEvent()
    {
        SceneManager.LoadScene("Substation");
        PlayerPrefs.SetInt("Event", 4);
    }

    public void endGame()
    {
        Application.Quit();
    }
}
