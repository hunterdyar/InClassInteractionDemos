using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throttle : MonoBehaviour
{
    bool isOverlapping;
    public bool throttleActive;
    public Transform rightHand;
    public OVRInput.Button gripButton;
    public OVRInput.Controller controller;
    public float throttleResistance = 1;
    public float speed;
    void Update()
    {
        if(OVRInput.GetDown(gripButton,controller)){
            if(isOverlapping){
                throttleActive = true;
            }
        }else if(OVRInput.GetUp(gripButton,controller)){
            throttleActive = false;
        }

        if(throttleActive){//
                 //find the direction of the throttle and the hand 
            Vector3 directionToHand = rightHand.position - transform.position;
            Vector3 throttleDirection = transform.up;


            //make the directions planar, so we only care about them in a 2d sense, so the distance my hand is to the right/left of the throttle doesnt matter
            directionToHand = Vector3.ProjectOnPlane(directionToHand,transform.forward);
            throttleDirection = Vector3.ProjectOnPlane(throttleDirection,transform.forward);

            //get the signed angle (can be negative) and the angle (always positive) between the vectors
            float signedPullAngle = Vector3.SignedAngle(throttleDirection,directionToHand,transform.forward);
            float pullAngle = Vector3.Angle(directionToHand,throttleDirection);

            //rotate me
            if(signedPullAngle > 0){
                if(Vector3.SignedAngle(Vector3.up,transform.up,transform.forward)<80){//Gets the angle between world up and throttle up, checks if it less than 80
                    transform.RotateAround(transform.position,transform.forward, pullAngle*Time.deltaTime*throttleResistance);
                }
            }else{
                if(Vector3.SignedAngle(transform.up,Vector3.up,transform.forward)<80){//gets the angle between throttle and world up (opposite of above), checks if THAT is under 80.
                    transform.RotateAround(transform.position,transform.forward, -Time.deltaTime*pullAngle*throttleResistance);
                }
            }
            speed = (Vector3.SignedAngle(Vector3.up,transform.up,transform.forward)+90)/18;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Hand")){
            isOverlapping = true;
        }
    }
    void OnTriggerExit(Collider col){
        if(col.CompareTag("Hand")){
            isOverlapping = false;
        }
    }

    //Get a usable version of the speed for controlling the car

    //returns speed
    public float GetSpeed(){

        if(speed<1){
            return 0;
        }

        if(speed > 8){
            return 10;
        }
        
        return speed;

    }
}
