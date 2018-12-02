using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInventory;

    private Camera playerCamera;
    private Vector3 rayOrigin;
    public int range;

    private void Awake()
    {
        playerCamera = Camera.main;
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
                if (hit.transform.gameObject.tag == "Egg")
                {
                    playerInventory.AddItem(hit.transform.gameObject.GetComponent<Egg>());
                    Debug.DrawRay(rayOrigin, playerCamera.transform.forward * hit.distance, Color.white);
                    Debug.Log("Hit!");
                }
                else if (hit.transform.gameObject.tag == "Keyhole" && playerInventory.currentlySelected != null)
                {
                    Debug.Log(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>());
                    playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>().placeInKeyHole(hit.transform.gameObject.GetComponent<Keyhole>());
                    playerInventory.RemoveItem(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>());
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, playerCamera.transform.forward * 1000, Color.red);
                Debug.Log("Missed..");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ToggleQuitMenu();
        }
	}
}
