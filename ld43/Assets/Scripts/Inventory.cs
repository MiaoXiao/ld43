using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private float holdDistance = 5f;

    [SerializeField]
    private float holdVerticalOffset = 0.25f;

    [SerializeField]
    private GameObject inventoryContents;

    public GameObject selectedArrow;

    [SerializeField]
    private ObjectPooler itemSlotPooler;

    private List<InventoryItem> allItems = new List<InventoryItem>();
    private int currentIndex = 0;
    public InventoryItem currentlySelected
    {
        get
        {
            if (currentIndex >= allItems.Count)
                return null;
            return allItems[currentIndex];
        }
    }

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
        egg.gameObject.GetComponent<Collider>().enabled = false;

        GameObject item = itemSlotPooler.RetrieveCopy();
        InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
        inventoryItem.inFrontOfPlayerObj = egg.gameObject;
        GameObject eggCopy = Instantiate(egg.gameObject);
        Destroy(eggCopy.GetComponentInChildren<ParticleSystem>());
        eggCopy.gameObject.SetActive(true);
        eggCopy.transform.SetParent(item.transform, false);
        RecursivelySetLayer(eggCopy.transform);
        
        allItems.Add(inventoryItem);

        SelectNew(inventoryItem);
    }

    private void RecursivelySetLayer(Transform child)
    {
        child.gameObject.layer = LayerMask.NameToLayer("Item");
        for (int i = 0; i < child.childCount; ++i)
        {
            child.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Item");
            if (child.GetChild(i).childCount != 0)
                RecursivelySetLayer(child.GetChild(i));
        }
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
                eggToRemove.GetComponent<Collider>().enabled = true;
                Destroy(allItems[i].eggVisual.gameObject);
                itemSlotPooler.DeactivateObject(allItems[i].gameObject);
                eggToRemove.gameObject.SetActive(true);
                allItems.RemoveAt(i);
                if (currentIndex == allItems.Count)
                    GoNext();
                if (allItems.Count != 0)
                    MoveArrow(currentlySelected.transform);
                return;
            }
        }
    }

    public bool ToggleInventoryStatus(FirstPersonController controller)
    {
        bool status = !inventoryContents.activeInHierarchy;
        inventoryContents.SetActive(status);
        if (status && currentlySelected)
        {
            StartCoroutine(WaitOneFrame());
        }
        controller.enabled = !status;
        Cursor.visible = status;
        if (status)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        return status;
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
        Vector3 cameraForward = Camera.main.transform.forward + (Vector3.down *holdVerticalOffset);
        Vector3 destination = Camera.main.transform.position + (cameraForward * holdDistance);
        mesh.transform.position = destination;
        mesh.transform.LookAt(Camera.main.transform, Vector3.up);
        mesh.transform.Rotate(new Vector3(0, 180, 0));
    }
}
