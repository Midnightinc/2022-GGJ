using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, ICanHit
{
    private float health;
    [SerializeField] private float maxHealth;

    public UnityEvent onDeath;

    private void OnEnable()
    {
        health = maxHealth;
    }

    public void OnHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            print($"{gameObject.name} has died");
            onDeath?.Invoke();
        }
    }
}
