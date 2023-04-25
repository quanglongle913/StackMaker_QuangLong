using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject DashParent;
    [SerializeField] private GameObject PrevDash;

    [SerializeField] private float speed;
    [SerializeField] private float sizeBrick=0.3f;


    private bool isMoving = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PrevDash == null)
        {
            return;
        }
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.LeftArrow)||MobileInput.Instance.swipeLeft)
        {
            if (Physics.Raycast(PrevDash.transform.position, Vector3.left, out hit, LayerMask.GetMask("brickLayer")) && hit.collider.tag == "Dashpickup")
            {
                Debug.Log("position Hit: " + hit.collider.tag);
                isMoving = true;
               // rb.velocity = Vector3.left * speed;
                rb.transform.position = hit.transform.position;
                //transform.position = Vector3.MoveTowards(transform.position, hit.collider.transform.position, 0.01f);
            }
            else {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
        {
            
            
            
            if (Physics.Raycast(PrevDash.transform.position, Vector3.right, out hit, LayerMask.GetMask("brickLayer")) && hit.collider.tag == "Dashpickup")
            {
                Debug.Log("position Hit: " + hit.collider.tag);
                isMoving = true;
                rb.transform.position = hit.transform.position;
                //rb.velocity = Vector3.MoveTowards(transform.position, hit.collider.transform.position, 0.01f);
            }
            else
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }
         else if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.Instance.swipeUp)
        {
         
            
          
            if (Physics.Raycast(PrevDash.transform.position, Vector3.forward, out hit, LayerMask.GetMask("brickLayer")) && hit.collider.tag == "Dashpickup")
            {
                Debug.Log("position Hit: " + hit.collider.tag);
                transform.position = hit.collider.transform.position;
                isMoving = true;
                //rb.transform.position = hit.transform.position;
                rb.velocity =  Vector3.MoveTowards(transform.position, hit.collider.transform.position, 0.01f);
            }
            else
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.Instance.swipeDown)
        {
            isMoving = true;
          
            if (Physics.Raycast(PrevDash.transform.position, -Vector3.forward, out hit, LayerMask.GetMask("brickLayer")) && hit.collider.tag == "Dashpickup")
            {
                Debug.Log("position Hit: " + hit.collider.tag);
                isMoving = true;
                rb.transform.position = hit.transform.position;
                //rb.velocity = Vector3.MoveTowards(transform.position, hit.collider.transform.position, 0.01f);
            }
            else
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }
      
        //Debug.Log("gameobj name:  " + gameObject.name);
    }
    public void PickDash(GameObject dashObj)
    {
        Vector3 pos = dashObj.transform.position;
        //Di chuyen vien gach ve vi tri player va xep o duoi cung
        dashObj.transform.SetParent(DashParent.transform);
        // 0.3f la do cao vien gach 
        dashObj.transform.localPosition = new Vector3(PrevDash.transform.localPosition.x, PrevDash.transform.localPosition.y - sizeBrick, PrevDash.transform.localPosition.z);
        //tang chieu cao player them 0.3f 
        transform.localPosition = new Vector3(transform.position.x, transform.position.y + sizeBrick, transform.position.z);

        //transform.localPosition = new Vector3(pos.x, transform.position.y + sizeBrick, pos.z);
        //dat Doi tuong moi
        PrevDash = dashObj;
        PrevDash.GetComponent<BoxCollider>().isTrigger = false;
        if (!isMoving) {
            transform.localPosition = new Vector3(pos.x, transform.position.y + sizeBrick, pos.z);
        }
    }
}
