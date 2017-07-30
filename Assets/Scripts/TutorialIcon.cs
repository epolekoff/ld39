using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIcon : MonoBehaviour {

    public Sprite keyboardImage;
    public Sprite xboxImage;

    private SpriteRenderer icon;
    private bool axisDownThisFrame;

    private List<string> controllerButtonNames;

	// Use this for initialization
	void Start () {
        icon = GetComponent<SpriteRenderer>();
        if(icon == null)
        {
            Debug.LogWarning(name + " can't find it's spriterenderer component.");
        } else
        {
            icon.sprite = xboxImage;
        }

	}
	
	// Update is called once per frame
	void Update () {

        CheckLastUsedSystem();
        
	}
    
    void CheckLastUsedSystem()
    {

        //If a controller was used.
        //Iterate through the axis
        if (Input.GetAxis("Horizontal") != 0)
        {
            if(!axisDownThisFrame)
            {
                axisDownThisFrame = true;
                icon.sprite = xboxImage;
            }
            
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            if (!axisDownThisFrame)
            {
                axisDownThisFrame = true;
                icon.sprite = xboxImage;
            }
        }
        else if (Input.GetAxis("AimHorizontal") != 0)
        {
            if (!axisDownThisFrame)
            {
                axisDownThisFrame = true;
                icon.sprite = xboxImage;
            }
        }
        else if (Input.GetAxis("AimVertical") != 0)
        {
            if (!axisDownThisFrame)
            {
                axisDownThisFrame = true;
                icon.sprite = xboxImage;
            }
        }
        else
        {
            axisDownThisFrame = false;
        }


        //If a keyboard or mouse was used
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            icon.sprite = keyboardImage;
        }





    }
    
}
