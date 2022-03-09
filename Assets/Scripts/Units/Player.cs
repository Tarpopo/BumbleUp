using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ForwardMover))]
[RequireComponent(typeof(HorizontalMover))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticles;
    private readonly Dictionary<MoveType, IMove> _movers = new Dictionary<MoveType, IMove>();
    private IMove _currentMover;
    private Health _health;

    private void Start()
    {
        foreach (var movers in GetComponents<IMove>()) _movers.Add(movers.MoveType, movers);
        _health = GetComponent<Health>();
        _health.OnTakeDamage += DisablePlayer;
        _currentMover = _movers[MoveType.Forward];
    }

    private void DisablePlayer()
    {
        _deathParticles.transform.position = transform.position;
        _deathParticles.Play();
        gameObject.SetActive(false);
    }

    public void EnablePlayer() => gameObject.SetActive(true);

    private void OnEnable()
    {
        PlayerInput.Instance.OnSwipeHorizontal += MoveHorizontal;
        PlayerInput.Instance.OnTouchUp += MoveForward;
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnSwipeHorizontal -= MoveHorizontal;
        PlayerInput.Instance.OnTouchUp -= MoveForward;
    }

    private void MoveHorizontal() => TryMove(MoveType.Horizontal);

    private void MoveForward() => TryMove(MoveType.Forward);

    private void TryMove(MoveType moveType)
    {
        if (_movers.ContainsKey(moveType) == false) return;
        _currentMover.StopMove();
        _currentMover = _movers[moveType];
        _currentMover.Move();
    }
}