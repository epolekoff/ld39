﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {

    public LayerMask GroundLayer;
    public LayerMask DeathLayer;
    public Transform GroundDetectionObject;

    private Player m_player;
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;

    private bool m_onGround = false;
    public bool OnGround { get { return m_onGround; } }
    private const float GroundCheckRadius = 0.2f;
    private float m_maxJumpVelocity;
    private float m_maxWallJumpVelocity;
    private float m_minJumpVelocity;
    private float m_gravity;
    private bool m_wallSliding = false;
    private float m_clingTimer = 0;
    private float m_wallJumpMovementLockoutTimer = 0f;
    private float m_racerHeight = 1f;
    private bool m_hasMovementSpeedBoost = false;
    private float m_movementMultiplier = 1f;

    private bool m_rewinding = false;
    private const float FrameRecordingDelay = 0.1f;
    private float m_recordingTimer = 0;

    public bool MovementOverride = false;
    public bool GravityOverride = false;
    public float Gravity { get { return m_gravity; } }

    

    // Use this for initialization
    void Start () {
        m_player = GetComponent<Player>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        UpdateValuesFromDesignerVariables();
    }
	
	// Update is called once per frame
	void Update () {

        // Calculate new values for jump parameters based on the values the designers want.
        UpdateValuesFromDesignerVariables();

        // Handle movement inputs
        HandleMovement();

        // Check if we're on the ground
        CheckGround();

        // Handle jumping and ability use.
        HandleJump();

        // Count down the timers.
        HandleTimers();

        if(Input.GetKeyDown(KeyCode.R))
        {
            KillPlayer();
        }
	}

    /// <summary>
    /// Execute a jump action using internal variables.
    /// This may be called externally from the RacerAbilities class.
    /// </summary>
    public void ExecuteJump()
    {
        m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_maxJumpVelocity);

        //Activate jumping in Animator
        m_animator.SetBool("isJumping", true);
    }

    /// <summary>
    /// Execute a jump action using internal variables.
    /// This may be called externally from the RacerAbilities class.
    /// </summary>
    public void ExecuteWallJump()
    {
        // Raycast in both directions to determine where the wall is.
        float castLength = GetComponent<BoxCollider2D>().size.x;
        float velocitySign = 0;
        if (Physics2D.Raycast(transform.position, Vector2.right, castLength, GroundLayer.value))
        {
            velocitySign = -1;
        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, castLength, GroundLayer.value))
        {
            velocitySign = 1;
        }
        m_rigidbody.velocity = new Vector2(Designer.Instance.WallJumpKickoffVelocity * velocitySign, m_maxJumpVelocity);
        m_wallJumpMovementLockoutTimer = Designer.Instance.WallJumpMovementLockoutTimeSeconds;
    }

    /// <summary>
    /// Enhances the racer's walk speed by the given value;
    /// </summary>
    /// <param name="multiplier">Boost value.</param>
    public void EnableMovementBoost(float multiplier) {
        m_hasMovementSpeedBoost = true;
        m_movementMultiplier = multiplier;
    }

    /// <summary>
    /// Turns off the racer's speed boost.
    /// </summary>
    public void DisableMovementBoost() {
        m_hasMovementSpeedBoost = false;
        m_movementMultiplier = 1f;
    }

    /// <summary>
    /// Update local variables with values from Designer Variables at runtime.
    /// </summary>
    private void UpdateValuesFromDesignerVariables()
    {
        m_gravity = -(2 * Designer.Instance.MaxJumpHeight) / Mathf.Pow(Designer.Instance.TimeToMaxHeight, 2);
        m_maxJumpVelocity = Mathf.Abs(m_gravity) * Designer.Instance.TimeToMaxHeight;
        m_maxWallJumpVelocity = Mathf.Abs(-(2 * Designer.Instance.MaxWallJumpHeight) / Mathf.Pow(Designer.Instance.TimeToMaxHeight, 2)) * Designer.Instance.TimeToMaxHeight;
        m_minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(m_gravity)) * Designer.Instance.MinJumpHeight;
    }

    /// <summary>
    ///  Take player input to move the racers.
    /// </summary>
    private void HandleMovement()
    {
        // If movement is overridden by another ability, then you can't move.
        if(MovementOverride || m_wallJumpMovementLockoutTimer > 0)
        {
            return;
        }

        Vector2 velocity = m_rigidbody.velocity;

        // Get input
        float horizontalMovement = Input.GetAxis("Horizontal");
        float moveSpeed = OnGround ? Designer.Instance.MovementSpeed : Designer.Instance.AirMovementSpeed;
        // Account for the speed boost powerup 
        if (m_hasMovementSpeedBoost) {
            moveSpeed *= m_movementMultiplier;
        }

        if ((velocity.x < moveSpeed && horizontalMovement > 0) || 
            (velocity.x > -moveSpeed && horizontalMovement < 0))
        {
            // Move by input * speed.
            velocity += new Vector2(horizontalMovement * moveSpeed, 0);
            // Limit the velocity in range.
            velocity.x = Mathf.Clamp(velocity.x, -moveSpeed, moveSpeed);

            //Activate run animation
            m_animator.SetBool("isRunning", true);
        }

        //Flip sprite if running to the left
        if (horizontalMovement < 0)
        {
            m_player.CharacterSprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalMovement > 0)
        {
            m_player.CharacterSprite.GetComponent<SpriteRenderer>().flipX = false;
        }

        // Decay movement speed when on the ground and not pressing a direction.
        if(horizontalMovement <= 0.1f && horizontalMovement >= -0.1f)
        {
            float friction = m_onGround ? Designer.Instance.MovementSpeedGroundFriction : Designer.Instance.MovementSpeedAirFriction;
            if (velocity.x > friction || velocity.x < -friction)
            {
                velocity -= new Vector2(
                    Mathf.Sign(velocity.x) * friction,
                    0);
                //velocity = new Vector2(0, velocity.y);
            }
            else
            {
                velocity = new Vector2(0, velocity.y);
            }
            //Stop run animation if at rest
            if (velocity.x == 0)
            {
                m_animator.SetBool("isRunning", false);
            }
        }

        // Cast a ray horizontally to determine wall slides
        if (Physics2D.Raycast(transform.position - new Vector3(0, m_racerHeight / 3.9f, 0), horizontalMovement * Vector2.right, GetComponent<BoxCollider2D>().size.x, GroundLayer.value) ||
            Physics2D.Raycast(transform.position + new Vector3(0, m_racerHeight / 2, 0), horizontalMovement * Vector2.right, GetComponent<BoxCollider2D>().size.x, GroundLayer.value) ||
            Physics2D.Raycast(transform.position, horizontalMovement * Vector2.right, GetComponent<BoxCollider2D>().size.x, GroundLayer.value)) 
        {
            m_wallSliding = true;
            velocity.x = 0;
            m_clingTimer = Designer.Instance.WallClingTimeSeconds;
        }
        else if(m_clingTimer <= 0)
        {
            m_wallSliding = false;
        }

        m_rigidbody.velocity = velocity;
    }

    /// <summary>
    /// Use collision to detect ground under the racer.
    /// </summary>
    private void CheckGround()
    {
        var groundOverlap = Physics2D.OverlapCircle(GroundDetectionObject.position, GroundCheckRadius, GroundLayer);
        m_onGround = groundOverlap;

        //Cancel jumping animation if on ground
        if (m_onGround)
        {
            m_animator.SetBool("isJumping", false);
        }

        // Check platforms
        //if(groundOverlap != null && groundOverlap.transform.GetComponent<MovingPlatform>())
        //{
        //    var velocity = groundOverlap.transform.GetComponent<Rigidbody2D>().velocity * Time.deltaTime;
        //    transform.position += new Vector3(velocity.x, velocity.y, 0);
        //}
    }

    /// <summary>
    /// Handle collisions with objects.
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(1 << coll.gameObject.layer == DeathLayer.value)
        {
            KillPlayer();
        }
    }

    /// <summary>
    /// Handle collisions with triggers.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (1 << other.gameObject.layer == DeathLayer.value)
        {
            KillPlayer();
        }
    }

    /// <summary>
    /// Handle jumping and ability use.
    /// </summary>
    private void HandleJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            // On the ground, we jump
            if(m_onGround)
            {
                ExecuteJump();
            }
            else if(m_wallSliding)
            {
                ExecuteWallJump();
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(m_rigidbody.velocity.y > m_minJumpVelocity)
            {
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_minJumpVelocity);
            }
        }

        //Apply Gravity.
        if(!GravityOverride)
        {
            float multiplier = m_wallSliding && m_rigidbody.velocity.y < 0 ? Designer.Instance.WallSlideMultiplier : 1;
            m_rigidbody.velocity += Vector2.up * m_gravity * multiplier * Time.deltaTime;
        }
    }

    /// <summary>
    /// Count down the timers.
    /// </summary>
    private void HandleTimers()
    {
        // Decrement the wall cling timer and wall jump timer.
        if (m_clingTimer > 0)
        {
            m_clingTimer -= Time.deltaTime;
        }
        if (m_wallJumpMovementLockoutTimer > 0)
        {
            m_wallJumpMovementLockoutTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Kill the racer. Move it back a bit.
    /// </summary>
    private void KillPlayer()
    {
        Debug.Log("PlayerMovement.KillPlayer()");
    }
}