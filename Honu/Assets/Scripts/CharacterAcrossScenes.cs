using UnityEngine;
using System.Collections;

public class CharacterAcrossScenes : MonoBehaviour
{
    private static GameObject playerInstance;
    private static bool firstHouseLoad = true;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (playerInstance == null)
        {
            playerInstance = this.gameObject;
        }
        else {
            DestroyObject(this.gameObject);
        }
    }

    void Start ()
    {
	    
	}
	
	void Update ()
    {
	    
	}

    void OnLevelWasLoaded (int levelNum)
    {
        if (levelNum == 1)
        {
            this.gameObject.transform.position = new Vector3(-0.006f, -1.643f, -1.643f);

            if (firstHouseLoad)
            {
                this.gameObject.transform.position = new Vector3(-1.93f, 1.61f, -1.643f);
                firstHouseLoad = false;
            }

            if (this.GetComponent<CharacterInteractionAndInventory>().hasToolbox)
            {
                Destroy(GameObject.Find("Toolbox"));
            }

            if (this.GetComponent<CharacterInteractionAndInventory>().hasScubaGear)
            {
                Destroy(GameObject.Find("ClosedLockedCloset"));
                GameObject.Find("OpenLockedCloset").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (levelNum == 2)
        {
            this.gameObject.transform.position = new Vector3(1.681146f, -0.8012725f, -0.8012725f);
        }
        else if (levelNum == 3)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
