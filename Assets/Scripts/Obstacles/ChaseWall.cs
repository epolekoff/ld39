using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWall : MonoBehaviour {

    public float wallSpeed;

    private Vector2 translationVector;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(GameController.Instance.GameStarted)
        {
            translationVector = Vector2.right * wallSpeed;

            transform.Translate(translationVector * Time.deltaTime);
        }
    }
}
