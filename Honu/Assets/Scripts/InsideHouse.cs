using UnityEngine;
using System.Collections;

public class InsideHouse : MonoBehaviour
{
    
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Application.LoadLevel(0);
    }
}
