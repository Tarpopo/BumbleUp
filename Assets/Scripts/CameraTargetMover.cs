using System;
using UnityEngine;

public class CameraTargetMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    private Vector3 _direction;

    private void Start() => _direction = (transform.position - _target.position);

    private void FixedUpdate()
    {
        if (_target == null) return;
        var position = _target.position + _direction;
        position.z = _direction.z;
        transform.position = Vector3.Lerp(transform.position, position, _speed);
    }
}

[Serializable]
public class CameraParameters
{
    public bool IsTarget => _target != null;
    public Vector3 Rotation => _rotation;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private bool _onlyXMoving;

    public void SetDirection(Vector3 position) => _direction = (position - _target.position).normalized;
    public Vector3 TargetPosition => _target.position;

    public Vector3 GetLerpPosition(Vector3 position)
    {
        if (_onlyXMoving == false) return Vector3.Lerp(position, _target.position + _direction * _distance, _speed);
        position.x = Vector3.Lerp(position, _target.position + _direction * _distance, _speed).x;
        return position;
    }

    public Vector3 GetAllLerpPosition() => _target.position + _direction * _distance;
}