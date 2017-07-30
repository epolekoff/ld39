using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public MoveType EnemyMoveType;
    public float moveSpeed;
    public float moveRangeIncrease = 1;

    private bool dead = false;

    public float energyDropPercent = 100;
    private float energyDropAmount = 0.3f;

    private Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {


	}

    void FixedUpdate ()
    {
        if(!dead)
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

        var rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = false;

        dead = true;

        Destroy(this.gameObject, 3f);
    }

    void DropEnergy()
    {
        GameController.Instance.AddCharge(energyDropAmount);
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

                float newXPos = (Mathf.Sin(Time.time * moveSpeed) * moveRangeIncrease) + startingPos.x;
                //newYPos *= moveRangeIncrease;

                transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);

                break;

            case (MoveType.WALKING):

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (1 << other.gameObject.layer == 11) // Attack Layer
        {
            Death();
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
