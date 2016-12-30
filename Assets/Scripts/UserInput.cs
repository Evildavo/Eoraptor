using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerDinosaur))]
public class UserInput : MonoBehaviour {

    private PlayerDinosaur PlayerDinosaur;

	void Start ()
    {
        PlayerDinosaur = GetComponent<PlayerDinosaur>();
    }
	
	void Update ()
    {
        // Keyboard input.
        if (PlayerDinosaur.IsSprinting())
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PlayerDinosaur.Walk();
        }
        else if (PlayerDinosaur.IsStopped())
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                PlayerDinosaur.Walk();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                PlayerDinosaur.GrabHigh();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayerDinosaur.GrabLow();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayerDinosaur.SprintForward();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayerDinosaur.Stop();
            }
        }
    }
}
