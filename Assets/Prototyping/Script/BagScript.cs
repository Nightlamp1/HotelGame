using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {

    public int customerId = 0;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        CheckForPlayerInRange();
	}

    void CheckForPlayerInRange()
    {
        Collider2D playerCheck = Physics2D.OverlapCircle(this.transform.position, 0.75f);

        if(playerCheck != null && playerCheck.tag == "Player")
        {
            if (Input.GetButtonDown("Submit") && transform.parent == null)
            {
                transform.SetParent(playerCheck.transform);
            }
            else if (Input.GetButtonDown("Submit"))
            {
                transform.SetParent(null);
            }
        }
    }
}
