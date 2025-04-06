using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation dialogue; 
    private bool hasTriggered = false; 

    private void Start()
    {
        
        if (PlayerPrefs.GetInt("DialogueTriggered_" + gameObject.name, 0) == 1)
        {
            hasTriggered = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player")) 
        {
            hasTriggered = true; 
            PlayerPrefs.SetInt("DialogueTriggered_" + gameObject.name, 1); 
            PlayerPrefs.Save(); 
            ConversationManager.Instance.StartConversation(dialogue); 
            Debug.Log("Dialogue triggered: " + dialogue.name);
        }
    }
}
