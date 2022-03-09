using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveDelay;
    private readonly Timer _timer = new Timer();
    private IMove _move;
    
    private void Start()
    {
        _move = GetComponent<IMove>();
        Move();
    }

    private void Move()
    {
        _move.StopMove();
        _move.Move();
        _timer.StartTimer(_moveDelay, Move);
    }

    private void Update() => _timer.UpdateTimer();
}