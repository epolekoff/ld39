/// <summary>
/// A box of four TrackSpaceCoordinate corners.
/// </summary>
class TrackSpaceBoundingBox
{
    /// <summary>
    /// (0, 0)
    /// </summary>
    public TrackSpaceCoordinate BottomLeft { get; set; }

    public TrackSpaceCoordinate BottomRight { get; set; }

    /// <summary>
    /// (1, 1)
    /// </summary>
    public TrackSpaceCoordinate TopRight { get; set; }

    public TrackSpaceCoordinate TopLeft { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bottomLeft"></param>
    /// <param name="topRight"></param>
    public TrackSpaceBoundingBox(TrackSpaceCoordinate bottomLeft, TrackSpaceCoordinate topRight)
    {
        this.BottomLeft = bottomLeft;
        this.TopRight = topRight;

        this.BottomRight = new TrackSpaceCoordinate(bottomLeft.Position, topRight.Rotation, bottomLeft.Height);
        this.TopLeft = new TrackSpaceCoordinate(topRight.Position, bottomLeft.Rotation, bottomLeft.Height);
    }
}
