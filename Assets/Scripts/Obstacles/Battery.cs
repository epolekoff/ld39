using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// When colliding with a player, destroy self and add charge.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            bool chargeAdded = GameController.Instance.AddCharge(Designer.Instance.BatteryRechargeAmount);

            // Only destroy the battery if it was consumed.
            if(chargeAdded)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
