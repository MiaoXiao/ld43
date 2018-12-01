using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for a door
public class Door : MonoBehaviour {

    public List<Keyhole> keyholes;
    public List<SlidingObject> doors;
    public bool doorStatus;

	// Use this for initialization
	void Start () {
        keyholes = new List<Keyhole>();
        foreach (Transform child in this.transform)
        {
            if(child.GetComponent<Keyhole>() != null)
            {
                keyholes.Add(child.GetComponent<Keyhole>());
            }
        }
        if(keyholes.Count == 0)
        {
            Debug.LogError("There are no keyholes parented to this door!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ValidateDoor()
    {
        foreach (Keyhole keyhole in keyholes)
        {
            if (!keyhole.correctInsertedEgg)
            {
                if(!doorStatus)
                {
                    Debug.Log("One or more key not filled!");
                    return;
                }
                else
                {
                    doorStatus = false;
                    Close();
                    return;
                }
            }
        }
        doorStatus = true;
        Open();
        return;
    }

    public void Open()
    {
        Debug.Log("Opening Door!");
        foreach (SlidingObject door in doors)
        {
            door.OpenDoor();
        }
    }

    public void Close()
    {
        Debug.Log("Closing Door!");
        foreach (SlidingObject door in doors)
        {
            door.CloseDoor();
        }
    }


}
