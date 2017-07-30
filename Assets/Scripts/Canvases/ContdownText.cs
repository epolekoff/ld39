using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContdownText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTextReady()
    {
        GetComponent<Text>().text = "Ready";
    }

    public void SetTextSet()
    {
        GetComponent<Text>().text = "Set";
    }

    public void SetTextGo()
    {
        GetComponent<Text>().text = "GO!";
    }
}
