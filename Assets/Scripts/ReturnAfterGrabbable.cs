using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnAfterGrabbable : OVRGrabbable
{

    public Transform returnToThis;
    // Start is called before the first frame update
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity){
        base.GrabEnd(Vector3.zero,Vector3.zero);
        transform.position = returnToThis.position;
        transform.rotation = returnToThis.rotation;
    }
    void Update(){
        if(!base.isGrabbed){
            transform.position = returnToThis.position;
            transform.rotation = returnToThis.rotation;
        }
    }
}
