using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBeam : MonoBehaviour
{
    //Updates a line renderer start and end points to be self, and a raycast point.
    public GameObject pointingAt;
    private GameObject prevPointingAt;//used for the timer thing, not needed
    public Vector3 pointingPoint;//The collision point we are pointing at.


    // [HideInInspector]//public things but not visible in Unity Editor. This keeps my inspector tidy. This [blah] is an "attribute", which will apply to what follows it.
    public float timeNotNull = 0;//just for scripting challenge
    LineRenderer lr;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPositions(new Vector3[2]{Vector3.zero, Vector3.zero});
    }

    // Update is called once per frame
    void Update()
    {
        //The following 3 lines of code are 99% the whole deal.

        //A RaycastHit object is just a data holder for the point where the raycast... hits.
        //we make a ray on a separate line, just to keep the code readable. Theres a version of the Raycast script where you give it a transform and a direction. Thats fine too, but I think this is tidier.
        //Physics.Raycast returns true or false, if we hit something. Thats useful, but then if thats what it returns, how do we get the data? Shouldn't it return raycasthit?
        //Well, lets introduce the "out" keyword. We have it both ways. A nice boolean return type we can plug into if statements, and all our data accessable outside of the scope of the function.
        //how? "out" passes the a variable reference directly into the function so it can be edited, instead of like usual where it works with its own copy.
        //I use out in raycasts and like 2 other things and thats it. I can count the number of times i've needed to write a function with out on one fifth of a hand.
        RaycastHit hit;
        Ray ray = new Ray(transform.position,transform.forward);
        if(Physics.Raycast(ray,out hit,100))
        {
            // lr.enabled = true;
            prevPointingAt = pointingAt;
            pointingAt = hit.collider.gameObject;
            pointingPoint = hit.point;
            UpdateLine(hit.point);
        }else{
            //lr.enabled = false;
            UpdateLine(ray.GetPoint(100));//Gets a point 100 units along the ray (aka far away).
            pointingAt = null;
            pointingPoint = Vector3.zero;
        }

        ////This is part not needed, just for a little scripting challenge. HandBeamInfo uses this stuff.
        
        if(pointingAt != prevPointingAt){
            timeNotNull = 0;//Reset the timer to be more recent when going from one object to another but not null first.
        }
        
        if(pointingAt != null){
            timeNotNull = timeNotNull+Time.deltaTime;
        }else{
            timeNotNull = 0;
        }
        //above not needed

    }

    //Q: why does our line look pink
    //A: we never put a material on it.
    void UpdateLine(Vector3 endPoint){
        
        lr.SetPosition(0,transform.position);
        lr.SetPosition(1,endPoint);

    }
}
