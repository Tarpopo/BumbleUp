using UnityEngine;

public static class BezierCurve
{
    public static Vector3 GetPointOnCurve(Vector3 from, Vector3 to, float t, Vector3 delta)
    {
        var middlePoint = Vector3.Lerp(from, to, 0.5f) + delta;
        var fromToMiddle = Vector3.Lerp(from, middlePoint, t);
        var middleTo = Vector3.Lerp(middlePoint, to, t);
        return Vector3.Lerp(fromToMiddle, middleTo, t);
    }
}