using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 0.5f;

    public Quaternion startRotation;

    Vector3 lastPosition;
    Vector3 nextPosition;

	void Start ()
    {
        startRotation = this.transform.rotation;
	}
	
	void Update ()
    {
        this.transform.rotation = startRotation;

        // Use WASD to move
	    if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(-Vector3.left * speed * Time.deltaTime);
        }

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
    }
}
