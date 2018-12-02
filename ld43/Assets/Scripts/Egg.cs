using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    //Id used for server upload
    public int eggId;
    //Size of egg
    public int size;

    public Keyhole sourceKeyhole;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void placeInKeyHole(Keyhole keyhole)
    {
        transform.position = keyhole.transform.position;
        keyhole.InsertEgg(this);
        sourceKeyhole = keyhole;
    }

    public void removeFromKeyHole()
    {
        sourceKeyhole.RemoveEgg();
        sourceKeyhole = null;
    }
}
