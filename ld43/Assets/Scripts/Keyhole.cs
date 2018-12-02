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

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Egg>() != null)
        {
            //other.GetComponent<Egg>().placeInKeyHole(this);
            checkInput(other.GetComponent<Egg>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Egg>() != null)
        {
            correctInsertedEgg = false;
            sourceDoor.ValidateDoor();
            other.GetComponent<Egg>().inKeyHole = false;
            Debug.Log("Egg exited! " + correctInsertedEgg);
        }
    }

}
