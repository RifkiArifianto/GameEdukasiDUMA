using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class navigation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   public player player;

   int nav = 0;

   public void OnPointerDown(PointerEventData pointerEventData)
   {
        if(gameObject.name == "nav left"){
            nav = 1;
        }

         if(gameObject.name == "nav right"){
            nav = 2;
        }

         if(gameObject.name == "nav jump"){
            player.jump();
        }
   }

   public void OnPointerUp(PointerEventData pointerEventData)
   {
        if(gameObject.name == "nav left" || gameObject.name == "nav right"){
            nav = 0;
            player.stop();
        }
   }

   void Update(){
        if (nav == 1){
            player.runleft();
        } else if (nav == 2){
            player.runright();
        }
   }
}
