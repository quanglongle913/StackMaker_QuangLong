using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [System.Obsolete]
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dashpickup")
        {
            //Debug.Log("va cham");
            other.gameObject.tag = "normal";
            PlayerController.instance.AddBrick(other.gameObject);
            
        }
        if (other.tag == "UnBrickBlock")
        {
           
            //Debug.Log("va cham UnBrickBlock");
            other.gameObject.tag = "normal";
            PlayerController.instance.RemoveBrick(other.gameObject);    
        }
        if (other.tag == "CheckPoint")
        {
            //Debug.Log("va cham UnBrickBlock");
            //other.gameObject.tag = "normal";
            PlayerController.instance.ClearBrick();
        }
        if (other.tag == "WinBrickBlock")
        {
            other.gameObject.SetActive(false);
        }
    }
}
