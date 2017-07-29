using UnityEngine;
using System.Collections;

public class TrackSpaceCoordinate {

    [SerializeField]
    public float Position = 0;

    [SerializeField]
    public float Rotation = 0;

    [SerializeField]
    public float Height = 0;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public TrackSpaceCoordinate(float position, float rotation, float height)
    {
        this.Position = position;
        this.Rotation = rotation;
        this.Height = height;
    }

    /// <summary>
    /// Overload the subtraction operator.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static TrackSpaceCoordinate operator -(TrackSpaceCoordinate A, TrackSpaceCoordinate B)
    {
        return new TrackSpaceCoordinate(A.Position - B.Position, A.Rotation - B.Rotation, A.Height = B.Height);
    }

    /// <summary>
    /// Overload the addition operator.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static TrackSpaceCoordinate operator +(TrackSpaceCoordinate A, TrackSpaceCoordinate B)
    {
        return new TrackSpaceCoordinate(A.Position + B.Position, A.Rotation + B.Rotation, A.Height + B.Height);
    }

    /// <summary>
    /// Overload the multiplication operator.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static TrackSpaceCoordinate operator *(TrackSpaceCoordinate A, TrackSpaceCoordinate B)
    {
        return new TrackSpaceCoordinate(A.Position * B.Position, A.Rotation * B.Rotation, A.Height * B.Height);
    }

    /// <summary>
    /// Overload the division operator.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static TrackSpaceCoordinate operator /(TrackSpaceCoordinate A, int B)
    {
        return new TrackSpaceCoordinate(A.Position / B, A.Rotation / B, A.Height / B);
    }

    /// <summary>
    /// Overload the equality check.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static bool operator ==(TrackSpaceCoordinate A, TrackSpaceCoordinate B)
    {
        bool equal = false;
        try
        {
            equal = A.Position == B.Position && A.Rotation == B.Rotation && A.Height == B.Height;
        }
        catch (System.Exception)
        {
            return false;
        }
        
        return equal;
    }

    /// <summary>
    /// Overload the inequality check.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static bool operator !=(TrackSpaceCoordinate A, TrackSpaceCoordinate B)
    {
        bool equal = false;
        try
        {
            equal = A.Position != B.Position || A.Rotation != B.Rotation || A.Height != B.Height;
        }
        catch (System.Exception)
        {
            return true;
        }

        return equal;
    }

    /// <summary>
    /// Equals
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return (TrackSpaceCoordinate)obj == this;
    }

    /// <summary>
    /// Hashbrowns
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Position.GetHashCode() * Rotation.GetHashCode() * Height.GetHashCode();
    }
}
