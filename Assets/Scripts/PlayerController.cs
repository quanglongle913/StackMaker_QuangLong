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
        if (Input.GetKeyDown(KeyCode.LeftArrow)||MobileInput.Instance.swipeLeft && isMoving)
        {
            isMoving = true;
            rb.velocity = Vector3.left * speed * Time.deltaTime;
           
            //rb.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.Instance.swipeRight && isMoving)
        {
            isMoving = true;
            rb.velocity = Vector3.right * speed * Time.deltaTime;
            //rb.AddForce(Vector3.right * speed * Time.deltaTime);
        }
         else if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.Instance.swipeUp && isMoving)
        {
            isMoving = true;
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.Instance.swipeDown && isMoving)
        {
            isMoving = true;
            rb.velocity = -Vector3.forward * speed * Time.deltaTime;
        }
        if (rb.velocity==Vector3.zero)
        {
            isMoving = false;
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

        
        //transform.localPosition = new Vector3(dashObj.transform.position.x, transform.position.y + 0.3f, dashObj.transform.position.z);

        //transform.localPosition = new Vector3(pos.x, transform.position.y + 0.3f, pos.z);
       
        //dat Doi tuong moi
        PrevDash = dashObj;
        PrevDash.GetComponent<BoxCollider>().isTrigger = false;
       
    }
}
