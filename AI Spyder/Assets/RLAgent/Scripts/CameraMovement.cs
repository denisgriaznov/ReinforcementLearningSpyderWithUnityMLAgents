using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float gap;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gap = transform.position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!(player==null))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + gap);
        }
    }
}
