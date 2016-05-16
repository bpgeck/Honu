using UnityEngine;
using System.Collections;

public class ToUpstairs : MonoBehaviour
{
    Vector3 upstairsPos;

	void Start ()
    {
        upstairsPos = GameObject.Find("TopOfStairs").transform.position;
	}
	
	void Update ()
    {
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.transform.position = upstairsPos;
    }
}
