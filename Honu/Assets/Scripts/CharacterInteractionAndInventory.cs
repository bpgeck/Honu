using UnityEngine;
using System.Collections;

public class CharacterInteractionAndInventory : MonoBehaviour
{
    /* 
        Directions:
        1: Up
        2: Left
        3: Down
        4: Right
    */

    // Super Awesome Inventory Management
    public bool hasToolbox;
    public bool hasScubaGear;
    public bool hasPropeller;
    public bool hasBirthday;
    public bool hasHint;
    public int numDriftwood;

    public int directionFacing = 3;
    float checkDistance = 0.25f;

	void Start ()
    {
        hasToolbox = false;
        hasScubaGear = false;
        hasPropeller = false;
        hasBirthday = false;
        hasHint = false;
        numDriftwood = 0;
	}
	
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Checking");

            Vector3 rayDirection = new Vector3(0, 0, 0);
            if (directionFacing == 1) rayDirection = new Vector3(0, checkDistance, 0);
            else if (directionFacing == 2) rayDirection = new Vector3(-checkDistance, 0, 0);
            else if (directionFacing == 3) rayDirection = new Vector3(0, -checkDistance, 0);
            else if (directionFacing == 4) rayDirection = new Vector3(checkDistance, 0, 0);
            else Debug.LogError("Not facing any direction when trying to interact");

            Ray checkInFrontRay = new Ray(this.transform.position, rayDirection);
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, rayDirection, checkDistance);
            Debug.DrawRay(this.transform.position, rayDirection, Color.red);
            
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log(hits[i].collider.name);

                if (hits[i].collider.tag == "Interactable")
                {
                    if (hits[i].collider.name.Contains("Toolbox"))
                    {
                        Debug.Log("Grabbing Toolbox");

                        hasToolbox = true;
                        hits[i].collider.gameObject.SetActive(false);
                    }
                    else if (hits[i].collider.name.Contains("Trash"))
                    {
                        Debug.Log("Looking in Trashcan");

                        if (hasToolbox)
                        {
                            hasPropeller = true;
                            hits[i].collider.gameObject.tag = "Untagged";
                        }
                    }
                    else if (hits[i].collider.name.Contains("Driftwood"))
                    {
                        Debug.Log("Grabbing Driftwood");

                        numDriftwood++;
                        hits[i].collider.gameObject.SetActive(false);
                    }
                    else if (hits[i].collider.name.Contains("Closet"))
                    {
                        Debug.Log("Opening Closet");

                        if (hasHint && hasBirthday)
                        {
                            hasScubaGear = true;
                            hits[i].collider.gameObject.SetActive(false);
                            GameObject.Find("OpenLockedCloset").GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                    else if (hits[i].collider.name.Contains("Calendar"))
                    {
                        Debug.Log("Reading Calendar");

                        hasBirthday = true;
                        hits[i].collider.gameObject.tag = "Untagged";
                    }
                    else if (hits[i].collider.name.Contains("Computer"))
                    {
                        Debug.Log("Reading Computer");

                        hasHint = true;
                        hits[i].collider.gameObject.tag = "Untagged";
                    }
                }
            }
        }
	}
}
