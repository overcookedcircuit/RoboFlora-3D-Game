using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text instructionText;
    
    private String[] instructionSets;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        instructionSets = new String[12];
        index = 0;
        instructionSets[0] = "Press Left Click to Shoot";
        instructionSets[1] = "Hold Down Right Click, \nwait till fully charge, \nthen left click to shoot";
        instructionSets[2] = "Press 2 To Switch to Bird Form";
        instructionSets[3] = "Press W A S D to Move,\n E to ascend, Q to descend";
        instructionSets[4] = "Every time you switch, \nthe charge battery will diminish";
        instructionSets[5] = "Every 10 second \nthe battery will replenish itself";
        instructionSets[6] = "Press 3 To Switch to\n Rhino Form";
        instructionSets[7] = "Press W A S D to Move, \nSHIFT to run";
        instructionSets[8] = "Hold Down Right Click, \nand release to activate a charge attack";
        instructionSets[9] = "Press 1 To Switch to\n Humanoid Form";
        instructionSets[10] = "Are You Ready?";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            if(index == 11){
                SceneManager.LoadScene("world1");
            }else{
                instructionText.text = instructionSets[index];
                index++;
            }
            
        }
    }
}