using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameObject brickObj1;

    //private GameObject brickObj;
    //private bool isActive;
    public GameObject GetbrickObj()
    {
        return brickObj1;
    }

    //private GameObject brickObj;
    //private bool isActive;
    public void SetbrickObj(GameObject value)
    {
        brickObj1 = value;
    }

    private bool isActive1;

    public bool GetisActive()
    {
        return isActive1;
    }

    public void SetisActive(bool value)
    {
        isActive1 = value;
    }

    public Brick(GameObject brickObj, bool isActive)
    {
        this.SetbrickObj(brickObj);
        this.SetisActive(isActive);
    }
}
