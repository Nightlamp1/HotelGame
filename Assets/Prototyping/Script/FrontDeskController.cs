using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDeskController : MonoBehaviour {

    public Transform startPoint, endPoint;
    Vector2 startPosition, endPosition;

    public Transform customerStartPoint, customerEndPoint;
    Vector2 customerStartPos, customerEndPos;

    public Transform customerSpawn;
    public GameObject customer;
    public GameObject[] rooms;

    public bool isCustomerWaiting = false; //Default is no customers

	// Use this for initialization
	void Start () {
		startPosition = new Vector2 (startPoint.position.x, startPoint.position.y);
        endPosition = new Vector2 (endPoint.position.x, endPoint.position.y);

        customerStartPos = new Vector2(customerStartPoint.position.x, customerStartPoint.position.y);
        customerEndPos = new Vector2(customerEndPoint.position.x, customerEndPoint.position.y);

        rooms = GameObject.FindGameObjectsWithTag("Room"); //Grab all Room objects in the scene
    }
	
	// Update is called once per frame
	void Update () {
        Collider2D playerCheck = Physics2D.OverlapArea(startPosition, endPosition);
        Collider2D customerCheck = Physics2D.OverlapArea(customerStartPos, customerEndPos);

        if(customerCheck != null)
        {
            if(customerCheck.tag == "Customer")
            {
                isCustomerWaiting = true;
            }
        }

        if(playerCheck != null)
        {
            if (playerCheck.tag == "Player" && isCustomerWaiting)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    CheckInCustomer(customerCheck);
                }
            }
        }
       
        
	}

    void CheckInCustomer(Collider2D customerCheck)
    {
        for(int currentRoom = 0; currentRoom < rooms.Length; currentRoom++)
        {
            RoomScript roomScript = rooms[currentRoom].GetComponent<RoomScript>();
            if (!roomScript.isRoomOccupied && !roomScript.isRoomDirty)
            {
                //Debug.Log("room is free user can checkin");
                Destroy(customerCheck.gameObject);
                roomScript.isRoomOccupied = true;
                isCustomerWaiting = false;
                break;
            }
        }
    }
}
