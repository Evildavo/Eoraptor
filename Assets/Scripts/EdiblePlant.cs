using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class EdiblePlant : MonoBehaviour {

    private BoxCollider2D boxCollider2D;

    public BoxCollider2D BoxCollider2D
    {
        get
        {
            return boxCollider2D;
        }
    }
    
    void Start ()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	void Update () {
	
	}
}
