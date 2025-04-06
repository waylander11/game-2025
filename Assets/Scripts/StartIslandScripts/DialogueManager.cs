using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
                
public class DialogueManager : MonoBehaviour
{
    [Header("Conversations")]
    [SerializeField] private NPCConversation firstConversation; // Первый диалог
    [SerializeField] private NPCConversation secondConversation; // Второй диалог
                
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f; // Скорость движения NPC
    [SerializeField] private Transform targetPoint; // Точка, куда движется NPC
                
    [Header("Animation Settings")]
     private Animator npcAnimator; // Аниматор NPC
    [SerializeField] private string walkUpAnimation = "Up"; // Анимация движения вверх
    [SerializeField] private string idleDownAnimation = "Down"; // Анимация ожидания вниз
                
    private bool isPlayerInTrigger = false; // Флаг, находится ли игрок в триггере
    private bool isMoving = false; // Флаг, движется ли NPC
    private bool firstConversationCompleted = false; // Флаг, завершён ли первый диалог
                
    private void Start()
    {
        
        ConversationManager.OnConversationEnded += OnConversationEnded;
                
        LoadNPCState();
        if (npcAnimator == null)
        {
            npcAnimator = GetComponent<Animator>();
        }
    }
                
    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
                
    private void Update()
    {
    
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!firstConversationCompleted)
            {
                ConversationManager.Instance.StartConversation(firstConversation);
            }
            else
            {
                ConversationManager.Instance.StartConversation(secondConversation);
            }
        }
                
                       
        if (isMoving)
        {
            MoveToTarget();
                
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isMoving = false;
                npcAnimator.Play(idleDownAnimation);

                SaveNPCState();
            }
        }
    }
                
    private void OnConversationEnded()
    {
        Debug.Log("Conversation ended!");
        if (!firstConversationCompleted)
        {
            firstConversationCompleted = true;
            isMoving = true;
            npcAnimator.enabled = true;
            npcAnimator.Play(walkUpAnimation);
        }
    }
                
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            isMoving = false;
            
        }
    }
                
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }
                
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
                
    private void SaveNPCState()
    {
        PlayerPrefs.SetFloat("NPC_PosX", transform.position.x);
        PlayerPrefs.SetFloat("NPC_PosY", transform.position.y);
        PlayerPrefs.SetFloat("NPC_PosZ", transform.position.z);
        PlayerPrefs.SetInt("FirstConversationCompleted", firstConversationCompleted ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("NPC position saved!");
    }
                

    private void LoadNPCState()
    {
        if (PlayerPrefs.HasKey("NPC_PosX") && PlayerPrefs.HasKey("NPC_PosY") && PlayerPrefs.HasKey("NPC_PosZ"))
        {
            float x = PlayerPrefs.GetFloat("NPC_PosX");
            float y = PlayerPrefs.GetFloat("NPC_PosY");
            float z = PlayerPrefs.GetFloat("NPC_PosZ");
            transform.position = new Vector3(x, y, z);
            Debug.Log("NPC position loaded!");
        }
        if (PlayerPrefs.HasKey("FirstConversationCompleted"))
    {
        firstConversationCompleted = PlayerPrefs.GetInt("FirstConversationCompleted") == 1;
        Debug.Log("NPC conversation state loaded: " + firstConversationCompleted);
    }
    }
                
            
        
}


