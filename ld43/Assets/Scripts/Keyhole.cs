using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for handling the area where eggs are put into
public class Keyhole : MonoBehaviour {

    //Minimum size needed to fit in input
    [SerializeField]
    private int maxSize;
    [SerializeField]
    private Door sourceDoor;
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

    //Attempts to insert egg if it is big enough
    public bool InsertEgg(Egg egg)
    {
        if(!eggInserted && validateSize(egg))
        {
            eggInserted = true;
            sourceDoor.ValidateDoor();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveEgg()
    {
        eggInserted = false;
        sourceDoor.ValidateDoor();
    }

    public bool validateSize(Egg egg)
    {
        if(maxSize >= egg.size)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
