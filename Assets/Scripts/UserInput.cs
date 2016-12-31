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

    public UI UI;

    public float MinSwipeDistance;

    void Start ()
    {
        PlayerDinosaur = GetComponent<PlayerDinosaur>();
    }
	
	void Update ()
    {
        upSwipe = false;
        downSwipe = false;
        leftSwipe = false;
        rightSwipe = false;

        // Keyboard input style.
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            upSwipe = true;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            downSwipe = true;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            leftSwipe = true;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            rightSwipe = true;

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
        
        // Check for arrow button presses.
        if (touching && !swiped)
        {
            if (UI.SwipeUpGuide.IsPointOver(positionAtTouch))
            {
                upSwipe = true;
                swiped = true;
            }
            else if (UI.SwipeDownGuide.IsPointOver(positionAtTouch))
            {
                downSwipe = true;
                swiped = true;
            }
            else if (UI.SwipeLeftGuide.IsPointOver(positionAtTouch))
            {
                leftSwipe = true;
                swiped = true;
            }
            else if (UI.SwipeRightGuide.IsPointOver(positionAtTouch))
            {
                rightSwipe = true;
                swiped = true;
            }
        }

        // Control dinosaur.
        if (PlayerDinosaur.IsSprinting())
        {
            if (upSwipe)
            {
                PlayerDinosaur.GrabHigh();
                UI.SwipeUpGuide.Swipe();
            }
            else if (downSwipe)
            {
                PlayerDinosaur.GrabLow();
                UI.SwipeDownGuide.Swipe();
            }
        }
        else if (PlayerDinosaur.IsStopped())
        {
            if (rightSwipe)
            {
                PlayerDinosaur.Walk();
                UI.SwipeRightGuide.Swipe();
            }
            if (upSwipe)
            {
                PlayerDinosaur.GrabHigh();
                UI.SwipeUpGuide.Swipe();
            }
            else if (downSwipe)
            {
                PlayerDinosaur.GrabLow();
                UI.SwipeDownGuide.Swipe();
            }
        }
        else
        {
            if (upSwipe)
            {
                PlayerDinosaur.GrabHigh();
                UI.SwipeUpGuide.Swipe();
            }
            else if (downSwipe)
            {
                PlayerDinosaur.GrabLow();
                UI.SwipeDownGuide.Swipe();
            }
            else if (rightSwipe)
            {
                PlayerDinosaur.SprintForward();
                UI.SwipeRightGuide.Swipe();
            }
            else if (leftSwipe)
            {
                PlayerDinosaur.Stop();
                UI.SwipeLeftGuide.Swipe();
            }
        }

        // Update swipe guides.
        if (!UI.SwipeUpGuide.IsSwiping() &&
            !UI.SwipeDownGuide.IsSwiping() &&
            !UI.SwipeLeftGuide.IsSwiping() &&
            !UI.SwipeRightGuide.IsSwiping())
        {
            if (PlayerDinosaur.IsSprinting())
            {
                UI.SwipeUpGuide.Show();
                UI.SwipeDownGuide.Show();
                UI.SwipeLeftGuide.Hide();
                UI.SwipeRightGuide.Hide();
            }
            else if (PlayerDinosaur.IsStopped())
            {
                UI.SwipeUpGuide.Show();
                UI.SwipeDownGuide.Show();
                UI.SwipeLeftGuide.Hide();
                UI.SwipeRightGuide.Show();
            }
            else
            {
                UI.SwipeUpGuide.Show();
                UI.SwipeDownGuide.Show();
                UI.SwipeLeftGuide.Show();
                UI.SwipeRightGuide.Show();
            }
        }
    }
}
