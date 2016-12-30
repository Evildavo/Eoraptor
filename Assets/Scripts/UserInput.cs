using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerDinosaur))]
public class UserInput : MonoBehaviour {

    private PlayerDinosaur PlayerDinosaur;
    private bool touching = false;
    private bool swiped = false;
    private Vector3 currentTouchPosition;
    private Vector3 positionAtTouch;
    private bool upSwipe;
    private bool downSwipe;
    private bool leftSwipe;
    private bool rightSwipe;

    public float MinSwipeDistance;

    void Start ()
    {
        PlayerDinosaur = GetComponent<PlayerDinosaur>();
    }
	
	void Update ()
    {
        // Keyboard input style.
        upSwipe = Input.GetKeyDown(KeyCode.UpArrow);
        downSwipe = Input.GetKeyDown(KeyCode.DownArrow);
        leftSwipe = Input.GetKeyDown(KeyCode.LeftArrow);
        rightSwipe = Input.GetKeyDown(KeyCode.RightArrow);

        // Get mouse/touch state.
        if (Input.touches.Length > 0)
        {
            currentTouchPosition = Input.touches[0].position;
            if (!touching)
            {
                positionAtTouch = currentTouchPosition;
                touching = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            currentTouchPosition = Input.mousePosition;
            if (!touching)
            {
                positionAtTouch = currentTouchPosition;
                touching = true;
            }
        }
        else
        {
            touching = false;
            swiped = false;
        }

        // Check for swipe motions.
        if (touching && !swiped)
        {
            Vector3 touchDelta = currentTouchPosition - positionAtTouch;
            if (touchDelta.magnitude > MinSwipeDistance)
            {
                swiped = true;

                if (touchDelta.x > 0f && Mathf.Abs(touchDelta.y) < touchDelta.x)
                    rightSwipe = true;
                else if (touchDelta.x < 0f && Mathf.Abs(touchDelta.y) < -touchDelta.x)
                    leftSwipe = true;
                else if (touchDelta.y > 0f && Mathf.Abs(touchDelta.x) < touchDelta.y)
                    upSwipe = true;
                else if (touchDelta.y < 0f && Mathf.Abs(touchDelta.x) < -touchDelta.y)
                    downSwipe = true;
            }
        }

        // Control dinosaur.
        if (PlayerDinosaur.IsSprinting())
        {
            if (upSwipe)
            {
                PlayerDinosaur.GrabHigh();
            }
            else if (downSwipe)
            {
                PlayerDinosaur.GrabLow();
            }
        }
        else if (PlayerDinosaur.IsStopped())
        {
            if (rightSwipe)
            {
                PlayerDinosaur.Walk();
            }
        }
        else
        {
            if (upSwipe)
            {
                PlayerDinosaur.GrabHigh();
            }
            else if (downSwipe)
            {
                PlayerDinosaur.GrabLow();
            }
            else if (rightSwipe)
            {
                PlayerDinosaur.SprintForward();
            }
            else if (leftSwipe)
            {
                PlayerDinosaur.Stop();
            }
        }
    }
}
