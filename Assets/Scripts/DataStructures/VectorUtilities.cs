using UnityEngine;
using System.Collections;

public static class VectorUtilities {

    /// <summary>
    /// Determine if the target point is between pointA and pointB.
    /// NOTE: pointA and pointB must already be on the same line as lineStart and lineEnd.
    /// </summary>
    /// <param name="targetPointOnLine">The target to check. Determine if both A and B are past this point.</param>
    /// <param name="pointA">One point to check.</param>
    /// <param name="pointB">Another point to check.</param>
    /// <returns></returns>
	public static bool IsTargetBetweenPointsOnLine(Vector3 targetPointOnLine, Vector3 pointA, Vector3 pointB)
    {
        float pointOnVectorDotProduct = Vector3.Dot(pointB - pointA, pointB - targetPointOnLine);

        bool pastEndPoint =
            pointOnVectorDotProduct > 0 &&
            pointOnVectorDotProduct < Mathf.Pow(Vector3.Distance(pointB, pointA), 2);

        return pastEndPoint;
    }

    /// <summary>
    /// Determine if points A and B are past the lineEnd on the given line.
    /// </summary>
    /// <param name="lineStart">The start of the line to check.</param>
    /// <param name="lineEnd">The end point on that line. The point to check if we are past.</param>
    /// <param name="pointA">The first point to check.</param>
    /// <param name="pointB">The second point to check.</param>
    /// <returns></returns>
    public static bool ArePointsPastLineEndPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 pointA, Vector3 pointB)
    {
        bool a = IsTargetBetweenPointsOnLine(lineEnd, lineStart, pointA);
        bool b = IsTargetBetweenPointsOnLine(lineEnd, lineStart, pointB);
        return a && b;
    }

    /// <summary>
    /// Given a position, get the closest point on the given line to the provided point.
    /// It will be the collision point of a right-angle line to the provided line.
    /// </summary>
    /// <param name="position">The point off of the line.</param>
    /// <param name="lineStart">The start of the line to check.</param>
    /// <param name="lineEnd">The end of the line to check.</param>
    /// <returns>The closes point on the line.</returns>
    public static Vector3 GetClosestPointOnLine(Vector3 position, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 aTOb = lineEnd - lineStart;
        Vector3 aTOp = position - lineStart;
        float tempDist = Vector3.Dot(aTOb, aTOp) / Mathf.Pow(Vector3.Magnitude(aTOb), 2);
        return new Vector3(lineStart.x + aTOb.x * tempDist,
                           lineStart.y + aTOb.y * tempDist,
                           lineStart.z + aTOb.z * tempDist);
    }
}
