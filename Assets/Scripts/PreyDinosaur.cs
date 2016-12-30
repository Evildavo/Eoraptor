using UnityEngine;
using System.Collections;

public class PreyDinosaur : MonoBehaviour {

    public float RunSpeed;

	void Start () {
	
	}
	
	void Update () {
        transform.Translate(RunSpeed * Time.deltaTime, 0f, 0f);
	}
}
