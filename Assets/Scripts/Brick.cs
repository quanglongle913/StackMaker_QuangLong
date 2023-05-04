using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //private GameObject brickObj;
    //private bool isActive;
    public GameObject brickObj { get; set; }
    public bool isActive { get; set; }

    public Brick(GameObject brickObj, bool isActive)
    {
        this.brickObj = brickObj;
        this.isActive = isActive;
    }
}
