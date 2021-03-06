﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Egg : MonoBehaviour
{

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

    public bool placeInKeyHole(Keyhole keyhole)
    {
        //If egg can fit into keyhole, it will teleport onto it.
        if(keyhole.InsertEgg(this))
        {
            if(keyhole.insertPoint)
            {
                transform.position = keyhole.insertPoint.position;
                transform.rotation = keyhole.insertPoint.rotation;
            }
            else
            {
                transform.position = keyhole.transform.position;
                transform.rotation = keyhole.transform.rotation;
            }
            sourceKeyhole = keyhole;
            return true;
        }
        return false;
    }

    public void removeFromKeyHole()
    {
        sourceKeyhole.RemoveEgg();
        sourceKeyhole = null;
    }
}
