using UnityEngine;
using System.Collections;

public class PlayerDinosaur : MonoBehaviour {

    private Animator Animator;
    private CameraTarget CameraTarget;

    public float WalkSpeed = 1.0f;
    public float RunSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        Animator = GetComponent<Animator>();
        CameraTarget = GetComponentInParent<CameraTarget>();
	}
	
	// Update is called once per frame
	void Update ()
    {        
        // Move forward based on the animation state. 
        AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);
  
        if (state.IsName("Walk") || state.IsName("LowGrab") || state.IsName("HighGrab"))
        {
            CameraTarget.transform.Translate(WalkSpeed * Time.deltaTime, 0f, 0f);
        }
        else if (state.IsName("Run"))
        {
            CameraTarget.transform.Translate(RunSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}
