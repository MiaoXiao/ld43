using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory playerInventory;
    private Camera playerCamera;
    public Text interactionText;
    //Raycast Related
    private Vector3 rayOrigin;
    public int interactionRange;
    public int sightRange;
    private int layerMask = ~(1 << 10); //Avoid player layer

    SoundManager soundManager;

    private void Awake()
    {
        playerCamera = Camera.main;
        soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        RaycastHit hit;
        rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        //May need to add some delay if it gets laggy
        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, sightRange, layerMask))
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hitObject.tag == "Egg")
            {
                interactionText.text = "Press E to pick up egg";
            }
            else if (hitObject.tag == "Keyhole" && playerInventory.currentlySelected)
            {
                if (!hitObject.GetComponent<Keyhole>().eggInserted)
                {
                    if (hitObject.GetComponent<Keyhole>().validateSize(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>()))
                    {
                        if(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>().eggId == 0)
                        {
                            interactionText.text = "Press E to sacrifice me";
                        }
                        else if (playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>().eggId == 1)
                        {
                            interactionText.text = "Press E to place egg";
                        }
                        else if (playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>().eggId == 2)
                        {
                            interactionText.text = "Press E to deposit egg";
                        }
                    }
                    else
                    {
                        interactionText.text = "Egg is too big to fit";
                    }
                }
            }
        }
        else
        {
            interactionText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Tab)) //open inventory
        {
            playerInventory.ToggleInventoryStatus(GetComponent<FirstPersonController>());
        }
        else if (Input.GetKeyDown(KeyCode.E)) //interact
        {
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, interactionRange, layerMask))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.tag == "Egg")
                {
                    if (hitObject.GetComponent<Egg>().sourceKeyhole)
                        hitObject.GetComponent<Egg>().removeFromKeyHole();
                    playerInventory.AddItem(hitObject.GetComponent<Egg>());
                    //Debug.DrawRay(rayOrigin, playerCamera.transform.forward * hit.distance, Color.white);
                    //Debug.Log("Hit!");
                }
                else if (hitObject.tag == "Keyhole")
                {
                    if (playerInventory.currentlySelected != null && !hitObject.GetComponent<Keyhole>().eggInserted)
                    {
                        //If we can place the current egg in the keyhole, remove it from the inventory.
                        if (playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>().placeInKeyHole(hitObject.GetComponent<Keyhole>()))
                        {
                            playerInventory.RemoveItem(playerInventory.currentlySelected.inFrontOfPlayerObj.GetComponent<Egg>());
                        }
                        else
                        {
                            soundManager.PlaySFX("wrong_size_sound");
                        }
                    }
                }
                else
                {
                    Debug.DrawRay(rayOrigin, playerCamera.transform.forward * hit.distance, Color.blue);
                    //Debug.Log("Hit something that's not relevant..");
                }
            }
            else
            {
                //Debug.DrawRay(rayOrigin, playerCamera.transform.forward * 1000, Color.red);
                //Debug.Log("Missed..");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ToggleQuitMenu();
        }
	}
}
