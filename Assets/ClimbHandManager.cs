using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbHandManager : MonoBehaviour
{
    public bool isGripped = false;
    public ClimbHandManager otherHand;
    Vector3 whenPressedPos;
    Vector3 playSpaceWhenPressedPos;
    Vector3 offsetFromPressedPos;
    Vector3 parentGoalPos;
    [Tooltip("You want OVRCameraRig here")]
    public Transform playSpaceParent;//OVRCameraRigh probably
    public OVRInput.Controller controller;//L Touch or R Touch for quest/rift/rift s
    public OVRInput.Button gripButton;//https://developer.oculus.com/documentation/unity/latest/concepts/unity-ovrinput/


  

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(gripButton,controller) || Input.GetButtonDown("Jump")){
            //Check for collision? Cast sphere around controller towards wall?
            //keep list of overlapping colliders using ontrigger enter and exit to add/remove them?
            StartGrip();
        }
        if(OVRInput.GetUp(gripButton, controller) || Input.GetButtonUp("Jump")){
            EndGrip();
        }

       if(isGripped){
           offsetFromPressedPos = whenPressedPos - transform.position;//using our (world) position for this, not localPosition?
           playSpaceParent.position = playSpaceWhenPressedPos + offsetFromPressedPos;   
       }

    }

    void StartGrip(){
        otherHand.EndGrip();
        // Debug.Log("start grip");
        isGripped = true;//begin moving parent
        whenPressedPos = transform.position;//set our initial grab position to calculate the offset from
        playSpaceWhenPressedPos = playSpaceParent.position;//set parents position to be set to this, plus the offset
    }
    void EndGrip(){
        // Debug.Log("let go");
        isGripped = false;
    }
}
