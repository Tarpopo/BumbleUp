using UnityEngine;

public class DamageableApplyer : MonoBehaviour
{
    [SerializeField] private int _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable) == false) return;
        damageable.TakeDamage(_damage);
    }
}
