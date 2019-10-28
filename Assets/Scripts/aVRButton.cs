using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class aVRButton : MonoBehaviour
{
    public UnityEvent DoOnPush; 
    
    public float travelDownOnPush = 0.1f;
    public float buttonPushSpeed = 10f;
    Vector3 upPos;
    Vector3 downPos;
    Vector3 goalPos;
    public bool hasReset = true;
    
    void Start(){
        upPos = transform.position;
        downPos = transform.position + Vector3.down*travelDownOnPush;
        goalPos = upPos;
    }    

    void Update(){
        transform.position = Vector3.Lerp(transform.position,goalPos,buttonPushSpeed*Time.deltaTime);
        Vector3 reset = transform.position-upPos;
        if(reset.magnitude < 0.01f){
            hasReset = true;
        }
    }
    void OnTriggerEnter(){
        Debug.Log("buttons col");
        goalPos = downPos;

        if(hasReset){
            DoOnPush.Invoke();
            hasReset = false;
        }
    }
    void OnTriggerExit(){
        goalPos = upPos;
    }
}
