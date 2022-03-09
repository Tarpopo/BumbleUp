using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPositions : MonoBehaviour
{
    private readonly List<Vector3> _transforms = new List<Vector3>();
    private Vector3 _currentPosition;
    private Transform _playerTransform;
    public float ZPlayerPosition => _playerTransform.position.z;

    public void OnStart(IEnumerable<Transform> transforms, Transform player)
    {
        foreach (var transform in transforms) _transforms.Add(transform.position);
        _playerTransform = player;
    }

    public Vector3 GetWallPosition()
    {
        if (_transforms.Count == 0) return Vector3.zero;
        _currentPosition = _transforms[_transforms.Count - 1];
        _transforms.Remove(_currentPosition);
        return _currentPosition;
    }
}