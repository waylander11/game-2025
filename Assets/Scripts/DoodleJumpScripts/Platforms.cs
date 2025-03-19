using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public float forceJump;
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.relativeVelocity.y < 0)
        {
            DoodleMovement.instance.rb.velocity = Vector2.up * forceJump;
        } 
    }
}
