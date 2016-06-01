using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartThatShit()
    {
        Debug.Log("Starting Game");
        Application.LoadLevel(1);
    }
}
