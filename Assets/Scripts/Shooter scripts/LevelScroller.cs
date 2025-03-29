using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float speed = 0.1f; 
    [SerializeField] private Vector2 offset = new Vector2(3f, 0f); // Зміщення камери

    private Vector3 targetPosition;

    private void LateUpdate()
    {
        if (player == null) return;

      
        targetPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        
    
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }
}
