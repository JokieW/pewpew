using UnityEngine;
using UnityEditor;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (gameObject.name == "Play")
        {
            Application.LoadLevel("main");
        }
        else if (gameObject.name == "Leave")
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
