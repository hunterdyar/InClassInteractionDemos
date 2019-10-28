using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//UNity can't serialize (show in the inspector) type-flexible things (with the <>), so we are creating our own class, boolEvent, that will override UnityEvent<bool>. It replaces it, and adds 0 things that is unique.
[System.Serializable]
public class boolEvent : UnityEvent<bool>{}
//We can just do it here in this script, for demo purposes, but on projects i use unityEvents for I will have one script that holds all of these event overrides.

public class aToggleSwitch : MonoBehaviour
{
    bool toggle = true;

    public GameObject onObject;
    public GameObject offObject;
    public boolEvent onToggleChange;

    void Start(){
        SetChildrenTo(toggle);
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        toggle = !toggle;
        onToggleChange.Invoke(toggle);
        SetChildrenTo(toggle);
    }
    void SetChildrenTo(bool onoff){
        onObject.SetActive(onoff);
        offObject.SetActive(!onoff);
    }
}
