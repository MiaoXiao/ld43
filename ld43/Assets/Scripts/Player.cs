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
    [SerializeField]
    private Egg testEgg2;
    [SerializeField]
    private Egg testEgg3;

    private void Awake()
    {
        playerInventory.AddItem(testEgg);
        playerInventory.AddItem(testEgg2);
        playerInventory.AddItem(testEgg3);
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
