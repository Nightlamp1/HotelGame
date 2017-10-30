using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public GameObject[] rooms;
    public GameObject desk;
    public float eventTimer = 5f;
    public bool allRoomsFull = false;

	// Use this for initialization
	void Start () {
        rooms = GameObject.FindGameObjectsWithTag("Room"); //Grab all Room objects in the scene
        desk = GameObject.FindGameObjectWithTag("Desk"); //Grab the Front Desk object in the scene
	}
	
	// Update is called once per frame
	void Update () {
		if (eventTimer > 0)
        {
            eventTimer -= Time.deltaTime;
        }
        else
        {
            int eventItem = Random.Range(0, 2);
            if(eventItem == 0 && !allRoomsFull)
            {
                SpawnCustomer(); //Will need logic to check for open rooms
            }
            else
            {
                MakeRoomDirty();
            }
            eventTimer = 7f;
        }

        CheckForOpenRooms();

	}

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Make rooms dirty"))
        {
            foreach(GameObject room in rooms)
            {
                RoomScript roomScript = room.GetComponent<RoomScript>();
                roomScript.isRoomDirty = true;
            }
        }

        if (GUI.Button(new Rect(10, 130, 100, 30), "Random dirty"))
        {
            MakeRoomDirty();
        }

        if (GUI.Button(new Rect(10, 160, 130, 30), "Spawn Customer"))
        {
            SpawnCustomer();
        }

    }

    void MakeRoomDirty()
    {
        //Add logic to check if room is/was occupied
        int roomId = Random.Range(0, rooms.Length);
        RoomScript roomScript = rooms[roomId].GetComponent<RoomScript>();
        if (roomScript.isRoomOccupied)
        {
            roomScript.isRoomDirty = true;
            roomScript.isRoomOccupied = false;
        }
        
    }

    void SpawnCustomer()
    {
        FrontDeskController deskController = desk.GetComponent<FrontDeskController>();
        Instantiate(deskController.customer, deskController.customerSpawn.position, Quaternion.identity);
    }

    void CheckForOpenRooms()
    {
        for (int room = 0; room < rooms.Length; room++)
        {
            RoomScript roomScript = rooms[room].GetComponent<RoomScript>();
            if (!roomScript.isRoomOccupied)
            {
                allRoomsFull = false;
                return;
            }
        }
        allRoomsFull = true;
        //Debug.Log("ALL ROOMS ARE FULL");
    }
}
