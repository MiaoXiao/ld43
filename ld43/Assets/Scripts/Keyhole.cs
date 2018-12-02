using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for handling the area where eggs are put into
public class Keyhole : MonoBehaviour {

    //Minimum size needed to fit in input
    [SerializeField]
    private int maxSize;
    //Check if input has the correct egg inside of it
    public bool correctInsertedEgg = false;
    [SerializeField]
    private Door sourceDoor;
    private Egg currentEgg; 
    public bool eggInserted = false;

	// Use this for initialization
	void Start () {
        sourceDoor = GetComponentInParent<Door>() != null ? GetComponentInParent<Door>() : null;
        if(!sourceDoor)
        {
            Debug.LogError(gameObject.name + " must be a child of a door!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkInput(Egg egg)
    {
        if(maxSize >= egg.size)
        {
            correctInsertedEgg = true;
            sourceDoor.ValidateDoor();
        }
        else
        {
            correctInsertedEgg = false;
        }
        Debug.Log("Input Filled: " + correctInsertedEgg);
    }

    public void InsertEgg(Egg egg)
    {
        if(!eggInserted)
        {
            checkInput(egg);
            currentEgg = egg;
            eggInserted = true;
        }
    }

    public void RemoveEgg()
    {
        correctInsertedEgg = false;
        currentEgg = null;
        eggInserted = false;
        sourceDoor.ValidateDoor();
        Debug.Log("Egg exited! " + correctInsertedEgg);
    }

}
