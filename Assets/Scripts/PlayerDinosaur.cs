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
    private bool endReached;

    public float WalkSpeed = 1.0f;
    public float RunSpeed = 1.0f;
    public float SprintDistance;
    public float SprintTime = 1.0f;

    public Transform MouthPoint;
    public Transform HighFoodContainer;
    public Transform LowFoodContainer;

    [Space(10f)]
    public Transform StopPoint;

    public UI UI;

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
        clearMouth();
    }

    // Jumps up to grab something up high.
    public void GrabHigh()
    {
        Animator.SetTrigger("HighGrab");
        clearMouth();
    }

    // Clears the dinosaur's mouth of food.
    void clearMouth()
    {
        foreach (Food item in MouthPoint.GetComponentsInChildren<Food>())
            Destroy(item.gameObject);
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

    // Returns true if the dinosaur has reached the water hole.
    public bool IsEndReached()
    {
        return endReached;
    }

    // Handles the moment the dinosaur grabs above.
    public void OnHighGrab()
    {
        // Check for food caught.
        const float NEW_DEPTH = 1f;
        float plantX;
        float mouthX = MouthPoint.transform.position.x;
        foreach (EdiblePlant plant in HighFoodContainer.GetComponentsInChildren<EdiblePlant>())
        {
            plantX = plant.transform.position.x;
            if (Mathf.Abs(plantX - mouthX) < plant.BoxCollider2D.size.x / 2f)
            {
                // Food was caught.
                UI.FoodBar.Progress += plant.GetComponent<Food>().FoodValue;
                plant.transform.SetParent(MouthPoint.transform);
                plant.transform.localPosition = new Vector3(0f, 0f, NEW_DEPTH);
                break;
            }
        }
    }

    // Handles the moment the dinosaur grabs below.
    public void OnLowGrab()
    {
        // Check for food caught.
        const float NEW_DEPTH = 1f;
        float preyX;
        float mouthX = MouthPoint.transform.position.x;
        foreach (PreyDinosaur prey in LowFoodContainer.GetComponentsInChildren<PreyDinosaur>())
        {
            preyX = prey.transform.position.x;
            if (Mathf.Abs(preyX - mouthX) < prey.BoxCollider2D.size.x / 2f)
            {
                // Food was caught.
                UI.FoodBar.Progress += prey.GetComponent<Food>().FoodValue;
                prey.Die();
                prey.transform.SetParent(MouthPoint.transform);
                prey.transform.localPosition = new Vector3(0f, 0f, NEW_DEPTH);
                break;
            }
        }
    }

    IEnumerator FadeOutAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        UI.GetComponentInChildren<ScreenFade>().StartFadeOut();
    }

    void Update()
    {
        // Stop when past the stop point.
        if (transform.position.x >= StopPoint.transform.position.x)
        {
            endReached = true;
            Stop();

            // Determine if we won or not.
            if (UI.FoodBar.Progress >= UI.FoodBar.Quota)
            {
                UI.GetComponentInChildren<EndMessage>().ShowVictory();
            }
            else
            {
                UI.GetComponentInChildren<EndMessage>().ShowDefeat();
            }

            // Fade out after a delay.
            StartCoroutine(FadeOutAfterDelay(5));
        }

        // Move forward based on the animation state.
        AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);

        if (!Animator.GetBool("Stopped") && 
            (state.IsName("Walk") || state.IsName("LowGrab") || state.IsName("HighGrab")))
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
