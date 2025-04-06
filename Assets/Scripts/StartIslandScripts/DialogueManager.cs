using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class DialogueManager : MonoBehaviour
{
    public NPCConversation myConversation;

    private bool isPlayerInTrigger = false;

    private void Update()
    {
        
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E)) //
        {
            //if (!ConversationManager.Instance.IsConversationActive)
            //{
                ConversationManager.Instance.StartConversation(myConversation);
                 Debug.Log("Starting conversation...");
           // }
            //else
            //{
               // Debug.Log("Conversation already active.");
           // }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger zone.");
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited trigger zone.");
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
    
    
}
