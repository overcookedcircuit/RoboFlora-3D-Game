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
        instructionSets = new String[13];
        index = 0;
        instructionSets[0] = "Press Left Click to Shoot";
        instructionSets[1] = "Hold Right Click to charge, then Left Click while holding Right Click to Shoot";
        instructionSets[2] = "Press 2 To Switch to Bird Form";
        instructionSets[3] = "Control the camera by holding right click";
        instructionSets[4] = "Press W A S D to Move, E to ascend, Q to descend";
        instructionSets[5] = "Every time you switch, the charge battery will diminish";
        instructionSets[6] = "Every 10 seconds the battery will replenish itself";
        instructionSets[7] = "Press 3 To Switch to Rhino Form";
        instructionSets[8] = "Press W A S D to Move, SHIFT to run";
        instructionSets[9] = "Control the camera by holding right click";
        instructionSets[10] = "Hold the key 'C', and release to activate a charge attack";
        instructionSets[11] = "Fly through the holes on the wall to practice fighting enemies";
        instructionSets[12] = "Are You Ready?";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            if (index == 13)
            {
                SceneManager.LoadScene("world1");
            }
            else
            {
                instructionText.text = instructionSets[index];
                index++;
            }

        }
    }
}
