using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance { set; get; }
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public Vector2 swipeDelta, startTouch;
    private const float deadZone = 100;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {

        tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;

        //Input for mouse and Touch


        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {

            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }

            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;

            }
        }


        if (swipeDelta.magnitude > deadZone)
        {

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                {
                    //Debug.Log("SwipeLeft");
                    swipeLeft = true;
                }
                else
                {
                    //Debug.Log("swipeRight");
                    swipeRight = true;
                }
            }
            else
            {

                if (y < 0)
                {
                    //Debug.Log("swipeDown");
                    swipeDown = true;
                }
                else
                {
                    //Debug.Log("swipeUp");
                    swipeUp = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
