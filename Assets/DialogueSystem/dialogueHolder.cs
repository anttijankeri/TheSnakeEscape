﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueHolder : MonoBehaviour {

    public string dialogue;
    private dialogueManager dMAn;

    public string[] dialogueLines;

    /// <summary>
    /// Loads dialogueManager in the start of every scene
    /// </summary>
	// Use this for initialization
	void Start () {
        dMAn = FindObjectOfType<dialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// If player collides with the text box collider,
    /// then show dialoguebox from dialogueManager
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "SnakeHead")
        {
            if (Input.GetMouseButtonUp(0))
            {
                //dMAn.ShowBox(dialogue);

                //If dialogue is not active, reset the lines back to zero.
                if (!dMAn.dialogueActive)
                {
                    dMAn.dialogLines = dialogueLines;
                    dMAn.currentLine = 0;
                    dMAn.ShowDialogue();
                }
            }
        }
    }
}