using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PrintInventory();
    }

    public void AddItem(Item newItem){
        bool itemExists = false;

        //Check if item exist in inventory if yes increase count by the amount the new item has
        foreach(Item item in items){
            if(item.name == newItem.name){
                item.count += newItem.count;
                itemExists = true;
                break;
            }
        }

        //If item doesn't exist add to inventory
        if(!itemExists){
            items.Add(newItem);
        }
        //Debugs
        Debug.Log("Added new item: " + newItem.name + ". Amount: " + newItem.count);
    }

    public void RemoveItem(Item itemRemove){
        foreach(Item item in items){
            if(item.name == itemRemove.name){
                item.count -= itemRemove.count;
                if(item.count <= 0){
                    items.Remove(itemRemove);
                }
                break;
            }
        }

        Debug.Log("Remove item: " + itemRemove.name + ". Amount: " + itemRemove.count);
    }

    public void PrintInventory(){
        if(Input.GetKey(KeyCode.H)){
            Debug.Log(items.Count);            
        }
    }
}
