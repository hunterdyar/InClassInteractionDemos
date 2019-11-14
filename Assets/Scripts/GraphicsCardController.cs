using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsCardController : MonoBehaviour
{
    public int state = 0;
    public int coolCounter = 0;
    Animator animator;
    float timer = 0;
    void Start(){
        animator = GetComponent<Animator>();
    }
    void Update(){
        timer = timer + Time.deltaTime;
        if(timer > 4){
            state++;
            timer = 0;
        }
        animator.SetInteger("state",state);
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Fan")){
            timer = 0;
            coolCounter++;
            if(coolCounter > 6){
                if(state > 0){
                    state = state - 1;
                }//end state if
                coolCounter = 0;
            }//end cool counter
        }//end if fan tag
    }//end OnTriggerEnter
}

