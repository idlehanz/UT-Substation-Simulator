/*Tyler Poff
 InputManager.cs
 This file contains the code for the InputManager class
 the class uses a singleton to provide a global access point for some input parameters,
 any script that creaetes an instance of InputManager will have access to all the same data,
 
  the purpose of the class is to take in id - key pairs,
  the key represents an input button for unity
  the id is what it is represented by (i.e. player forward is w)
  the class provides a way to update the keys and values meaning they can be adjusted,
  
   every class needing player input should go through this class,
   
    by default it will load input.config for default parameters for the keyboard,
    input.param is of the form
    id
    key
    id
    key
    it will construct the pairs from this file.
    
     */
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputManager
{
    //a static variable for our private class of InputParameters
    //this will be shared by every instance, 
    private static InputParameters instance;

    //constructor for InputManager
    //all it does is see if the instance is null, if it is then it creates an instance.
    public InputManager() {
        if (instance == null)
            instance = new InputParameters();
    }

    //this function takes in a string id and checks to see if the key it is bound to is pressed
    //returns true on the frame it's pressed, does not return true if held down
    public bool isKeyEntryPressed(string id)
    {
        return instance.isKeyEntryPressed(id);
    }

    //returns true if the key is pressed, returns true if held
    public bool isKeyEntryDown(string id)
    {

        return instance.isKeyEntryDown(id);
    }

    

    //this takes in a id and string for the key and updates the entry in the table in our InputParameters.
    public void updateKeyEntry(string id, string entry)
    {
        instance.updateKeyEntry(id, entry);
    }


    //reset by creating a new InputParameter class,
    //since our constructor for InputParameters starts by opening input.param and loading in the id-key pairs
    //this will act as a reset mechanism if we don't want to keep any changes we have saved. 
    public void clearInputParameters()
    {
        instance = new InputParameters();
    }


    //if we want to keep the changes we may have made we overwrite our input.param file.
    public void saveInputParameters()
    {
        instance.writeParameters("Input.Param");
    }
    
    //same as clearInput, but under a different name......why idk......I had a good reason
    //just can't remember it.....I'm really tired.....
    public void resetParameters()
    {
        clearInputParameters();

    }

    //a protected inner class,
    //this will keep track of our id - key pairs,
    protected class InputParameters
    {
        //this is the hashtable we will keep the pairs.
        protected Hashtable keyTable = new Hashtable();


        //the constructor for our inner class,
        //all we do is read in the pairs from Input.config
        public InputParameters() {

            readInParameters("Input.config");

        }

        //this function takes in a id and returns if the key bound to the id is true
        //since unity's Input does error checking for bad inputs we don't have to worry about a bad key
        //being sent to Input.GetKeyDown.
       public bool isKeyEntryPressed(string id)
        {
            bool isActive = false;
            string entry = (string)keyTable[id];

            if (entry != null)
            {

                if (Input.GetKeyDown(entry))
                {
                    isActive = true;
                }
            }
            return isActive;
        }

        //function for telling is a key is held down.
        public bool isKeyEntryDown(string id)
        {
            bool isActive = false;
            string entry = (string)keyTable[id];

            if (entry != null)
            {

                if (Input.GetKey(entry))
                {
                    isActive = true;
                }
            }
            return isActive;
        }

        //this function will update a id - key entry
        //if the entry doesn't exist allready then the keyTable will automatically generate it. 
        //if it does exist then it will overwrite it.
        public void updateKeyEntry(string id, string entry)
        {
            
            keyTable[id] = entry;
            string test = (string)keyTable[id];
        }


        //this function will take the current hash table and write it out to a file.
        public void writeParameters(string file)
        {

            string fileContents = "";
            foreach (string key in keyTable.Keys)
            {
                fileContents += String.Format("{0}\n{1}\n", key, keyTable[key]);
                
            }
            System.IO.File.WriteAllText(file, fileContents);

        }

        //read in the key-value paris from the specified file.
        public void readInParameters(string file)
        {
            string text = System.IO.File.ReadAllText(@file);
            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length-1; i += 2)
            {
                string id = lines[i];
                id= id.TrimEnd('\r', '\n');
                string entry = lines[i + 1];
                entry = entry.TrimEnd('\r', '\n');
                updateKeyEntry(id, entry);
            }
        }
    }

    
}