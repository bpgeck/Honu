using UnityEngine;
using UnityEngine.UI;
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
	private bool pressedE;
	private float timetoHideSpeech = 3.5f; //seconds

    bool hullFixed;
    bool propFixed;

    public int directionFacing = 3;
    float checkDistance = 0.25f;

	private GameObject speechBubble;
	private Text speechText;
	private RectTransform comprt;
	private RectTransform textrt;

	private Vector2 comprtSD;
	private Vector3 comprtLS;
	private Vector3 comprtT;

	private Vector2 textrtSD;
	private Vector2 textrtT;

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

		pressedE = false;

		speechBubble = GameObject.Find ("Speech");
		speechText = GameObject.Find ("Text").GetComponent<Text>();
		speechBubble.SetActive (false);

		comprt = speechBubble.GetComponent<RectTransform>();
		textrt = speechText.GetComponent<RectTransform>();

		comprtSD = new Vector2 (comprt.sizeDelta.x, comprt.sizeDelta.y);
		comprtLS = new Vector3 (comprt.localScale.x, comprt.localScale.y, comprt.localScale.z);
		comprtT = new Vector3 (comprt.localPosition.x, comprt.localPosition.y, comprt.localPosition.z);

		textrtSD = new Vector2 (textrt.sizeDelta.x, textrt.sizeDelta.y);
		textrtT = new Vector3 (textrt.localPosition.x, textrt.localPosition.y, textrt.localPosition.z);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

			if (pressedE == true) {
				return;
			}

			pressedE = true;
			StartCoroutine (resetE(2.0f));
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

			comprt.sizeDelta = comprtSD;
			comprt.localScale = comprtLS;
			comprt.localPosition = comprtT;
			
			textrt.sizeDelta = textrtSD;
			textrt.localPosition = textrtT;
			
			for (int i = 0; i < hits.Length; i++)
            {
               

                if (hits[i].collider.tag == "Interactable")
                {
					Debug.Log(hits[i].collider.name);
                    if (hits[i].collider.name.Contains("Toolbox"))
                    {
                        Debug.Log("Grabbing Toolbox");
					
						speechBubble.SetActive(true);
						speechText.text = "Hmm, maybe this toolbox will come in handy.";
						StartCoroutine (showthenHide(timetoHideSpeech));

                        hasToolbox = true;
                        hits[i].collider.gameObject.SetActive(false);
                    }
                    else if (hits[i].collider.name.Contains("Trash"))
                    {
                        Debug.Log("Looking in Trashcan");

                        if (hasToolbox)
                        {

							speechBubble.SetActive(true);
							speechText.text = "One man's trash is another man's propeller.";
							StartCoroutine (showthenHide(3.0f));
                            // Text: "One man's trash is another man's propeller"

                            hasPropeller = true;
                            //hits[i].collider.gameObject.tag = "Untagged";
                        }
                        else
                        {
							comprt.sizeDelta = new Vector2(135,135);
							comprt.localPosition = new Vector3(175.0f, 100.0f, -5.0f);
							
							textrt.sizeDelta = new Vector2(230, 125);
							textrt.localPosition = new Vector3(1.7f, textrt.localPosition.y, textrt.localPosition.z);

							speechBubble.SetActive(true);
							speechText.text = "Hey, there's a broken fan in here. Nothing looks broken. I guess the motor must have gone out.";
							//StartCoroutine (showthenHide(5.0f));
							// Text: "Hey there's a broken fan in here. Nothing looks broken. I guess the motor must have gone out."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Driftwood"))
                    {
						speechBubble.SetActive(true);
						speechText.text = "Driftwood. That might come in handy.";
						StartCoroutine (showthenHide(3.0f));
                        // Text: "Driftwood. That might come in handy."

                        Debug.Log("Grabbing Driftwood");

                        numDriftwood++;
                        hits[i].collider.gameObject.SetActive(false);

                        if (numDriftwood == 5)
                        {
							speechBubble.SetActive(true);
							speechText.text = "Alright, that's all the driftwood I can carry.";
							StartCoroutine (showthenHide(3.0f));
                            // Text: "Alright that's all the driftwood I can carry."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Closet"))
                    {
                        Debug.Log("Opening Closet");

                        if (hasHint && hasBirthday)
                        {
							speechBubble.SetActive(true);
							speechText.text = "Alright, let's try this out...Aha! Scuba gear!";
							//StartCoroutine (showthenHide(timetoHideSpeech));
                            // Text: "Alright let's try this out... Aha! Scuba gear!"

                            hasScubaGear = true;
                            hits[i].collider.gameObject.SetActive(false);
							GameObject.Find("OpenLockedCloset").GetComponent<SpriteRenderer>().enabled = true;
					
                        }
                        else
                        {
							comprt.sizeDelta = new Vector2(130,100);
							comprt.localPosition = new Vector3(165.0f, 71.0f, -5.0f);
							
							textrt.sizeDelta = new Vector2(220, 92);
							textrt.localPosition = new Vector3(5.0f, textrt.localPosition.y, textrt.localPosition.z);

							speechBubble.SetActive(true);
							speechText.text = "The storage closet is locked. The last guy here must have set a new code.";
							//StartCoroutine (showthenHide(timetoHideSpeech));
                            // Text: "The storage closet is locked. The last guy here must have set a new code."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Calendar"))
                    {
                        Debug.Log("Reading Calendar");

						comprt.sizeDelta = new Vector2(125, 100);
						comprt.localPosition = new Vector3(169.0f, 71.0f, -5.0f);

						textrt.sizeDelta = new Vector2(185, 92);
			
						speechBubble.SetActive(true);
						speechText.text = "\"Jamie's Birthday: 03-18-08.\" Huh. Happy belated birthday Jamie.";
						//StartCoroutine (showthenHide(timetoHideSpeech));
                        // Text: ""Jamie's Birthday: 03-18-08." Huh. Happy belated birthday Jamie."

                        hasBirthday = true;
                      //  hits[i].collider.gameObject.tag = "Untagged";
                    }
                    else if (hits[i].collider.name.Contains("Computer"))
                    {
                        Debug.Log("Reading Computer");

						comprt.sizeDelta = new Vector2(290,265);
						comprt.localScale = new Vector3(2,2,comprt.localScale.z);
						comprt.localPosition = new Vector3(252.0f, 210.0f, -5.0f);

						textrt.sizeDelta = new Vector2(465, 350);
						textrt.localPosition = new Vector3(0.0f, -20.0f, textrt.localPosition.z);
						//speechBubble.GetComponent<RectTransform>()
						speechBubble.SetActive(true);
						speechText.text = "This looks like the work log for the last scientist:NEWLINE" +
							"Day 1: Catalogued plenty of sea turtles today. Good to see they're still migrating on time.NEWLINE" +
							"Day 2: I've noticed some of the turtles are getting attracted to the lights of my boat. I'll stop going out at night now.NEWLINE" +
							"Day 3: Accidentally broke the lock on the closet downstairs. Make sure to tell the next scientist the passcode is my daughter's birthday.";
						speechText.text = speechText.text.Replace("NEWLINE", "\n");
						StartCoroutine (showthenHide(15.0f));
                        // Text: "This looks like the work log for the last scientist:
                        //       "Day 1: Cataloged plenty of sea turtles today. Good to see they're still migrating on time.
                        //       "Day 2: I've noticed some of the turtles are getting attracted to the lights of my boat. I'll stop going out at night now.
                        //       "Day 3: Accidentaly broke the lock on the closet downstairs. Make sure to tell the next scientist the passcode is my daughter's birthday.

                        hasHint = true;
                      //  hits[i].collider.gameObject.tag = "Untagged";
                    }
                    else if (hits[i].collider.name.Contains("Boat"))
                    {
                        if (hasToolbox && numDriftwood == 5 && !hullFixed)
                        {
							speechBubble.SetActive(true);
							speechText.text = "Alright, driftwood plus tools equals a fixed hole. Awesome!";
							StartCoroutine (showthenHide(timetoHideSpeech));
                            // Text: Alright driftwood plus tools equals a fixed hole. Awesome!

                            hullFixed = true;
                        }

                        if (hasToolbox && hasPropeller && !propFixed)
                        {
							speechBubble.SetActive(true);
							speechText.text = "Fan propeller. Boat propeller. Same thing. Fixed!";
							StartCoroutine (showthenHide(timetoHideSpeech));
                            // Text: Fan propeller. Boat propeller. Same thing. Fixed!

                            propFixed = true;
                        }

                        if (hullFixed && propFixed && hasScubaGear)
                        {
							speechBubble.SetActive(true);
							speechText.text = "Sweet, it looks like that's everything. Let's get out on the sea and find that little guy.";
							StartCoroutine (showthenHide(timetoHideSpeech));
							// Text: Sweet it looks like that's everything. Let's get out on the sea and find that little guy."
                        }
                    }
                    else if (hits[i].collider.name.Contains("Ship"))
                    {
						speechBubble.SetActive(true);
						speechText.text = "According to my radar, the turtle is in the rocky region of the sea. I should probably stick with a smaller ship today.";
						StartCoroutine (showthenHide(5.0f));
						// Text: "Accoring to my radar, the turtle is in the rocky region of the sea. I should probably stick with a smaller ship today."
                    }
                }
				else {
					if (hits[i].collider.name.Contains("Desk")) return;
					if (hits[i].collider.name.Contains ("OpenLockedCloset")) return;
					if (hits[i].collider.name.Contains("Top")) return;
					if (hits[i].collider.name.Contains("Left")) return;
					if (hits[i].collider.name.Contains("Bottom")) return;
					if (hits[i].collider.name.Contains("Right")) return;
					speechBubble.SetActive(false);
				}
            }
        }
    }
	IEnumerator showthenHide(float waitTime){
		yield return new WaitForSeconds (waitTime);
		speechBubble.SetActive (false);
	}
	IEnumerator resetE(float waitTime){
		yield return new WaitForSeconds(waitTime);
		pressedE = false;
	}
}