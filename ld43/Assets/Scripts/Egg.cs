using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    //Id used for server upload
    [SerializeField]
    public int eggId;
    //Size of egg
    [SerializeField]
    public int size;

    public bool inKeyHole = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void placeInKeyHole(Keyhole keyhole)
    {
        transform.position = keyhole.transform.position;
    }
}
