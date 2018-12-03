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
            //At least one keyhole does not have an egg inserted
            if (!keyhole.eggInserted)
            {
                if(!doorStatus)
                {
                    return;
                }
                else //Handles case where door is already opened, but player takes out a key
                {
                    doorStatus = false;
                    Close();
                    return;
                }
            }
        }
        //All eggs have been inserted and are correct
        doorStatus = true;
        Open();
        return;
    }

    public void Open()
    {
        GameObject.Find("GameManager").GetComponent<SoundManager>().PlaySFX("door_open", 1.5f);
        foreach (SlidingObject door in doors)
        {
            door.OpenDoor();
        }
    }

    public void Close()
    {
        foreach (SlidingObject door in doors)
        {
            door.CloseDoor();
        }
    }


}
