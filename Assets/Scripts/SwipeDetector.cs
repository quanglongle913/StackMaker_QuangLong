using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 swipeDelta;

    private bool isTouched = false;

    private const float SWIPE_THRESHHOLD = 100;

    public UnityAction onSwipeRight;
    public UnityAction onSwipeLeft;
    public UnityAction onSwipeUp;
    public UnityAction onSwipeDown;

    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            isTouched = true;
            fingerDownPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fingerDownPos = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;
        if (fingerDownPos != Vector2.zero)
        {

            if (Input.touches.Length != 0)
            {
               
                swipeDelta = Input.touches[0].position - fingerDownPos;
                DetectorSwipe();
            }

            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - fingerDownPos;
                DetectorSwipe();

            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0) 
        {
            Touch curTouch = Input.GetTouch(0);
            if (!isTouched && curTouch.phase == TouchPhase.Began) 
            {
                fingerDownPos = curTouch.deltaPosition;
                isTouched = true;
            }

            if (isTouched)
            {
                if (curTouch.phase == TouchPhase.Moved)
                {
                    swipeDelta = (Vector2)curTouch.deltaPosition - fingerDownPos;
                    DetectorSwipe();
                }
            }

            if (isTouched && curTouch.phase == TouchPhase.Ended) 
            {
                fingerDownPos = Vector2.zero;
                isTouched = false;
            }
        }
#endif


    }
    void DetectorSwipe()
    {
        if (swipeDelta.sqrMagnitude > SWIPE_THRESHHOLD)
        {

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                {
                    //Debug.Log("SwipeLeft");
                    onSwipeLeft();
                }
                else
                {
                    //Debug.Log("swipeRight");
                    onSwipeRight();
                }
            }
            else
            {

                if (y < 0)
                {
                    //Debug.Log("swipeDown");
                    onSwipeDown();
                }
                else
                {
                    //Debug.Log("swipeUp");
                    onSwipeUp();
                }
            }
            if (Input.touches.Length != 0)
            {
                fingerDownPos = Input.touches[0].position;

            }

            else if (Input.GetMouseButton(0))
            {
                fingerDownPos = (Vector2)Input.mousePosition;
            }
            swipeDelta = Vector2.zero;
        }
    }
} 
