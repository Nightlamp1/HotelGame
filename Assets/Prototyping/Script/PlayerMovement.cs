using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4.0f; //Max speed player can move

    private Rigidbody2D playerBody; //Used to store reference to players rigidbody

	// Use this for initialization
	void Start () {
        //Get and store reference to player ridgidbody
        playerBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Get horizontal movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        //Get vertical movement
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Create movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Move the player by applying velocity
        playerBody.velocity = movement * speed;
	}
}
