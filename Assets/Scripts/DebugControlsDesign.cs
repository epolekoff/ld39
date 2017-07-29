using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControlsDesign : MonoBehaviour {

    public Player playerObject;

    public Vector3 resetPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (playerObject != null)
                playerObject.transform.position = resetPos;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if (playerObject != null)
                resetPos = playerObject.transform.position;
        }
	}
}
