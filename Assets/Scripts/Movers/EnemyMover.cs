using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IMove
{
    [SerializeField] private EnemyPositions _enemyPositions;
    [SerializeField] private MoveType _moveType;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Vector3 _delta;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private int _frames;
    [SerializeField] private Vector2 _zBorders;
    public MoveType MoveType => _moveType;
    public bool Moving => _coroutine != null;

    private Coroutine _coroutine;

    public void Move()
    {
        _coroutine = StartCoroutine(BezierMove(_frames));
        var rotation = transform.eulerAngles;
        rotation.x += 180;
        StartCoroutine(EulerRotate(transform, rotation, _frames*2));
    }

    public void StopMove()
    {
        if (Moving == false) return;
        StopAllCoroutines();
        _coroutine = null;
        //StopCoroutine(_coroutine);
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
        var endPoint = _enemyPositions.GetWallPosition() + _direction;
        endPoint.z = _enemyPositions.ZPlayerPosition;
        for (int i = 0; i < frames; i++)
        {
            var parameter = (float) i / frames;
            var point = BezierCurve.GetPointOnCurve(startPoint, endPoint, parameter, _delta);
            transform.position = point;
            startPoint = point;
            yield return new WaitForFixedUpdate();
        }

        _coroutine = null;
    }

    public static IEnumerator EulerRotate(Transform transform, Vector3 rotateTo, int frames)
    {
        var delta = Quaternion.Euler((rotateTo - transform.eulerAngles) / frames);
        for (int i = 0; i < frames; i++)
        {
            transform.rotation *= delta;
            yield return null;
        }
    }
}