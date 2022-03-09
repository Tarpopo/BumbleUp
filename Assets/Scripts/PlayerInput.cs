using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : Singleton<PlayerInput>
{
    [SerializeField] private float _minSwipeDistance;
    [SerializeField] private float _minActive;
    [SerializeField] private UnityEvent _onTouchUp;
    private Vector2 _startPosition;
    private Vector2 _direction;
    private bool _activeRadius => Vector2.Distance(_startPosition, Input.mousePosition) >= _minSwipeDistance;

    public event UnityAction OnTouchUp
    {
        add => _onTouchUp.AddListener(value);
        remove => _onTouchUp.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _onSwipeHorizontal;

    public event UnityAction OnSwipeHorizontal
    {
        add => _onSwipeHorizontal.AddListener(value);
        remove => _onSwipeHorizontal.RemoveListener(value);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _direction = ((Vector2) Input.mousePosition - _startPosition).normalized;
            if (Mathf.Abs(Vector2.Dot(Vector2.right, _direction)) >= _minActive && _activeRadius)
                _onSwipeHorizontal?.Invoke();
            else _onTouchUp?.Invoke();
        }
    }

    public int GetSingDirection() => Math.Sign(_direction.x);
}