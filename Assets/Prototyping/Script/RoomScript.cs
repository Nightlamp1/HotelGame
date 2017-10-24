using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {

    public bool isRoomDirty = false; //room starts as clean
    public bool isRoomOccupied = false; //room starts as unoccupied

    public bool interact = false; //room default interaction state is false
    public Transform lineStart, lineEnd; //Used for player in range raycast
    public Sprite occupiedRoom, dirtyRoom, cleanRoom; //Sprites for dirty room or clean room


	// Use this for initialization
	void Start () {
		
	}
	
    void Raycasting()
    {
        Debug.DrawLine(lineStart.position, lineEnd.position, Color.blue);
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

        if (interact)
        {
            if (Input.GetButtonDown("Submit") && isRoomDirty)
            {
                //If interact button is pressed by player room will be cleaned
                isRoomDirty = false;
                SpriteRenderer roomSprite = GetComponent<SpriteRenderer>();
                roomSprite.sprite = cleanRoom;
            }       
        }

        if (isRoomDirty)
        {
            //Update room sprite when room becomes dirty
            isRoomOccupied = false; //may need to revisit for multi night stays??
            SpriteRenderer roomSprite = GetComponent<SpriteRenderer>();
            roomSprite.sprite = dirtyRoom;    
        }
        else if (isRoomOccupied)
        {
            //Update room sprite to occupied sprite
            SpriteRenderer roomSprite = GetComponent<SpriteRenderer>();
            roomSprite.sprite = occupiedRoom;
        }
    }
}
