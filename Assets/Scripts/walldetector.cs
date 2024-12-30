using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D obj){
        if(obj.name == "Ground"){
            transform.parent.GetComponent<player>().iswall = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        if(obj.name == "Ground"){
            transform.parent.GetComponent<player>().iswall = false;
        }
    }
}
