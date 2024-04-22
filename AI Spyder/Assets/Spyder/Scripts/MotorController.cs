using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public enum RotationDirection {None = 0, Positive = 1, Negative = -1}

public class MotorController : MonoBehaviour
{
    public RotationDirection rotationState = RotationDirection.None;
    public float speed = 300.0f;

    public ArticulationBody articulation;


    // LIFE CYCLE

    void Start()
    {
        articulation = GetComponent<ArticulationBody>();
    }

    void FixedUpdate()
    {
        if (rotationState != RotationDirection.None)
        {
            float rotationChange = (float)rotationState * speed * Time.fixedDeltaTime;
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        }
    }


    // MOVEMENT HELPERS

    public float CurrentPrimaryAxisRotation()
    {
        if (articulation.jointPosition.dofCount < 1) return 0f;
        float currentRotationRads = articulation.jointPosition[0];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads;
        return currentRotation;
    }

    void RotateTo(float primaryAxisRotation)
    {
        var drive = articulation.xDrive;
        drive.target = Mathf.Clamp(primaryAxisRotation, drive.lowerLimit, drive.upperLimit);
        articulation.xDrive = drive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body)
        {
            //print(gameObject.name + " " + collision.body.name);
        }
        
    }
}
