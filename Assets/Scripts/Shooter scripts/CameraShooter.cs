using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooter : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float smoothSpeed = 5f; 
    [SerializeField] private float yOffset = 0f; // Фіксована позиція по Y

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x, yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
