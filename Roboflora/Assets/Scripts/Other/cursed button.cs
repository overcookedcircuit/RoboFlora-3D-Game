using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursedbutton : MonoBehaviour
{
    private bool PlayerInZone;
    public GameObject player;
    public float x = 553.94f;
    public float y = 13.42f;
    public float z = 366.61f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F)) // If in zone, press F, and not already moving
        {
            gameObject.GetComponent<Animator>().Play("switch");
            player.transform.position = new Vector3(x, y, z);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = true;
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         PlayerInZone = false;
    //     }
    // }
}
