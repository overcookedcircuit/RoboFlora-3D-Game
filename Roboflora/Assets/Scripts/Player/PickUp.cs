using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item item = new Item("Item Name", 1);
    // Start is called before the first frame update
    public String itemName = "Default Item";
    public int itemCount = 1;

    public GameObject PickUpText;
    void Start()
    {   
        item.name = itemName;
        item.count = itemCount;
        PickUpText.SetActive(false);
    }

    void OnTriggerStay(Collider other){
        if(other.CompareTag("Player")){
            PickUpText.SetActive(true);
            if(Input.GetKey(KeyCode.F)){
                // Debug.Log("PLAYER ENCOUNTERED");
                Inventory.instance.AddItem(item);
                PickUpText.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other){
        PickUpText.SetActive(false);
    }
}
