using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    private float itemDistance = 50f;

    public bool HasEgg { get { return eggVisual != null; } }

    public Egg eggVisual { get { return transform.GetChild(0).GetComponent<Egg>(); } }

    private Inventory inventory { get { return GetComponentInParent<Inventory>(); } }

    private void Update()
    {
        if (HasEgg)
        {
            Vector3 screenPos = transform.position;
            screenPos.z = itemDistance;
            eggVisual.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inventory.SelectNew(this);
        inventory.MoveArrow(transform);
    }
}
