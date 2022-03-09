using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallsPositions : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _firstWall;
    [SerializeField] private Transform[] _baseTransforms;
    [SerializeField] private UnityEvent _onPointsEmpty;
    private Queue<Transform> _transforms;
    private Transform _currentPosition;
    public Vector3 Direction { get; private set; }

    public event UnityAction OnPointsEmpty
    {
        add => _onPointsEmpty.AddListener(value);
        remove => _onPointsEmpty.RemoveListener(value);
    }

    private void Awake()
    {
        _currentPosition = _firstWall;
        Direction = _player.position - _firstWall.position;
        _transforms = new Queue<Transform>(_baseTransforms);
    }

    public Vector3 GetWallPosition()
    {
        if (_transforms.Count == 6) _onPointsEmpty?.Invoke();
        _currentPosition = _transforms.Dequeue();
        return _currentPosition.position;
    }

    public Transform[] GetAllPositions() => _transforms.ToArray();

    public Vector3 GetlastPosition() => _currentPosition.position;

    public void AddNewPosition(Transform position) => _transforms.Enqueue(position);
}