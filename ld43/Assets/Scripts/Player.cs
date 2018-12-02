using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInventory;

    [SerializeField]
    private Egg testEgg;

    private void Awake()
    {
        playerInventory.AddItem(testEgg);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //open inventory
        {
            playerInventory.ToggleInventoryStatus(GetComponent<FirstPersonController>());
        }
        else if (Input.GetKeyDown(KeyCode.E)) //interact
        {

        }
	}
}
