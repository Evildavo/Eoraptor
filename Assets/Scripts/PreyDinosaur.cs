using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PreyDinosaur : MonoBehaviour {

    private BoxCollider2D boxCollider2D;

    public float RunSpeed;

    public BoxCollider2D BoxCollider2D
    {
        get
        {
            return boxCollider2D;
        }
    }

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update () {
        transform.Translate(RunSpeed * Time.deltaTime, 0f, 0f);
	}
}
