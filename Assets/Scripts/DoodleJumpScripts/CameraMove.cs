using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform doodlePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doodlePos.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, doodlePos.position.y, transform.position.z);
        }
    }
}
