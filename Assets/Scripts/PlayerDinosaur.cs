using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerDinosaur : MonoBehaviour
{

    private Animator Animator;
    private CameraTarget CameraTarget;
    private bool isSprintingForward;
    private float timeAtSprintStart;
    private Vector3 basePosition;

    public float WalkSpeed = 1.0f;
    public float RunSpeed = 1.0f;
    public float SprintDistance;
    public float SprintTime = 1.0f;

    public Transform MouthPoint;
    public Transform HighFoodContainer;
    public Transform LowFoodContainer;

    void Start()
    {
        Animator = GetComponent<Animator>();
        CameraTarget = GetComponentInParent<CameraTarget>();
        if (!CameraTarget)
        {
            Destroy(gameObject);
            Debug.LogError("Parent does not contain CameraTarget", transform);
        }
        basePosition = transform.localPosition;
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
        Animator.SetBool("Running", false);
    }

    // Jumps up to grab something up high.
    public void GrabHigh()
    {
        Animator.SetTrigger("HighGrab");
        Animator.SetBool("Running", false);
    }

    // Briefly starts running forward.
    public void SprintForward()
    {
        Animator.SetBool("Running", true);
        isSprintingForward = true;
        timeAtSprintStart = Time.time;
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
        return isSprintingForward;
    }

    // Handles the moment the dinosaur grabs above.
    public void OnHighGrab()
    {
        const float NEW_DEPTH = 1f;
        float plantX;
        float mouthX = MouthPoint.transform.position.x;
        foreach (EdiblePlant plant in HighFoodContainer.GetComponentsInChildren<EdiblePlant>())
        {
            plantX = plant.transform.position.x;
            if (Mathf.Abs(plantX - mouthX) < plant.BoxCollider2D.size.x / 2f)
            {
                plant.transform.SetParent(MouthPoint.transform);
                plant.transform.localPosition = new Vector3(0f, 0f, NEW_DEPTH);
                break;
            }
        }
    }

    // Handles the moment the dinosaur grabs below.
    public void OnLowGrab()
    {
        const float NEW_DEPTH = 1f;
        float preyX;
        float mouthX = MouthPoint.transform.position.x;
        foreach (PreyDinosaur prey in LowFoodContainer.GetComponentsInChildren<PreyDinosaur>())
        {
            preyX = prey.transform.position.x;
            if (Mathf.Abs(preyX - mouthX) < prey.BoxCollider2D.size.x / 2f)
            {
                prey.Die();
                prey.transform.SetParent(MouthPoint.transform);
                prey.transform.localPosition = new Vector3(0f, 0f, NEW_DEPTH);
                break;
            }
        }
    }

    void Update()
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

        // Sprint forward to the right of the view.
        if (isSprintingForward)
        {
            Vector3 position = basePosition;
            float timeDelta = Time.time - timeAtSprintStart;
            if (timeDelta < SprintTime)
            {
                position.x += Mathf.Sin(timeDelta / SprintTime * Mathf.PI) * SprintDistance;
                if (state.IsName("Walk"))
                {
                    Animator.SetBool("Running", true);
                }
            }
            else
            {
                isSprintingForward = false;
                Walk();
            }
            transform.localPosition = position;
        }
    }
}
