using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomScript : MonoBehaviour {

    public bool isRoomDirty = false; //room starts as clean
    public bool isRoomOccupied = false; //room starts as unoccupied
    public int customerId;

    public bool interact = false; //room default interaction state is false
    public Transform lineStart, lineEnd; //Used for player in range raycast
    public Sprite occupiedRoom, dirtyRoom, cleanRoom; //Sprites for dirty room or clean room

    private float timeToClean; //Counter to keep track of how long room clean has gone
    public Slider cleanProgressBar; //Progress bar UI element to monitor room clean progress

    ScoreManager scoreManager; //Instance of ScoreManager to allow for editing of player score in game


    // Use this for initialization
    void Start () {
        timeToClean = 0.0f;
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
	
    void Raycasting()
    {
        Debug.DrawLine(lineStart.position, lineEnd.position, Color.blue); //Gizmo to help troubleshooting
        RaycastHit2D testRoomHit = Physics2D.Linecast(lineStart.position, lineEnd.position); //Check to see if any object is in front of the room using raycast
        if (testRoomHit.collider != null)
        {
            if (testRoomHit.collider.tag == "Player")
            {
                interact = true; //If player is in range interact will be set to true for this room
            }
        }
        else
        {
            interact = false; //If player moves out interact will return to false
        }
    }


	// Update is called once per frame
	void Update () {
        Raycasting(); //Call Raycasting() to verify if any objects/players in range of the room and able to interact
        CheckForInteraction(); //Call CheckForInteraction() to manage player interactions with this room
        CheckRoomState(); //Call CheckRoomState() to verify room state and update variables/spirtes as needed
    }

    void CheckRoomState()
    {
        if (isRoomDirty)
        {
            isRoomOccupied = false;
            customerId = 0;
            SpriteRenderer roomSprite = GetComponent<SpriteRenderer>();
            roomSprite.sprite = dirtyRoom;
            cleanProgressBar.gameObject.SetActive(true);
        }
        else if (isRoomOccupied)
        {
            SpriteRenderer roomSprite = GetComponent<SpriteRenderer>();
            roomSprite.sprite = occupiedRoom;
        }
        else
        {
            cleanProgressBar.gameObject.SetActive(false);
        }
    }

    void CheckForInteraction()
    {
        if (interact && Input.GetButton("Submit") && isRoomDirty)
        {
            //If interact button is pressed by player room cleaning action will be started and continue as long as button is pressed
            timeToClean += Time.deltaTime;
            cleanProgressBar.value = timeToClean;

            if (timeToClean > 3) //Once counter reaches 3 room clean will be completed
            {
                isRoomDirty = false;
                SpriteRenderer roomSprite = GetComponent<SpriteRenderer>();
                roomSprite.sprite = cleanRoom;
                timeToClean = 0.0f;
                cleanProgressBar.value = timeToClean;
                scoreManager.levelScore += 10;
            }
        }
        else if(interact && Input.GetButtonDown("Submit") && isRoomOccupied)
        {
            Debug.Log("checking for bag");
            Debug.DrawLine(lineStart.position, lineEnd.position, Color.blue); //Gizmo to help troubleshooting
            RaycastHit2D playerCheck = Physics2D.Linecast(lineStart.position, lineEnd.position); //Check to see if any object is in front of the room using raycast
            
            if(playerCheck.transform.childCount != 0)
            {
                GameObject bag = playerCheck.transform.GetChild(0).gameObject;
                BagScript thisBag = bag.GetComponent<BagScript>();

                if(thisBag.customerId == customerId)
                {
                    Destroy(bag);
                }
                else
                {
                    Debug.Log("this is the wrong bag");
                }
            }
            
            

        }
    }
}
