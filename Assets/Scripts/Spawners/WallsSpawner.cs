using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class WallsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startWall;
    [SerializeField] private Transform _endWall;
    [SerializeField] private GameObject _wall;
    [SerializeField] private int _wallsCount;
    [SerializeField] private WallsPositions _wallsPositions;
    [SerializeField] private UnityEvent _onWallsSpawn;
    private readonly List<GameObject> _activeWalls = new List<GameObject>();
    private Vector3 _direction;
    private Transform _currentWall;

    public event UnityAction OnWallsSpawn
    {
        add => _onWallsSpawn.AddListener(value);
        remove => _onWallsSpawn.RemoveListener(value);
    }

    private void Start()
    {
        _currentWall = _endWall.transform;
        _direction = _endWall.position - _startWall.position;
        ManagerPool.Instance.AddPool(PoolType.Entities).PopulateWith(_wall, 15);
        _wallsPositions.OnPointsEmpty += SpawnWalls;
        _wallsPositions.OnPointsEmpty += DespawnWalls;
    }

    private void SpawnWalls() => StartCoroutine(Spawn());
    private void DespawnWalls() => StartCoroutine(Despawn());

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _wallsCount; i++)
        {
            var wall = ManagerPool.Instance.Spawn(PoolType.Entities, _wall, _currentWall.position + _direction);
            _activeWalls.Add(wall);
            _currentWall = wall.transform;
            _wallsPositions.AddNewPosition(_currentWall);
            yield return new WaitForFixedUpdate();
        }
        _onWallsSpawn?.Invoke();
    }


    private IEnumerator Despawn()
    {
        for (int i = 0; i < _activeWalls.Count / 3; i++)
        {
            ManagerPool.Instance.Despawn(PoolType.Entities, _activeWalls[i]);
            yield return new WaitForFixedUpdate();
        }
        _activeWalls.RemoveRange(0, _activeWalls.Count / 3);
    }
}