using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerDinosaur : MonoBehaviour {

    private Animator Animator;
    private CameraTarget CameraTarget;

    public float WalkSpeed = 1.0f;
    public float RunSpeed = 1.0f;
    
    void Start ()
    {
        Animator = GetComponent<Animator>();
        CameraTarget = GetComponentInParent<CameraTarget>();
        if (!CameraTarget)
        {
            Destroy(gameObject);
            Debug.LogError("Parent does not contain CameraTarget", transform);
        }
    }

    // Returns to default walk state.
    public void Walk()
    {
        Animator.SetBool("Running", false);
        Animator.SetBool("Stopped", false);
    }

    // Ducks down to grab something down low.
    public void GrabLow()
    {
        Animator.SetTrigger("LowGrab");
    }

    // Jumps up to grab something up high.
    public void GrabHigh()
    {
        Animator.SetTrigger("HighGrab");
    }

    // Briefly starts running forward.
    public void SprintForward()
    {
        Animator.SetBool("Running", true);
    }

    // Slows to a stop.
    public void Stop()
    {
        Animator.SetBool("Stopped", true);
    }

    // Returns true if stopped.
    public bool IsStopped()
    {
        return Animator.GetBool("Stopped");
    }

    // Returns true if sprinting.
    public bool IsSprinting()
    {
        return Animator.GetBool("Running");
    }

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
