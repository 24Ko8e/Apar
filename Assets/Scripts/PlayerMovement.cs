using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float deadZone = 80.0f;

    private bool swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;
    private float sqrDeadZone;

    #region Public Properties
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    #endregion

    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 swipeDir;

    void Start()
    {
        sqrDeadZone = deadZone * deadZone;
        moveDir = Vector2.zero;
    }

    void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = false;

        CheckSwipe();

        transform.Translate(moveDir * 2.5f * Time.deltaTime);
    }

     void CheckSwipe()
    {
        if(Input.touches.Length != 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }

        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.touches.Length != 0)
        {
            swipeDelta = Input.touches[0].position - startTouch;
        }

        if (swipeDelta.sqrMagnitude > sqrDeadZone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                    if (moveDir == Vector2.right)
                        moveDir = Vector2.left;

                    swipeDir = Vector2.left;
                }
                else
                {
                    swipeRight = true;
                    if (moveDir == Vector2.left)
                        moveDir = Vector2.right;

                    swipeDir = Vector2.right;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                    if (moveDir == Vector2.up)
                        moveDir = Vector2.down;

                    swipeDir = Vector2.down;
                }
                else
                {
                    swipeUp = true;
                    if (moveDir == Vector2.down)
                        moveDir = Vector2.up;

                    swipeDir = Vector2.up;
                }
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
