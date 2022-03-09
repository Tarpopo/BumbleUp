using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private WallsSpawner _wallsSpawner;
    [SerializeField] private WallsPositions _wallsPositions;
    [SerializeField] private Vector3 _direction;
    private readonly List<GameObject> _currentEnemies = new List<GameObject>();

    private void Start()
    {
        ManagerPool.Instance.AddPool(PoolType.Entities).PopulateWith(_enemy, 2);
        _wallsSpawner.OnWallsSpawn += Spawn;
        _wallsPositions.OnPointsEmpty += Despawn;
    }

    private void Spawn()
    {
        var transforms = _wallsPositions.GetAllPositions();
        _currentEnemies.Add(ManagerPool.Instance.Spawn(PoolType.Entities, _enemy,
            transforms[transforms.Length - 1].position + _direction));
        _currentEnemies[_currentEnemies.Count - 1].GetComponent<EnemyPositions>().OnStart(transforms, _player);
    }

    private void Despawn()
    {
        if (_currentEnemies.Count <= 1) return;
        ManagerPool.Instance.Despawn(PoolType.Entities, _currentEnemies[0]);
        _currentEnemies.RemoveAt(0);
    }

    public void DespawnAll()
    {
        foreach (var enemy in _currentEnemies) ManagerPool.Instance.Despawn(PoolType.Entities, enemy);
        _currentEnemies.Clear();
    }
}