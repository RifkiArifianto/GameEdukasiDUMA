using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenecontroller : MonoBehaviour
{
    public void open_scene(string scene_name){
        Application.LoadLevel(scene_name);
    }

    public void exit(){
        Application.Quit();
    }
}
