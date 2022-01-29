using AbilitySystem;
using BulletHandlers;
using UnityEngine;

[RequireComponent(typeof(WeaponAbilityComponent))]
public class AttackController : MonoBehaviour
{
    private WeaponAbilityComponent weapon;

    [SerializeField] private BulletClass bull;

    private void Awake()
    {
        weapon = GetComponent<WeaponAbilityComponent>();
    }


    public void UseAttack()
    {
        var bullet = BulletPool.Instance.GetBullet();
        bullet.OnInstantiation(transform);

        weapon.Use(bullet);
    }


}
