using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for handling the area where eggs are put into
public class Keyhole : MonoBehaviour {

    //Minimum size needed to fit in input
    [SerializeField]
    private int maxSize;
    [SerializeField]
    private Puzzle sourcePuzzle;
    public bool eggInserted = false;

	// Use this for initialization
	void Start () {
        sourcePuzzle = GetComponentInParent<Puzzle>() != null ? GetComponentInParent<Puzzle>() : null;
        if(!sourcePuzzle)
        {
            Debug.LogError(gameObject.name + " must be a child of a puzzle!");
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
            sourcePuzzle.ValidateDoor();
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
        sourcePuzzle.ValidateDoor();
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
