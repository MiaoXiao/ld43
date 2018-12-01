using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInventory;

    [SerializeField]
    private Egg testEgg;

    private void Awake()
    {
        playerInventory.AddItem(testEgg);
        playerInventory.AddItem(testEgg);
        playerInventory.AddItem(testEgg);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerInventory.ToggleInventoryStatus();
        }
	}
}
