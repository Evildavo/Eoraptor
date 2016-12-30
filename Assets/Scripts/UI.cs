using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    public SwipeGuide SwipeUpGuide;
    public SwipeGuide SwipeDownGuide;
    public SwipeGuide SwipeLeftGuide;
    public SwipeGuide SwipeRightGuide;
    
    public void ShowSwipeUpGuide()
    {
        SwipeUpGuide.Swipe();
    }

    public void ShowSwipeDownGuide()
    {
        SwipeDownGuide.Swipe();
    }

    public void ShowSwipeLeftGuide()
    {
        SwipeLeftGuide.Swipe();
    }

    public void ShowSwipeRightGuide()
    {
        SwipeRightGuide.Swipe();
    }

    void Start () {
	
	}
	
	void Update () {
	
	}
}
