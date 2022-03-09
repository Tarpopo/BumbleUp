using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    private int _currentHealth;
    private bool _dead => _currentHealth <= 0;
    [SerializeField] private UnityEvent _onTakeDamage;

    public event UnityAction OnTakeDamage
    {
        add => _onTakeDamage.AddListener(value);
        remove => _onTakeDamage.RemoveListener(value);
    }

    private void ResetHealth() => _currentHealth = _health;

    public void TakeDamage(int damage = 1)
    {
        if (_dead) return;
        _currentHealth -= damage;
        _onTakeDamage?.Invoke();
    }

    private void OnEnable() => ResetHealth();
}