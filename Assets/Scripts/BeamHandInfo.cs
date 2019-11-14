using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHandInfo : MonoBehaviour
{

    //This script is 100 NOT needed for laser pointer things.
    //Simply put, when you have multiple laser pointers, but you only want one place to check the data if the hands are doing the same thing.
    //This script will look at every hands and, depending on which one updated most recently, it will update the data to that.

    //Whats missing is probably just a Oculus thing to automatically disable the HandBeam script if the controller has been disconnected, and re-enable it. 
    //That way it doesnt default to pointing at the floor or whatever.
    public GameObject pointingAtObject;
    public Vector3 pointPoint;
    public HandBeam[] allHands;
    int lastUpdatedHand = 0; 

    void Start(){
        allHands = GameObject.FindObjectsOfType<HandBeam>();//Gets all the HandBeam Objects in the scene.
    }
    void Update(){
        pointingAtObject = null;
        pointPoint = Vector3.zero;
        
        //Will favor one hand over another one when both are pointing at different objects.
        //THis is probably fine if we are using buttons to input, because left button for left hand pointer, right for right. no worry.

        // foreach(HandBeam hand in allHands){
        //     if(hand.pointingPoint != Vector3.zero){
        //         pointPoint = hand.pointingPoint;
        //     }
        //     if(hand.pointingAt != null){
        //         pointingAtObject = hand.pointingAt;
        //     }
        // }

        //Scripting Challenge: Make the data represent the most recently pointed-at thing from any number of hands pointing at things.
        int mostRecentHandIndex = -1;//an impossible index value
        float mostRecentTime = 1000000;//cant do math with infinity but we can do comparisons. all numbers are less than infinity.

        //This gets the hand that has most recently started touching an object
        for(int i = 0;i<allHands.Length;i++){
            if(allHands[i].timeNotNull != 0){
                if(allHands[i].timeNotNull < mostRecentTime){
                    mostRecentTime = allHands[i].timeNotNull;
                    mostRecentHandIndex = i;
                }
            }    
        }
        if(mostRecentHandIndex != -1){
            pointingAtObject = allHands[mostRecentHandIndex].pointingAt;
            pointPoint = allHands[mostRecentHandIndex].pointingPoint;
        }

    }
}
