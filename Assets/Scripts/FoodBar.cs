using UnityEngine;
using System.Collections;

public class FoodBar : MonoBehaviour {

    private float barWidth;
    private float fullBarScaleX;
    private float progress;

    public float Progress
    {
        get { return progress; }
        set
        {
            progress = value;
            UpdateBar();
        }
    }

    [Range(0f, 1f)]
    public float Quota = 0.6f;

    public Transform LeftEnd;
    public Transform RightEnd;
    public Transform BarFull;
    public Transform QuotaIcon;

    public void UpdateQuota()
    {
        QuotaIcon.position =
            LeftEnd.position + 
            new Vector3(Quota * barWidth, 0f, 0f);
    }

    public void UpdateBar()
    {
        BarFull.localScale = new Vector3(Progress * fullBarScaleX, 1f, 1f);
    }

    void Start ()
    {
        barWidth = RightEnd.position.x - LeftEnd.position.x;
        fullBarScaleX = BarFull.localScale.x;

        UpdateQuota();
        UpdateBar();
    }
	
	void Update ()
    {
    }
}
