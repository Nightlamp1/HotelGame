﻿using System.Collections;
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

    ScoreManager scoreManager;

    public bool isCustomerWaiting = false; //Default is no customers
    public GameObject bag;
    public Transform bagSpawn;
    

	// Use this for initialization
	void Start () {
		startPosition = new Vector2 (startPoint.position.x, startPoint.position.y);
        endPosition = new Vector2 (endPoint.position.x, endPoint.position.y);

        customerStartPos = new Vector2(customerStartPoint.position.x, customerStartPoint.position.y);
        customerEndPos = new Vector2(customerEndPoint.position.x, customerEndPoint.position.y);

        rooms = GameObject.FindGameObjectsWithTag("Room"); //Grab all Room objects in the scene
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
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
        BagScript newBag = bag.GetComponent<BagScript>();
        newBag.customerId = customerCheck.gameObject.GetInstanceID();
        Instantiate(bag, bagSpawn.position, Quaternion.identity);
        for (int currentRoom = 0; currentRoom < rooms.Length; currentRoom++)
        {
            RoomScript roomScript = rooms[currentRoom].GetComponent<RoomScript>();
            if (!roomScript.isRoomOccupied && !roomScript.isRoomDirty)
            {
                roomScript.isRoomOccupied = true;
                roomScript.customerId = customerCheck.gameObject.GetInstanceID();
                isCustomerWaiting = false;
                bagSpawn.Translate(Vector3.right * 1);
                Destroy(customerCheck.gameObject);
                scoreManager.levelScore += 10;
                break;
            }
        }
    }
}
