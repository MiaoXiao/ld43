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

    public Transform raycastPoint;
    public Camera playerCamera;
    private Vector3 rayOrigin;
    public int range;

    private void Awake()
    {
        playerInventory.AddItem(testEgg);
        playerInventory.AddItem(testEgg);
        playerInventory.AddItem(testEgg);
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
            rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, range))
            {
                if(hit.transform.gameObject.tag == "Egg")
                {
                    Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
                    Debug.Log("Hit!");
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
                Debug.Log("Missed..");
            }
        }
	}
}
