using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {

    public Button PlayButton;

	// Use this for initialization
	void Start () {
        EventSystem.current.SetSelectedGameObject(PlayButton.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
