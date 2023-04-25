using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dashpickup")
        {
            //Debug.Log("va cham");
            other.gameObject.tag = "normal";
            PlayerController.instance.PickDash(other.gameObject);
           
            other.gameObject.AddComponent<Rigidbody>();
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
           //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
           
            //add stack script vao doi tuong moi
            other.gameObject.AddComponent<Stack>();

            //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this);// xoa bo stack o doi tuong cu~
        }
    }
}
