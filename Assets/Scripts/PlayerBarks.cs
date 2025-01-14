﻿/* Code Acknowledgements
* From: diving_squid on YouTube 
* URL: https://www.youtube.com/watch?v=1nFNOyCalzo
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarks : MonoBehaviour
{

    private bool playerIsNearby;

    public string[] barkList;

    // Update is called once per frame
    private void Update()
    {
        // if player within range and clicks

        if (playerIsNearby && InputManager.Instance.ClickInput && !DialogueManager.Instance.DialogueIsActive() && this.CompareTag("Enemy"))
        {
            DialogueManager.Instance.playNonBlockingDialogue("Mr. NPC", barkList, 0.01f);
            // DialogueManager.Instance.playNonBlockingDialogue();
        }
        else if (playerIsNearby && InputManager.Instance.ClickInput && !DialogueManager.Instance.DialogueIsActive())
        {
            // start dialogue
            DialogueManager.Instance.playBlockingDialogue("Mr. NPC", barkList);
            playerIsNearby = false; // need to come back;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;
        }
    }

    public void ApartmentDialogue()
    {
        DialogueManager.Instance.playBlockingDialogue("Mr. NPC", barkList);
        playerIsNearby = false; // need to come back;
    }
}