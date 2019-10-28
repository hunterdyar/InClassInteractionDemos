using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsHandle : MonoBehaviour
{
    public Transform targetObject;
    public Transform handlePositionObject;
    HingeJoint hj;
    // Start is called before the first frame update
    void Start()
    {
        hj = GetComponent<HingeJoint>();
        hj.useMotor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(handlePositionObject.position.y-targetObject.position.y)>0.001f){
            JointMotor motor = hj.motor;
            motor.force = 100;
            motor.targetVelocity = (handlePositionObject.position.y-targetObject.position.y)*900;
            motor.freeSpin = false;
            hj.motor = motor;
        }
    }
}
