using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int levelScore;

	// Use this for initialization
	void Start () {
        levelScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnGUI()
    {
        GUIStyle newStyle = new GUIStyle();
        newStyle.normal.textColor = Color.black;
        newStyle.fontSize = 20;
        GUI.Label(new Rect(1000, 40, 200, 100), "Score: $" + levelScore, newStyle);
    }
}
