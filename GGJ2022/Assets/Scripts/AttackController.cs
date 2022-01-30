using AbilitySystem;
using BulletHandlers;
using UnityEngine;

[RequireComponent(typeof(WeaponAbilityComponent), typeof(HealthSystem))]
public class AttackController : MonoBehaviour
{
    private WeaponAbilityComponent weapon;

    private void Awake()
    {
        weapon = GetComponent<WeaponAbilityComponent>();
    }


    public void UseAttack()
    {
        var bullet = BulletPool.Instance.GetBullet();

        var collider = GetComponent<Collider>();

        bullet.OnInstantiation(transform, collider);

        weapon.Use(bullet);
    }


}
