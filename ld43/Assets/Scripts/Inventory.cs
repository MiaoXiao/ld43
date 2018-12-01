using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryContents;

    public GameObject selectedArrow;

    [SerializeField]
    private ObjectPooler itemSlotPooler;

    private List<InventoryItem> allItems = new List<InventoryItem>();
    private int currentIndex = 0;
    public InventoryItem currentlySelected { get { return allItems[currentIndex]; } }

    public void AddItem(Egg egg)
    {
        GameObject item = itemSlotPooler.RetrieveCopy();
        GameObject eggCopy = Instantiate(egg.gameObject);
        eggCopy.transform.SetParent(item.transform, false);

        allItems.Add(item.GetComponent<InventoryItem>());
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
            /*
            if (eggToRemove.eggID == allItems[i].eggVisual)
            {
                itemSlotPooler.DeactivateObject()
                return;
            }
            */
        }
    }

    public void ToggleInventoryStatus()
    {
        inventoryContents.SetActive(!inventoryContents.activeInHierarchy);
        if (inventoryContents.activeInHierarchy)
        {
            StartCoroutine(WaitOneFrame());
        }
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
                currentIndex = i;
            }
        }
    }

}
