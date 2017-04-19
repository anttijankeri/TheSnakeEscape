using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dialogueManager : MonoBehaviour {
    //Dialogue box
    public GameObject dialogueBox;
    //Dialogue text
    public Text dialogueText;
    public bool dialogueActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Input.GetMouseButtonDown(0) tai Input.GetKeyDown(KeyCode.Space)
        if (dialogueActive && Input.GetMouseButtonDown(0))
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;
        }
	}

    /// <summary>
    /// Sets dBox and dActive to true
    /// and sets dText to dialogue
    /// </summary>
    /// <param name="dialogue"></param>
    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }
}
