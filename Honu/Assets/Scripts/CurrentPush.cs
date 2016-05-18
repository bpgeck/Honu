using UnityEngine;
using System.Collections;

public class CurrentPush : MonoBehaviour
{
    int pushDirection;

    void Start()
    {
        if (this.name.Contains("Up"))
        {
            pushDirection = 1;
        }
        else if (this.name.Contains("Left"))
        {
            pushDirection = 2;
        }
        else if (this.name.Contains("Down"))
        {
            pushDirection = 3;
        }
        else if (this.name.Contains("Right"))
        {
            pushDirection = 4;
        }
        else // this should never happen
        {
            Debug.LogError("I do not recognize this tile name");
        }
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<CharacterMovement>().Push(pushDirection, this.transform.position);
    }
}
