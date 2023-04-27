using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject People;
    [SerializeField] private GameObject DashParent;
    [SerializeField] private GameObject PrevDash;

    [SerializeField] private float speed;
    [SerializeField] private float sizeBrick=0.3f;

    [SerializeField] private SwipeDetector swipeDetector;

    private enum Direct {
        None,
        Up,
        Down,
        Right,
        Left}
    private Direct direct;
    private Vector3 moveTarget;

    private bool isWin = false;
    private bool isMoving = false;
    private int countBrick;


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
        if (swipeDetector!=null)
        {
            swipeDetector.onSwipeLeft += SwipeDetector_OnSwipLeft;
            swipeDetector.onSwipeRight += SwipeDetector_OnSwipRight;
            swipeDetector.onSwipeUp += SwipeDetector_OnSwipUp;
            swipeDetector.onSwipeDown += SwipeDetector_OnSwipDown;
        }
        countBrick = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PrevDash == null)
        {
            return;
        }
        if (isMoving && direct == Direct.Left)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(PrevDash.transform.position, Vector3.left, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.fixedDeltaTime);
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
            if (Physics.Raycast(PrevDash.transform.position, Vector3.right, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.fixedDeltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
        if (isMoving && direct == Direct.Up)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(PrevDash.transform.position, Vector3.forward, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.fixedDeltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
        if (isMoving && direct == Direct.Down)
        {
            RaycastHit hit;
            LayerMask brickLayer = LayerMask.GetMask("Line");
            if (Physics.Raycast(PrevDash.transform.position, Vector3.back, out hit, 1f, brickLayer))
            {
                moveTarget = hit.collider.transform.position;
                moveTarget.y = rb.transform.position.y;
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.fixedDeltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
    }
    private void SwipeDetector_OnSwipDown()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Down;
        }
    }

    private void SwipeDetector_OnSwipUp()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Up;
        }
    }

    private void SwipeDetector_OnSwipRight()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Right;
        }
    }

    private void SwipeDetector_OnSwipLeft()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Left;
        }
    }
    public void AddBrick(GameObject dashObj)
    {
        countBrick++;

        //Di chuyen vien gach ve vi tri DashParent
        dashObj.transform.SetParent(DashParent.transform);
        // 0.3f la do cao vien gach 
        dashObj.transform.localPosition = new Vector3(PrevDash.transform.localPosition.x, PrevDash.transform.localPosition.y + sizeBrick * countBrick, PrevDash.transform.localPosition.z);
       
        //tang chieu cao player them 0.3f 
        People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y + sizeBrick, People.transform.localPosition.z);

    }

    [Obsolete]
    public void RemoveBrick(GameObject dashObj)
    {
        if (countBrick > 0) 
        {
            countBrick--;
            //Debug.Log("List Brick Size:" + DashParent.transform.GetChild(countBrick).gameObject.CompareTag("normal"));
            if (DashParent.transform.GetChild(countBrick + 1).gameObject.CompareTag("normal"))
            {
                
                DestroyObject(DashParent.transform.GetChild(countBrick + 1).gameObject);
            }
            dashObj.SetActive(false);
            People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y - sizeBrick, People.transform.localPosition.z);
        }
        
    }

    [Obsolete]
    public void ClearBrick()
    {
        if (countBrick > 0)
        {
            for (int i = 1; i < countBrick+1; i++)
            {
                if (DashParent.transform.GetChild(i).gameObject.CompareTag("normal"))
                {
                    DestroyObject(DashParent.transform.GetChild(i).gameObject);
                }
                People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y - sizeBrick, People.transform.localPosition.z);
            }
        }
        
    }

}