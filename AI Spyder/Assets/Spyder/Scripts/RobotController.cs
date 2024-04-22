using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] MotorController[] legOne;
    [SerializeField] MotorController[] legTwo;
    [SerializeField] MotorController[] legThree;
    [SerializeField] MotorController[] legFour;

    List<MotorController[]> legs = new List<MotorController[]>();
    private void Start()
    {
        legs.Add(legOne);
        legs.Add(legTwo);
        legs.Add(legThree);
        legs.Add(legFour);
    }
    // Update is called once per frame
    void Update()
    {
        //ControlAllJoints(legs, 0, KeyCode.Q, KeyCode.A);
        //ControlAllJoints(legs, 1, KeyCode.W, KeyCode.S);
        //ControlAllJoints(legs, 2, KeyCode.E, KeyCode.D);
    }

    void ControlJoint(MotorController[] leg, int joint, KeyCode codePositive, KeyCode codeNegative)
    {

        if (Input.GetKey(codePositive))
        {
            leg[joint].rotationState = RotationDirection.Positive;
        }
        else if (Input.GetKey(codeNegative))
        {
            leg[joint].rotationState = RotationDirection.Negative;
        }
        else
        {
            leg[joint].rotationState = RotationDirection.None;
        }
    }

    void ControlAllJoints(List<MotorController[]> legs, int joint, KeyCode codePositive, KeyCode codeNegative)
    {
        foreach (MotorController[] leg in legs)
        {
            ControlJoint(leg, joint, codePositive, codeNegative);
        }
    }

    public List<float> GetState()
    {
        List<float> state = new List<float>();
        if (legs.Count != 4)
        {
            for (int i = 0; i < 24; i++)
            {
                state.Add(0);
            }
        } else
        {
            foreach (var leg in legs)
            {
                foreach (var motor in leg)
                {
                    state.Add(motor.CurrentPrimaryAxisRotation());
                    state.Add((float) motor.articulation.jointVelocity[0]);
                }
            }
        }
        
        return state;
    }

    public void MakeAction(List<float> actions)
    {
        if (legs.Count != 4) return;
        for (int i = 0; i < 12; i++)
        {

            legs[i / 3][i % 3].rotationState = ActionToRotationState(actions[i]);
        }
    }

    RotationDirection ActionToRotationState(float action)
    {
        if (action > 0.5f)
        {
            return RotationDirection.Positive;
        }
        else if (action < -0.5f)
        {
            return RotationDirection.Negative;
        }
        else
        {
            return RotationDirection.None;
        }
    }
}
