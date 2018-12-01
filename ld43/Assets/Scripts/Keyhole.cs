using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour {

    //Minimum size needed to fit in input
    [SerializeField]
    private int maxSize;
    //Check if input has an egg inside of it
    private bool inputFilled = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkInput(Egg egg)
    {
        inputFilled = maxSize >= egg.size ? true : false;
    }
}
