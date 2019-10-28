using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapAndGrabbable : OVRGrabbable
{
    public LayerMask snapLayerMask;
    Collider col;
    Bounds boxBounds;
    
    void Start(){
        col = GetComponent<Collider>();
    }    
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity){
        base.GrabEnd(linearVelocity,angularVelocity);//do the parent stuff.
        //add our own functionality now.
        RaycastHit hit;
        boxBounds = col.bounds;

       Physics.BoxCast(boxBounds.center, boxBounds.extents,transform.forward,out hit,transform.rotation,0.1f,snapLayerMask,QueryTriggerInteraction.Collide);
        if(hit.collider != null){
           Debug.Log("overlap thing");
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //snap
            transform.position = hit.transform.position;
            transform.rotation = hit.transform.rotation;
        }
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
            //Draw a cube that extends to where the hit exists
        Gizmos.DrawWireCube(boxBounds.center, boxBounds.extents);
        
        
    }
}
