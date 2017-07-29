using UnityEngine;
using System.Collections;

public class Designer : Singleton<Designer> {

    [Header("Player Movement")]
    [Space(10)]
    public float MovementSpeed = 10f;
    public float AirMovementSpeed = 10f;
    public float MovementSpeedGroundFriction = 1f;
    public float MovementSpeedAirFriction = 0f;
    public float MaxJumpHeight = 1f;
    public float MinJumpHeight = 0.1f;
    public float TimeToMaxHeight = 0.5f;

    [Header("Arm Powers")]
    [Space(10)]
    public float ChargeRate;
    public float MaxCharge;
    public float ForceChargeMultiplier;

    [Header("Items")]
    [Space(10)]
    public float BatteryRechargeAmount = 0.3f;

    /// <summary>
    /// If you hold Charge for 1 second, how much power is used, in % from [0,1].
    /// </summary>
    public float ChargePowerUsageRatio = .1f;

    [Header("Unused - Wall Jump")]
    [Space(10)]
    [Range(0, 1)]
    public float MaxWallJumpHeight = 1f;
    public float WallSlideMultiplier;
    public float WallJumpKickoffVelocity;
    public float WallClingTimeSeconds;
    public float WallJumpMovementLockoutTimeSeconds;
}
