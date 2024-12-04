using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text instructionText;
    
    private String[] instructionSets;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        instructionSets = new String[10];
        index = 0;
        instructionSets[0] = "Press Left Click to Shoot";
        instructionSets[1] = "Hold Down Right Click, until charge, then left click to shoot";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            instructionText.text = instructionSets[index];
            index++;
        }
    }
}
