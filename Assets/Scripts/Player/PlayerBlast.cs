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
    public GameObject ReticleCover;

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
            if(m_charge == 0)
            {
                m_charge = Designer.Instance.MinCharge;
            }

            m_charge += Designer.Instance.ChargeRate * Time.deltaTime;

            // Cannot exceed the max value.
            if(m_charge > Designer.Instance.MaxCharge)
            {
                m_charge = Designer.Instance.MaxCharge;
            }

            // Cannot exceed the amount of charge we have left.
            if (m_charge * Designer.Instance.ChargePowerUsageRatio > GameController.Instance.CurrentPowerLevel)
            {
                m_charge = GameController.Instance.CurrentPowerLevel / Designer.Instance.ChargePowerUsageRatio;
            }
        }

        // Release to fire.
        if ((Input.GetAxis("Fire") == 0 && m_triggerDown) || 
            Input.GetButtonUp("FireButton"))
        {
            m_triggerDown = false;

            // Add the force
            Vector2 armDirection = new Vector2(m_aimX, -m_aimY);
            Vector2 force = -armDirection * m_charge * Designer.Instance.ForceChargeMultiplier;
            GetComponent<Rigidbody2D>().AddForce(force);

            // Remove the used charge from our meter.
            GameController.Instance.UseCharge(m_charge * Designer.Instance.ChargePowerUsageRatio);

            // Reset the charge
            m_charge = 0;
        }
    }

    /// <summary>
    /// Update the display of the reticle.
    /// </summary>
    private void UpdateReticle()
    {
        // Make The reticle visible if there is a charge.
        Reticle.SetActive(m_charge > 0);

        // Scale the reticle color overlay so it looks like a charge meter thing.
        Vector3 originalScale = ReticleCover.transform.localScale;
        ReticleCover.transform.localScale = new Vector3((m_charge / Designer.Instance.MaxCharge)/2, originalScale.y, originalScale.z);

    }
}
