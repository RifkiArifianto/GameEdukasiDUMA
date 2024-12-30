using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounddetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D obj){
        if(obj.name == "Ground"){
            transform.parent.GetComponent<player>().isground = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        if(obj.name == "Ground"){
            transform.parent.GetComponent<player>().isground = false;
        }
    }
}