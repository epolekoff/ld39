using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public MoveType EnemyMoveType;
    public float moveSpeed;
    public float moveRangeIncrease = 1;

    public float energyDropPercent = 100;

    private Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        HandleMovement();

	}

    public void Death()
    {
        //Drop Energy Chance
        float randomValue = Random.value;
        if(randomValue*100 <= energyDropPercent)
        {
            DropEnergy();
        }
    }

    void DropEnergy()
    {

    }

    void HandleMovement ()
    {
        switch(EnemyMoveType)
        {
            case (MoveType.STATIONARY):

                break;

            case (MoveType.VERTICAL):

                float newYPos = (Mathf.Sin(Time.time * moveSpeed)* moveRangeIncrease) + startingPos.y;
                //newYPos *= moveRangeIncrease;

                transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);

                break;

            case (MoveType.HORIZONTAL):

                transform.position = new Vector3(Mathf.Sin(Time.time) * moveSpeed + startingPos.x, transform.position.y, transform.position.z);

                break;

            case (MoveType.WALKING):

                break;
        }
    }
}

public enum MoveType
{
    STATIONARY,
    VERTICAL,
    HORIZONTAL,
    WALKING,

}
