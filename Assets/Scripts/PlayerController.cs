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
    [SerializeField] private BrickManager brickManager;
    private Vector3 StartPoint;
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

    string level = "Level";
    int inGameLevel;
    string brick = "Brick";
    int inGameBrick;

    // private Dictionary<GameObject, bool> current_obj_state;
    private List<Brick> brickList;
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
        //current_obj_state = new Dictionary<GameObject, bool>();
        brickList = new List<Brick>();
        inGameLevel = 0;
        StartPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        if (swipeDetector != null)
        {
            swipeDetector.onSwipeLeft += SwipeDetector_OnSwipLeft;
            swipeDetector.onSwipeRight += SwipeDetector_OnSwipRight;
            swipeDetector.onSwipeUp += SwipeDetector_OnSwipUp;
            swipeDetector.onSwipeDown += SwipeDetector_OnSwipDown;
        }
        if (brickManager != null)
        {
            brickManager.AddBrick += AddBrick;
            brickManager.ClearBrick += ClearBrick;
            brickManager.RemoveBrick += RemoveBrick;
            brickManager.WinPos += WinPos;
        }
        OnInit();
    }

    public void OnInit()
    {
        if (brickList.Count > 0) 
        {
            reset_obj_Value();
            //current_obj_state.Clear();
            brickList.Clear();
        }
        
        inGameLevel = PlayerPrefs.GetInt(level, 0);
        inGameBrick = PlayerPrefs.GetInt(brick, 0);
        transform.position = new Vector3(StartPoint.x + (30 * inGameLevel) , StartPoint.y, StartPoint.z);

        isWin = false;
        isMoving = false;
        direct = Direct.None;
        countBrick = 0;
        UIManager.instance.SetBrick(inGameBrick);
        UIManager.instance.SetLevel(inGameLevel+1);
        UIManager.instance.isNextButton(isWin);
        UIManager.instance.isReplayButton(isWin);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PrevDash == null)
        {
            return;
        }
        if (!isWin) 
        {
            if (isMoving && direct == Direct.Left)
            {
                Moving(Vector3.left);
            }
            if (isMoving && direct == Direct.Right)
            {
                Moving(Vector3.right);
            }
            if (isMoving && direct == Direct.Up)
            {
                Moving(Vector3.forward);
            }
            if (isMoving && direct == Direct.Down)
            {
                Moving(Vector3.back);
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
    private void Moving(Vector3 vector3)
    {
        RaycastHit hit;
        LayerMask brickLayer = LayerMask.GetMask("Line");
        if (Physics.Raycast(PrevDash.transform.position, vector3, out hit, 1f, brickLayer))
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
    private void AddBrick(GameObject brickObj)
    {
        //brickObj.gameObject.tag = "normal";
        brickObj.gameObject.SetActive(false);
        countBrick++;
        inGameBrick++;

        //current_obj_state.Add(brickObj, false);
        Brick item = new Brick(brickObj, false);
        brickList.Add(item);
        UIManager.instance.SetBrick(inGameBrick);
        GameObject clone_brick = Instantiate(brickObj);
        clone_brick.gameObject.SetActive(true);
        clone_brick.gameObject.tag = "normal";
        //Di chuyen vien gach ve vi tri DashParent
        clone_brick.transform.SetParent(DashParent.transform);
        // 0.3f la do cao vien gach 
        clone_brick.transform.localPosition = new Vector3(PrevDash.transform.localPosition.x, PrevDash.transform.localPosition.y + sizeBrick * countBrick, PrevDash.transform.localPosition.z);
       
        //tang chieu cao player them 0.3f 
        People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y + sizeBrick, People.transform.localPosition.z);

    }


    private void RemoveBrick(GameObject brickObj)
    {
        if (countBrick > 0 && brickObj.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            brickObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            brickObj.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            //current_obj_state.Add(brickObj.gameObject.transform.GetChild(0).gameObject, false);
            //current_obj_state.Add(brickObj.gameObject.transform.GetChild(1).gameObject, true);

            Brick item = new Brick(brickObj.gameObject.transform.GetChild(0).gameObject, false);
            brickList.Add(item);
            Brick item1 = new Brick(brickObj.gameObject.transform.GetChild(1).gameObject, true);
            brickList.Add(item1);

            countBrick--;
            inGameBrick--;
 

            UIManager.instance.SetBrick(inGameBrick);
            //Debug.Log("List Brick Size:" + DashParent.transform.GetChild(countBrick).gameObject.CompareTag("normal"));
            if (DashParent.transform.GetChild(countBrick + 1).gameObject.CompareTag("normal"))
            {
                //DestroyObject(DashParent.transform.GetChild(countBrick + 1).gameObject);
                Destroy(DashParent.transform.GetChild(countBrick + 1).gameObject);
            }
            People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y - sizeBrick, People.transform.localPosition.z);
        }
    }


    private void ClearBrick()
    {
  
        if (DashParent.transform.childCount > 0)
        {
            for (int i = 0; i < DashParent.transform.childCount; i++)
            {
                if (DashParent.transform.GetChild(i).gameObject.CompareTag("normal"))
                {
                    //DestroyObject(DashParent.transform.GetChild(i).gameObject);
                    Destroy(DashParent.transform.GetChild(i).gameObject);
                    People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y - sizeBrick, People.transform.localPosition.z);
                }
            }
            countBrick=0;
        }
    }
    private void WinPos(GameObject boxObj)
    {
        if (!isWin) 
        {
            //boxObj.gameObject.tag = "Untagged";
            boxObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            boxObj.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            //current_obj_state.Add(boxObj.gameObject.transform.GetChild(0).gameObject, false);
            //current_obj_state.Add(boxObj.gameObject.transform.GetChild(1).gameObject, true);

            Brick item = new Brick(boxObj.gameObject.transform.GetChild(0).gameObject, false);
            brickList.Add(item);
            Brick item1 = new Brick(boxObj.gameObject.transform.GetChild(1).gameObject, true);
            brickList.Add(item1);

            isWin = true;

            PlayerPrefs.SetInt(brick, inGameBrick);
            PlayerPrefs.Save();
            UIManager.instance.isNextButton(isWin);
            UIManager.instance.isReplayButton(isWin);
        }
        
    }
    public void nextLevel() 
    {
        //Level = inGameLevel +1, check Level <5 -> level +++ else ko doi
        if (inGameLevel < 4) 
        {
            inGameLevel++;
            PlayerPrefs.SetInt(level, inGameLevel);
            PlayerPrefs.Save();
        }

        OnInit();
    }
    public void replay()
    {
        PlayerPrefs.SetInt(level, inGameLevel);
        PlayerPrefs.Save();
        OnInit();
    }
    

    public void reset_obj_Value()
    {
        /*foreach (KeyValuePair<GameObject, bool> allactobj in current_obj_state)
        {
            allactobj.Key.gameObject.SetActive(!allactobj.Value);
        }*/
        for (int i=0; i< brickList.Count; i++)
        {
            brickList[i].GetbrickObj().gameObject.SetActive(!brickList[i].GetisActive());
        }
    }
}