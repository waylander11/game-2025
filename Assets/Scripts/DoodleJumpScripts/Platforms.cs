using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public float forceJump;
    private DoodleMovement playerScript;
   
    void Start () {

         GameObject playerObject = GameObject.Find("PlayerDoodleJump");
        if (playerObject != null)
        {
            playerScript = playerObject.GetComponent<DoodleMovement>();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.relativeVelocity.y < 0)
        {
            playerScript.rb.velocity = Vector2.up * forceJump;
        } 
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Cleaner")
        {
            float RandX = Random.Range(-3f, 3f);
            float RandY = Random.Range(transform.position.y + 20f, transform.position.y + 22f);
            transform.position = new Vector3(RandX, RandY, 0);
        }
    }
}
