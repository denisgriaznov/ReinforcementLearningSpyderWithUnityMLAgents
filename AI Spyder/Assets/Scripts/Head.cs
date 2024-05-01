using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    [SerializeField] RLAgent agent;
    float startPosition;
    private void Start()
    {
        agent = transform.parent.parent.GetComponent<RLAgent>();
        startPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        if ((startPosition - transform.position.y)>1.5f)
        {
            agent.Fail();
        }
    }


}
