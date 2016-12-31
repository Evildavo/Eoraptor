using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class EndMessage : MonoBehaviour {

    private TextMesh TextMesh;

    public void ShowVictory()
    {
        GetComponent<Renderer>().enabled = true;
        TextMesh.text = "Good job!";
    }

    public void ShowDefeat()
    {
        GetComponent<Renderer>().enabled = true;
        TextMesh.text = "You're still hungry!";
    }

	void Start () {
        TextMesh = GetComponent<TextMesh>();
    }
	
	void Update () {
	
	}
}
