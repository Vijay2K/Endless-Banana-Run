using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {
    public static bool tap, swipeUp, swipeDown, swipeRight, swipeLeft;
    private bool isDraging;
    private Vector2 startPos, swipeDelta;


    private void Update() {
        tap = swipeUp = swipeDown = swipeRight = swipeLeft = false;
        
        if(Input.touchCount > 0) {
            if(Input.touches[0].phase == TouchPhase.Began) {
                tap = true;
                isDraging = true;
                startPos = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
                ResetTouch();
            }
        }

        if(isDraging) {
            if(Input.touchCount > 0) {
                swipeDelta = Input.touches[0].position - startPos;
            }
        }

        if(swipeDelta.magnitude > 100) {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y)) {
                if(x > 0) {
                    swipeRight = true;
                } else {
                    swipeLeft = true;
                }
            } else {
                if(y > 0) {
                    swipeUp = true;
                } else {
                    swipeDown = true;
                }
            }

            ResetTouch();
        }
    }

    private void ResetTouch() {
        swipeDelta = Vector2.zero;
        tap = false;
        isDraging = false;
    }

}
