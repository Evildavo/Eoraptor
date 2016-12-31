﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ScreenFade : MonoBehaviour {

    private float visibility;
    private bool fadingOut;
    private SpriteRenderer SpriteRenderer;

    public float FadeTime = 1f;

    public void StartFadeOut()
    {
        fadingOut = true;
        SpriteRenderer.enabled = true;
        updateTransparency();
    }

    void updateTransparency()
    {
        Color colour = SpriteRenderer.color;
        colour.a = visibility;
        SpriteRenderer.color = colour;
    }

	void Start () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        // Update fade.
	    if (fadingOut)
        {
            visibility += Time.deltaTime / FadeTime;
            if (visibility >= 1f)
            {
                visibility = 1f;
                fadingOut = false;
            }
            else
            {
                updateTransparency();
            }
        }
	}
}
