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
    private enum Direct { None, Forward, Back, Right, Left}
    private Direct direct;
    private Vector3 moveTarget;

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
        direct = Direct.None;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PrevDash == null)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)||MobileInput.Instance.swipeLeft)
        {
            isMoving = true;
            direct = Direct.Left;
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
        {

            isMoving = true;
            direct = Direct.Right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.Instance.swipeUp)
        {

            isMoving = true;
            direct = Direct.Forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.Instance.swipeDown)
        {

            isMoving = true;
            direct = Direct.Back;

        }
        if (isMoving && direct==Direct.Left)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.deltaTime);
            }
            else 
            {
                isMoving = false;
            }
        }
        if (isMoving && direct == Direct.Right)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
        if (isMoving && direct == Direct.Forward)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
        if (isMoving && direct == Direct.Back)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(transform.position, Vector3.back, out hit, 10f, brickLayer) )
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
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
       
    }
}