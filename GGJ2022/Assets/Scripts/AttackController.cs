using AbilitySystem;
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


        BulletClass bullet = Instantiate(bull);
        bullet.OnInstantiation(transform);

        weapon.Use(bullet);
    }


}
