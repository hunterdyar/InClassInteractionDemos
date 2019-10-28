using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRandomColor : MonoBehaviour
{
    public Color[] colors;

    public void DoTurnRandomColor(){
        GetComponent<MeshRenderer>().material.color = colors[Random.Range(0,colors.Length)];
    }
}
