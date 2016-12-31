using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PreyDinosaur : MonoBehaviour {

    private BoxCollider2D boxCollider2D;
    private Animator Animator;
    private bool alive = true;

    public float RunSpeed;

    public BoxCollider2D BoxCollider2D
    {
        get
        {
            return boxCollider2D;
        }
    }

    // Kills the dinosaur.
    public void Die()
    {
        Animator.SetTrigger("Die");
        alive = false;
    }

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
    }

    void Update ()
    {
        if (alive)
        {
            // Run.
            transform.Translate(RunSpeed * Time.deltaTime, 0f, 0f);
        }
	}
}
