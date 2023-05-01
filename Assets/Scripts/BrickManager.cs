using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickManager : MonoBehaviour
{

    public UnityAction<GameObject> AddBrick;
    public UnityAction<GameObject> RemoveBrick;
    public UnityAction ClearBrick;
    public UnityAction<GameObject> WinPos;
    [System.Obsolete]
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dashpickup")
        {
            AddBrick(other.gameObject);
            
        }
        if (other.tag == "UnBrickBlock")
        {
            // Debug.Log("UnBrickBlock");
            RemoveBrick(other.gameObject); 
        }
        if (other.tag == "CheckPoint")
        {
            ClearBrick();
        }
        if (other.tag == "box")
        {
            WinPos(other.gameObject);
        }
    }
}
