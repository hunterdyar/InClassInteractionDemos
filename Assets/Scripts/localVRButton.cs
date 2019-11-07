using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class localVRButton : MonoBehaviour
{

    //Basically the same as the other button but should work as a child object.

    public UnityEvent DoOnPush; 
    
    public float travelDownOnPush = 0.1f;
    public float buttonPushSpeed = 10f;
    public Vector3 localTravelDir;
    Vector3 upPos;
    Vector3 downPos;
    Vector3 goalPos;
    public bool hasReset = true;
    
    void Start(){
        localTravelDir.Normalize();
        upPos = transform.localPosition;
        downPos = transform.localPosition + localTravelDir*travelDownOnPush;
        goalPos = upPos;

        //Tests
        Collider col = GetComponent<Collider>();
        if(!col.isTrigger){
            Debug.LogWarning("Is the collider a trigger?");
        }
        if(GetComponent<Rigidbody>().isKinematic == false){
            Debug.LogWarning("button rigidbody should be kinematic so we control its movement");
        }
        if(travelDownOnPush == 0){
            Debug.LogWarning("need to set a travelDownOnPush amount");
        }
        if(localTravelDir == Vector3.zero){
            Debug.LogWarning("Button needs a direction to go when it gets pushed");
        }
    }    

    void Update(){
        transform.localPosition = Vector3.Lerp(transform.localPosition,goalPos,buttonPushSpeed*Time.deltaTime);
        Vector3 reset = transform.localPosition-upPos;
        if(reset.magnitude < 0.01f){
            hasReset = true;
        }
    }
    void OnTriggerEnter(){
        Debug.Log("buttons col");
        goalPos = downPos;

        if(hasReset){
            Debug.Log("button pressed");
            DoOnPush.Invoke();
            hasReset = false;
        }
    }
    void OnTriggerExit(){
        goalPos = upPos;
    }
}
