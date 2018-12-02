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
    private int layerMask = ~(1 << 10); //Avoid player layer
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
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, range, layerMask))
            {
                GameObject hitObject = hit.transform.gameObject;
                Debug.Log(hitObject.name);
                if (hitObject.tag == "Egg")
                {
                    if (hitObject.GetComponent<Egg>().sourceKeyhole)
                        hitObject.GetComponent<Egg>().removeFromKeyHole();
                    playerInventory.AddItem(hitObject.GetComponent<Egg>());
                    //Debug.DrawRay(rayOrigin, playerCamera.transform.forward * hit.distance, Color.white);
                    Debug.Log("Hit!");
                }
                else if (hitObject.tag == "Keyhole" && playerInventory.currentlySelected != null && !hitObject.GetComponent<Keyhole>().eggInserted)
                {
                    Debug.Log(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>());
                    playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>().placeInKeyHole(hitObject.GetComponent<Keyhole>());
                    playerInventory.RemoveItem(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>());
                }
                else
                {
                    Debug.DrawRay(rayOrigin, playerCamera.transform.forward * hit.distance, Color.blue);
                    Debug.Log("Hit something that's not relevant..");
                }
            }
            else
            {
                //Debug.DrawRay(rayOrigin, playerCamera.transform.forward * 1000, Color.red);
                Debug.Log("Missed..");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ToggleQuitMenu();
        }
	}
}
