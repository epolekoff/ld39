using UnityEngine;
using System.Collections;
using System.IO;

public class Designer : Singleton<Designer> {

    private const string Directory = "DesignerVariables";
    private const string Filename = "DesignerVariables";
    private const string Extension = ".json";

    private SerializableDesignerVariables m_vars;

    void Awake()
    {
        ReadVariablesFromFile();
    }

    public float MovementSpeed { get { return m_vars.MovementSpeed; } }
    public float AirMovementSpeed { get { return m_vars.AirMovementSpeed; } }
    public float MovementSpeedGroundFriction { get { return m_vars.MovementSpeedGroundFriction; } }
    public float MovementSpeedAirFriction { get { return m_vars.MovementSpeedAirFriction; } }
    public float MaxJumpHeight { get { return m_vars.MaxJumpHeight; } }
    public float MinJumpHeight { get { return m_vars.MinJumpHeight; } }
    public float TimeToMaxHeight { get { return m_vars.TimeToMaxHeight; } }

    public float ChargeRate { get { return m_vars.ChargeRate; } }
    public float MinCharge { get { return m_vars.MinCharge; } }
    public float MaxCharge { get { return m_vars.MaxCharge; } }
    public float ForceChargeMultiplier { get { return m_vars.ForceChargeMultiplier; } }

    public float BatteryRechargeAmount { get { return m_vars.BatteryRechargeAmount; } }

    /// <summary>
    /// If you hold Charge for 1 second, how much power is used, in % from [0,1].
    /// </summary>
    public float ChargePowerUsageRatio { get { return m_vars.ChargePowerUsageRatio; } }

    public float MaxWallJumpHeight { get { return m_vars.MaxWallJumpHeight; } }
    public float WallSlideMultiplier { get { return m_vars.WallSlideMultiplier; } }
    public float WallJumpKickoffVelocity { get { return m_vars.WallJumpKickoffVelocity; } }
    public float WallClingTimeSeconds { get { return m_vars.WallClingTimeSeconds; } }
    public float WallJumpMovementLockoutTimeSeconds { get { return m_vars.WallJumpMovementLockoutTimeSeconds; } }

    /// <summary>
    /// Read the variables from the config in the project.
    /// </summary>
    public void ReadVariablesFromFile()
    {
        // Load the file, then deserialize it.
        TextAsset designerData = Resources.Load<TextAsset>(Path.Combine(Directory, Filename));
        m_vars = JsonUtility.FromJson<SerializableDesignerVariables>(designerData.text);
    }
}

[System.Serializable]
public class SerializableDesignerVariables
{
    [SerializeField]
    public float MovementSpeed;
    [SerializeField]
    public float AirMovementSpeed;
    [SerializeField]
    public float MovementSpeedGroundFriction;
    [SerializeField]
    public float MovementSpeedAirFriction;
    [SerializeField]
    public float MaxJumpHeight;
    [SerializeField]
    public float MinJumpHeight;
    [SerializeField]
    public float TimeToMaxHeight;

    [SerializeField]
    public float ChargeRate;
    [SerializeField]
    public float MinCharge;
    [SerializeField]
    public float MaxCharge;
    [SerializeField]
    public float ForceChargeMultiplier;

    [SerializeField]
    public float BatteryRechargeAmount;

    /// <summary>
    /// If you hold Charge for 1 second, how much power is used, in % from [0,1].
    /// </summary>
    [SerializeField]
    public float ChargePowerUsageRatio;

    [SerializeField]
    public float MaxWallJumpHeight;
    [SerializeField]
    public float WallSlideMultiplier;
    [SerializeField]
    public float WallJumpKickoffVelocity;
    [SerializeField]
    public float WallClingTimeSeconds;
    [SerializeField]
    public float WallJumpMovementLockoutTimeSeconds;
}
