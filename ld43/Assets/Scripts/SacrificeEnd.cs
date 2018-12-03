using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeEnd : MonoBehaviour
{
    [SerializeField]
    private Transform savedEgg;
    [SerializeField]
    private Transform sacrificedEgg1;
    [SerializeField]
    private Transform sacrificedEgg2;
    [SerializeField]
    private Transform sacrificedEgg3;
    [SerializeField]
    private GameObject endMenu;
    [SerializeField]
    private Image fadeScreen;

    [Space(10)]

    [SerializeField]
    private GameObject crackedEgg;

    [SerializeField]
    private GameObject purpleEgg;

    [SerializeField]
    private GameObject wingedEgg;

    private bool quitOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !quitOnce)
        {
            quitOnce = true;

            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            GameObject saved = null;
            GameObject sacrificed1 = null;
            GameObject sacrificed2 = null;
            GameObject sacrificed3 = null;
            int eggId = player.GetCurrentEggId();
            Debug.LogWarning(eggId);
            if (eggId == -1)
            {
                //No saved
                sacrificed1 = crackedEgg;
                sacrificed2 = purpleEgg;
                sacrificed3 = wingedEgg;
            }
            else if(eggId < 3)
            {
                //cracked egg
                saved = crackedEgg;
                sacrificed1 = purpleEgg;
                sacrificed2 = wingedEgg;
            }
            else if (eggId < 7)
            {
                //purple egg
                saved = purpleEgg;
                sacrificed1 = crackedEgg;
                sacrificed2 = wingedEgg;
            }
            else
            {
                //Winged egg
                saved = wingedEgg;
                sacrificed1 = crackedEgg;
                sacrificed2 = purpleEgg;
            }

            endMenu.gameObject.SetActive(true);
            if (saved)
                PositionGameobj(saved, savedEgg.gameObject);

            if (sacrificed1)
                PositionGameobj(sacrificed1, sacrificedEgg1.gameObject);

            if (sacrificed2)
                PositionGameobj(sacrificed2, sacrificedEgg2.gameObject);

            if (sacrificed3)
                PositionGameobj(sacrificed3, sacrificedEgg3.gameObject);

            Invoke("QuitGame", 10f);
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PositionGameobj(GameObject obj, GameObject position_at)
    {
        obj = Instantiate(obj);
        RecursivelySetLayer(obj.transform);

        position_at.gameObject.SetActive(true);
        Camera itemsCamera = GameObject.FindGameObjectWithTag("ItemsCamera").GetComponent<Camera>();
        Camera inventoryUICamera = GameObject.FindGameObjectWithTag("InventoryUICamera").GetComponent<Camera>();

        obj.transform.localScale = new Vector3(250, 250, 250);
        Vector3 inventoryPos = inventoryUICamera.WorldToScreenPoint(position_at.transform.position);
        inventoryPos.z = 30f;

        obj.transform.position = itemsCamera.ScreenToWorldPoint(inventoryPos);
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
}
