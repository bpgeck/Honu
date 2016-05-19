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

    bool hullFixed;
    bool propFixed;

    public int directionFacing = 3;
    float checkDistance = 0.25f;

    void Start()
    {
        hasToolbox = false;
        hasScubaGear = false;
        hasPropeller = false;
        hasBirthday = false;
        hasHint = false;

        hullFixed = false;
        propFixed = false;
        numDriftwood = 0;
    }

    void Update()
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

                        // SPEECH BUBBLE
                        // Text: "Hmmm, maybe this toolbox will come in handy"

                        hasToolbox = true;
                        hits[i].collider.gameObject.SetActive(false);
                    }
                    else if (hits[i].collider.name.Contains("Trash"))
                    {
                        Debug.Log("Looking in Trashcan");

                        if (hasToolbox)
                        {
                            // SPEECH BUBBLE
                            // Text: "One man's trash is another man's propeller"

                            hasPropeller = true;
                            hits[i].collider.gameObject.tag = "Untagged";
                        }
                        else
                        {
                            // SPEECH BUBBLE
                            // Text: "Hey there's a broken fan in here. Nothing looks broken. I guess the motor must have gone out."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Driftwood"))
                    {
                        // SPEECH BUBBLE
                        // Text: "Driftwood. That might come in handy."

                        Debug.Log("Grabbing Driftwood");

                        numDriftwood++;
                        hits[i].collider.gameObject.SetActive(false);

                        if (numDriftwood == 5)
                        {
                            // SPEECH BUBBLE
                            // Text: "Alright that's all the driftwood I can carry."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Closet"))
                    {
                        Debug.Log("Opening Closet");

                        if (hasHint && hasBirthday)
                        {
                            // SPEECH BUBBLE
                            // Text: "Alright let's try this out... Aha! Scuba gear!"

                            hasScubaGear = true;
                            hits[i].collider.gameObject.SetActive(false);
                            GameObject.Find("OpenLockedCloset").GetComponent<SpriteRenderer>().enabled = true;
                        }
                        else
                        {
                            // SPEECH BUBBLE
                            // Text: "The storage closet is locked. The last guy here must have set a new code."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Calendar"))
                    {
                        Debug.Log("Reading Calendar");

                        // SPEECH BUBBLE
                        // Text: ""Jamie's Birthday: 03-18-08." Huh. Happy belated birthday Jamie."

                        hasBirthday = true;
                        hits[i].collider.gameObject.tag = "Untagged";
                    }
                    else if (hits[i].collider.name.Contains("Computer"))
                    {
                        Debug.Log("Reading Computer");

                        // SPEECH BUBBLE
                        // Text: "This looks like the work log for the last scientist:
                        //       "Day 1: Cataloged plenty of sea turtles today. Good to see they're still migrating on time.
                        //       "Day 2: I've noticed some of the turtles are getting attracted to the lights of my boat. I'll stop going out at night now.
                        //       "Day 3: Accidentaly broke the lock on the closet downstairs. Make sure to tell the next scientist the passcode is my daughter's birthday.

                        hasHint = true;
                        hits[i].collider.gameObject.tag = "Untagged";
                    }
                    else if (hits[i].collider.name.Contains("Boat"))
                    {
                        if (hasToolbox && numDriftwood == 5 && !hullFixed)
                        {
                            // SPEECH BUBBLE
                            // Text: Alright driftwood plus tools equals a fixed hole. Awesome!

                            hullFixed = true;
                        }

                        if (hasToolbox && hasPropeller && !propFixed)
                        {
                            // SPEECH BUBBLE
                            // Text: Fan propeller. Boat propeller. Same thing. Fixed!

                            propFixed = true;
                        }

                        if (hullFixed && propFixed && hasScubaGear)
                        {
                            // SPEECH BUBBLE
                            // Text: Sweet it looks like that's everything. Let's get out on the sea and find that little guy."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Ship"))
                    {
                        // SPEECH BUBBLE
                        // Text: "Accoring to my radar, the turtle is in the rocky region of the sea. I should probably stick with a smaller ship today."
                    }
                }
            }
        }
    }
}