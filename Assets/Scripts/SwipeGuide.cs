using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class SwipeGuide : MonoBehaviour {

    private Vector3 basePosition;
    private float timeAtSwipe;
    private bool isSwiping = false;
    private SpriteRenderer SpriteRenderer;
    private BoxCollider2D BoxCollider2D;

    public float SwipeDistance;
    public float SwipeTime = 1.0f;

    public void Swipe()
    {
        isSwiping = true;
        timeAtSwipe = Time.time;
        SpriteRenderer.enabled = true;
    }

    public void Show()
    {
        SpriteRenderer.enabled = true;
    }

    public void Hide()
    {
        SpriteRenderer.enabled = false;
    }

    public bool IsSwiping()
    {
        return isSwiping;
    }

    public bool IsPointOver(Vector3 point)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(point);
        return BoxCollider2D.OverlapPoint(worldPosition);
    }

	void Start () {
        basePosition = transform.localPosition;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	void Update ()
    {
	    if (isSwiping)
        {
            // Swipe forward.
            float timeDelta = Time.time - timeAtSwipe;
            if (timeDelta < SwipeTime)
            {
                transform.localPosition = basePosition +
                    transform.rotation * new Vector3(0f, timeDelta / SwipeTime * SwipeDistance, 0f);
                Color colour = SpriteRenderer.color;
                colour.a = 1f - timeDelta / SwipeTime;
            }
            else
            {
                transform.localPosition = basePosition;
                SpriteRenderer.enabled = false;
                isSwiping = false;
            }
        }
	}
}
