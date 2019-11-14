using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class myOVRGrabbable : OVRGrabbable
{
    public UnityEvent OnGrabStart;
    public UnityEvent OnGrabEnd;
    public UnityEvent OnGrabStay;
    

    // Update is called once per frame
    void Update()
    {
        if(base.isGrabbed){
            OnGrabStay.Invoke();
        }
    }

    //Do the parent class's function, and then add our own code (invoke events).
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint){
        base.GrabBegin(hand,grabPoint);
        OnGrabStart.Invoke();
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity){
        base.GrabEnd(linearVelocity,angularVelocity);
        OnGrabEnd.Invoke();
    }
}
