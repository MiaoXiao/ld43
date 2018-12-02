using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private float holdDistance = 5f;

    [SerializeField]
    private GameObject inventoryContents;

    public GameObject selectedArrow;

    [SerializeField]
    private ObjectPooler itemSlotPooler;

    private List<InventoryItem> allItems = new List<InventoryItem>();
    private int currentIndex = 0;
    public InventoryItem currentlySelected { get { return allItems[currentIndex]; } }

    private GameObject playerGameobj;

    private void Awake()
    {
        playerGameobj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (currentlySelected != null)
        {
            MoveObjectInFrontOfPlayer(currentlySelected.inFrontOfPlayerObj);
        }
    }

    public void AddItem(Egg egg)
    {
        GameObject item = itemSlotPooler.RetrieveCopy();
        InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
        inventoryItem.inFrontOfPlayerObj = egg.gameObject;
        GameObject eggCopy = Instantiate(egg.gameObject);
        eggCopy.gameObject.SetActive(true);
        eggCopy.transform.SetParent(item.transform, false);
        eggCopy.layer = LayerMask.NameToLayer("Item");
        allItems.Add(inventoryItem);
    }

    public void MoveArrow(Transform selected)
    {
        Vector3 arrowPos = selectedArrow.transform.position;
        arrowPos.x = selected.position.x;
        selectedArrow.transform.position = arrowPos;
    }

    public void RemoveItem(Egg eggToRemove)
    {
        for (int i = 0; i < allItems.Count; ++i)
        {
            if (eggToRemove.eggId == allItems[i].eggVisual.eggId)
            {
                itemSlotPooler.DeactivateObject(allItems[i].eggVisual.gameObject);
                allItems.RemoveAt(i);
                if (i == allItems.Count)
                    GoNext();
                MoveArrow(currentlySelected.transform);
                return;
            }
        }
    }

    public void ToggleInventoryStatus(FirstPersonController controller)
    {
        bool status = !inventoryContents.activeInHierarchy;
        inventoryContents.SetActive(status);
        if (status)
        {
            StartCoroutine(WaitOneFrame());
        }
        controller.enabled = !status;
        Cursor.visible = status;
        if (status)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator WaitOneFrame()
    {
        yield return new WaitForEndOfFrame();
        MoveArrow(currentlySelected.transform);
    }

    public void GoNext()
    {
        if (currentIndex >= allItems.Count - 1)
            currentIndex = 0;
        else
            currentIndex++;
    }

    public void GoPrevious()
    {
        if (currentIndex <= 0)
            currentIndex = allItems.Count - 1;
        else
            currentIndex--;
    }

    public void SelectNew(InventoryItem item)
    {
        for(int i = 0; i < allItems.Count; ++i)
        {
            if (item == allItems[i])
            {
                currentlySelected.inFrontOfPlayerObj.gameObject.SetActive(false);
                currentIndex = i;
            }
        }
    }

    private void MoveObjectInFrontOfPlayer(GameObject mesh)
    {
        mesh.gameObject.SetActive(true);
        Vector3 destination = playerGameobj.transform.position + (playerGameobj.transform.forward * holdDistance);
        mesh.transform.position = destination;
        mesh.transform.rotation = Quaternion.LookRotation(mesh.transform.position - playerGameobj.transform.position);
    }
}
