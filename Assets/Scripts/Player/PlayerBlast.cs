using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerBlast : MonoBehaviour
{

    /// <summary>
    /// Arm
    /// </summary>
    public GameObject ArmObject;
    public GameObject ArmSprite;
    public GameObject Reticle;
    public Image ReticleCover;
    public Gradient ReticleFillGradient;

    public GameObject ForceConePrefab;

    public bool MoveAllowed { get; set; }

    private const float MinTimescale = 0.05f;
    private const float TimescaleDecayRate = 1f;
    private const float MaxSlowdownTime = 4f;

    private const float CameraLerpTime = 2f;
    private const float CameraResetTime = 0.5f;
    private float DefaultCameraSize;
    private Vector3 DefaultCameraPosition;
    private const float ZoomedCameraSize = 3.26f;
    private readonly Vector3 ZoomedCameraPosition = new Vector3(0, 0, -10);

    private float m_charge = 0f;
    private bool m_triggerDown = false;
    private bool m_charging = false;

    float m_aimX = 0, m_aimY = 0;

    // Use this for initialization
    void Start()
    {
        DefaultCameraSize = GetComponentInChildren<Camera>().orthographicSize;
        DefaultCameraPosition = GetComponentInChildren<Camera>().transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();

        if (MoveAllowed)
        {
            HandleShooting();
        }

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
        if (m_aimX == 0 && m_aimY == 0)
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
        if (Input.GetAxis("Fire") > 0 || Input.GetButton("FireButton"))
        {
            // If you just started charging, trigger some effects
            if (!m_charging)
            {
                GetComponent<Player>().CharacterSprite.GetComponent<Animator>().SetBool("Charging", true);
            }

            m_charging = true;
            m_triggerDown = Input.GetAxis("Fire") > 0;
            if (m_charge == 0)
            {
                m_charge = Designer.Instance.MinCharge;
                StartCoroutine(BlastSlowEffectCoroutine());
            }

            m_charge += Designer.Instance.ChargeRate * Time.deltaTime;

            // Cannot exceed the max value.
            if (m_charge > Designer.Instance.MaxCharge)
            {
                m_charge = Designer.Instance.MaxCharge;
            }

            // Cannot exceed the amount of charge we have left.
            if (m_charge * Designer.Instance.ChargePowerUsageRatio > GameController.Instance.CurrentPowerLevel)
            {
                m_charge = Mathf.Max(GameController.Instance.CurrentPowerLevel / Designer.Instance.ChargePowerUsageRatio, Designer.Instance.MinCharge);
            }

            // Disable movement while charging
            GetComponent<PlayerMovement>().MovementOverride = true;
        }

        // Release to fire.
        if ((Input.GetAxis("Fire") == 0 && m_triggerDown) ||
            Input.GetButtonUp("FireButton"))
        {
            if (Input.GetAxis("Fire") == 0 && m_triggerDown)
                m_triggerDown = false;

            // Add the force
            Vector2 armDirection = new Vector2(m_aimX, -m_aimY);
            Vector2 force = -armDirection * m_charge * Designer.Instance.ForceChargeMultiplier;
            GetComponent<Rigidbody2D>().AddForce(force);

            // Remove the used charge from our meter.
            GameController.Instance.UseCharge(m_charge * Designer.Instance.ChargePowerUsageRatio);

            // Animations
            GetComponent<Player>().CharacterSprite.GetComponent<Animator>().SetBool("Charging", false);
            FireForceCone();

            // Allow movement again
            GetComponent<PlayerMovement>().MovementOverride = false;

            // Reset the charge
            m_charge = 0;
            m_charging = false;
        }
    }

    /// <summary>
    /// Update the display of the reticle.
    /// </summary>
    private void UpdateReticle()
    {
        // Make The reticle visible if there is a charge.
        Reticle.SetActive(Input.GetAxis("AimHorizontal") != 0 || Input.GetAxis("AimVertical") != 0);
        ArmSprite.SetActive(m_charge > 0);

        // Scale the reticle color overlay so it looks like a charge meter thing.
        float ratio = (m_charge - Designer.Instance.MinCharge) / (Designer.Instance.MaxCharge - Designer.Instance.MinCharge);
        ReticleCover.fillAmount = ratio;
        ReticleCover.color = ReticleFillGradient.Evaluate(ratio);
    }

    /// <summary>
    /// Slow down timer when charging.
    /// </summary>
    private IEnumerator BlastSlowEffectCoroutine()
    {
        float timer = 0;
        Camera camera = GetComponentInChildren<Camera>();
        Vector3 startPosition = camera.transform.localPosition;
        float startSize = camera.orthographicSize;
        while (m_charging && timer < MaxSlowdownTime)
        {
            timer += Time.unscaledDeltaTime;
            if (Time.timeScale > MinTimescale)
            {
                Time.timeScale -= TimescaleDecayRate * Time.fixedDeltaTime;
            }

            Time.fixedDeltaTime = 0.02F * Time.timeScale;

            // Zoom the camera in.
            camera.transform.localPosition = Vector3.Lerp(startPosition, ZoomedCameraPosition, timer / CameraLerpTime);
            camera.orthographicSize = Mathf.Lerp(startSize, ZoomedCameraSize, timer / CameraLerpTime);

            yield return null;
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        StartCoroutine(ResetCameraPositionCoroutine());
    }

    /// <summary>
    /// Move the camera back to its position.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetCameraPositionCoroutine()
    {
        float timer = 0;
        Camera camera = GetComponentInChildren<Camera>();
        Vector3 startPosition = camera.transform.localPosition;
        float startSize = camera.orthographicSize;
        while (timer < CameraResetTime)
        {
            timer += Time.deltaTime;

            camera.transform.localPosition = Vector3.Lerp(startPosition, DefaultCameraPosition, timer / CameraLerpTime);
            camera.orthographicSize = Mathf.Lerp(startSize, DefaultCameraSize, timer / CameraLerpTime);

            yield return null;
        }
    }

    private void FireForceCone()
    {
        var forceConeObject = GameObject.Instantiate(ForceConePrefab) as GameObject;
        var forceCone = forceConeObject.GetComponent<ForceCone>();

        forceCone.transform.SetParent(this.transform);
        forceCone.transform.localPosition = ArmObject.transform.localPosition;
        forceCone.transform.localRotation = Quaternion.LookRotation(new Vector3(m_aimX, -m_aimY, 0), transform.right);
        forceCone.EmitParticles();

        // Deparent the attack hitbox.
        forceCone.Hitbox.transform.SetParent(null);

        // Destroy the cone when it's done.
        GameObject.Destroy(forceConeObject, 3f);
        GameObject.Destroy(forceCone.Hitbox, 0.5f);
    }
}
