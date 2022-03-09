using System.Collections;
using UnityEngine;

public class ForwardMover : MonoBehaviour, IMove
{
    [SerializeField] private MoveType _moveType;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Vector3 _delta;
    [SerializeField] private int _frames;
    [SerializeField] private WallsPositions _walls;
    public MoveType MoveType => _moveType;
    public bool Moving => _Coroutine != null;
    private Coroutine _Coroutine;

    public void Move() => _Coroutine = StartCoroutine(BezierMove(_frames));

    public void StopMove()
    {
        if (Moving == false) return;
        StopCoroutine(_Coroutine);
    }

    private void OnDrawGizmos()
    {
        var startPoint = transform.position;
        var endPoint = _endPoint.position;
        for (int i = 0; i < 20; i++)
        {
            var parameter = (float) i / 20;
            var point = BezierCurve.GetPointOnCurve(startPoint, endPoint, parameter, _delta);
            Gizmos.DrawLine(startPoint, point);
            startPoint = point;
        }
    }

    private IEnumerator BezierMove(int frames)
    {
        var startPoint = transform.position;
        var endPoint = _walls.GetWallPosition() + _walls.Direction;
        endPoint.z = startPoint.z;
        for (int i = 0; i < frames; i++)
        {
            var parameter = (float) i / frames;
            var point = BezierCurve.GetPointOnCurve(startPoint, endPoint, parameter, _delta);
            transform.position = point;
            startPoint = point;
            yield return new WaitForFixedUpdate();
        }

        _Coroutine = null;
    }
}