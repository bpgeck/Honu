using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float pushSpeed = 2;
    float tileLength = 0.2f;

    public Quaternion startRotation;

    Vector3 lastPosition;
    Vector3 nextPosition;

    Vector3 pushEndPosition;

    bool pushed;

	void Start ()
    {
        startRotation = this.transform.rotation;
    }
	
	void Update ()
    {
        this.transform.rotation = startRotation;

        if (pushed)
        {
            transform.position = Vector3.Lerp(this.transform.position, pushEndPosition, pushSpeed * Time.deltaTime);
            if (Mathf.Abs(transform.position.x - pushEndPosition.x) < 0.05f && Mathf.Abs(transform.position.y - pushEndPosition.y) < 0.05f) // when close to end point, stop pushing
            {
                pushed = false;
            }
        }

        else
        {
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

    public void Push(int direction, Vector3 tilePosition)
    {
        pushed = true;
        if (direction == 1) // 1 is UP
        {
            pushEndPosition = new Vector3(tilePosition.x, tilePosition.y + tileLength, this.transform.position.z);
        }
        else if (direction == 2) // 2 is LEFT
        {
            pushEndPosition = new Vector3(tilePosition.x - tileLength, tilePosition.y, this.transform.position.z);
        }
        else if (direction == 3) // 3 is BACK
        {
            pushEndPosition = new Vector3(tilePosition.x, tilePosition.y - tileLength, this.transform.position.z);
        }
        else if (direction == 4) // 4 is RIGHT
        {
            pushEndPosition = new Vector3(tilePosition.x + tileLength, tilePosition.y, this.transform.position.z);
        }
        else // this should never happen
        {
            Debug.LogError("Movement direction is invalid");
        }

        Debug.Log(transform.position);
        Debug.Log(pushEndPosition);
    }
}
