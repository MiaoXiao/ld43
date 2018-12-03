using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    private float itemDistance = 0.5f;

    private float newScale = 250f;

    public bool HasEgg { get { return eggVisual != null; } }

    private SoundManager soundManager;

    public Egg eggVisual
    {
        get
        {
            if(transform.childCount == 0)
            {
                return null;
            }
            return transform.GetChild(0).GetComponent<Egg>();
        }
    }


    private Inventory inventory { get { return GetComponentInParent<Inventory>(); } }
    public GameObject inFrontOfPlayerObj;

    private Camera itemsCamera;
    private Camera inventoryUICamera;

    private void Awake()
    {
        itemsCamera = GameObject.FindGameObjectWithTag("ItemsCamera").GetComponent<Camera>();
        inventoryUICamera = GameObject.FindGameObjectWithTag("InventoryUICamera").GetComponent<Camera>();
        soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (HasEgg)
        {
            eggVisual.transform.localScale = new Vector3(newScale, newScale, newScale);
            Vector3 inventoryPos = inventoryUICamera.WorldToScreenPoint(transform.position);
            inventoryPos.z = itemDistance;
            eggVisual.transform.position = itemsCamera.ScreenToWorldPoint(inventoryPos);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        soundManager.PlaySFX("menu_select");
        inventory.SelectNew(this);
        inventory.MoveArrow(transform);
    }
}
