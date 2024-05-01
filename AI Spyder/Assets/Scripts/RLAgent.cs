using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class RLAgent : Agent
{
    [SerializeField] GameObject bodyPrefab;
    GameObject body;
    //GameObject target;
    RobotController robotController;
    bool newEpisode;

    void Start()
    {
        robotController = GetComponentInChildren<RobotController>();
        body = transform.Find("Body").gameObject;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        List<float> action = new List<float>();
        for (int i = 0; i < 12; i++)
        {
            action.Add(actions.ContinuousActions[i]);
        }
        if (robotController!=null)
        {
            robotController.MakeAction(action);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (newEpisode) return;
        sensor.AddObservation(robotController.GetState());
        sensor.AddObservation(body.transform.position);
        sensor.AddObservation(body.transform.rotation.eulerAngles);
        sensor.AddObservation(body.GetComponent<ArticulationBody>().velocity);
        sensor.AddObservation(body.GetComponent<ArticulationBody>().angularVelocity);

        var speed = body.GetComponent<ArticulationBody>().velocity.z;
        var reward = 0f;
        if (speed>0.1)
        {
            reward = speed;
        }
        else
        {
            reward = speed - 0.1f;
        }
        if (Mathf.Abs(body.transform.position.x) > 2)
        {
            reward -= 0.5f;
        }
        AddReward(reward);
    }

    public override void OnEpisodeBegin()
    {
        if (body)
        {
            DestroyImmediate(body);
            newEpisode = true;
            StartCoroutine(NewBodyCoroutine());
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        for (int i = 0; i < 12; i++)
        {
            continuousActionsOut[i] = Random.Range(-1f, 1f);
        }
       
    }



    public void Fail()
    {
        AddReward(-4f);
        EndEpisode();
    }

    private IEnumerator NewBodyCoroutine()
    {
        yield return (new WaitForEndOfFrame());
        print(body);
        body = Instantiate(bodyPrefab, transform);
        body.GetComponent<ArticulationBody>().velocity = Vector3.zero;
        body.GetComponent<ArticulationBody>().angularVelocity = Vector3.zero;
        robotController = body.GetComponent<RobotController>();
        newEpisode = false;
    }
}
