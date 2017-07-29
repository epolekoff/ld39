using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerBlast : MonoBehaviour {

    /// <summary>
    /// Arm
    /// </summary>
    public GameObject ArmObject;
    public GameObject Reticle;

    private const float ChargeRate = 1f;
    private const float MaxCharge = 3f;
    private const float ForceChargeMultiplier = 100f;

    private float m_charge = 0f;
    private bool m_triggerDown = false;

    float m_aimX = 0, m_aimY = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleAiming();

        HandleShooting();

        UpdateReticle();
	}

    /// <summary>
    /// Aim the direction of the blast.
    /// </summary>
    private void HandleAiming()
    {
        // Get the angle
        m_aimX = Input.GetAxis("AimHorizontal");
        m_aimY = Input.GetAxis("AimVertical");

        // Solve the Look Rotation Is 0 case.
        if(m_aimX == 0 && m_aimY == 0)
        {
            m_aimX = 0.1f;
        }

        // Rotate by the angle
        ArmObject.transform.localRotation = Quaternion.LookRotation(new Vector3(m_aimX, -m_aimY, 0), transform.right);
    }

    /// <summary>
    /// Shoot a blast to go forward.
    /// </summary>
    private void HandleShooting()
    {
        // Hold down the button to charge.
        if(Input.GetAxis("Fire") > 0 || Input.GetButton("FireButton"))
        {
            m_triggerDown = true;
            m_charge += ChargeRate * Time.deltaTime;
            if(m_charge > MaxCharge)
            {
                m_charge = MaxCharge;
            }
        }

        // Release to fire.
        if ((Input.GetAxis("Fire") == 0 && m_triggerDown) || 
            Input.GetButtonUp("FireButton"))
        {
            m_triggerDown = false;

            // Add the force
            Vector2 armDirection = new Vector2(m_aimX, -m_aimY);
            Vector2 force = -armDirection * m_charge * ForceChargeMultiplier;
            GetComponent<Rigidbody2D>().AddForce(force);

            // Reset the charge
            m_charge = 0;
        }
    }

    /// <summary>
    /// Update the display of the reticle.
    /// </summary>
    private void UpdateReticle()
    {
        Reticle.SetActive(m_charge > 0);
    }
}
