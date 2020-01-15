using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterSwipe : MonoBehaviour {
    private const float DEADZONE = 50f;
    public enum Swipes
    {
        right,
        left,
        up,
        down,
        tap,
        hold
    };

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public static bool SwipeLeft { get { return swipeLeft; } }
    public static bool SwipeRight { get { return swipeRight; } }
    public static bool SwipeUp { get { return swipeUp; } }
    public static bool SwipeDown { get { return swipeDown; } }

    private static bool tap, hold, swipeLeft, swipeRight, swipeDown, swipeUp;
    private Vector2 swipeDelta, startTouch;

    void Update ()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if(Input.GetKeyDown(KeyCode.A))
        {
            swipeLeft = true;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            swipeRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            swipeDown = true;
        }

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                hold = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
                hold = false;
            }
        }
        // Calculate distance
        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero)
        {
            if(Input.touchCount > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
        }
        if(swipeDelta.magnitude > DEADZONE)
        {
            // Confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left of right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                // Up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
