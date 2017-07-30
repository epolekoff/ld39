using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIcon : MonoBehaviour {

    public Sprite keyboardImage;
    public Sprite xboxImage;

    private SpriteRenderer icon;

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
        //If a keyboard or mouse was used
        if(Input.anyKeyDown)
        {
            icon.sprite = keyboardImage;
        }

        //If a controller was used.
        //Iterate through the axis

        if (Input.GetAxis("Horizontal") != 0)
        {

        }

        for (int i = 0; i <= 9; i++)
        {

        }
    }
    
}
