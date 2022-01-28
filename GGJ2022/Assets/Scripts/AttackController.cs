using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    private Action Shoot;

    private float firerate = 8f;
    private float nextTimetoFire = 0.5f;

    private int currentBullets = 0;
    private int maxBullets = 16;

    private float range = 10f;
    public LayerMask ignoreMe;

    public GameObject bullets;
    public GameObject gunBarrel;
    public BulletManager manager;

    private RaycastHit Hit;
    Ray ray;

    private void Start()
    {
        currentBullets = maxBullets;
        Upgrade(BasicShoot);
    }


    private void Fire()
    {
        Shoot.Invoke();
    }

    private void BasicShoot()
    {
        Vector3 infront = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, infront, out Hit, range, ~ignoreMe))
        {
            manager.Bullets();
        }
    }
    public void Upgrade(Action Upgrade)
    {
        Shoot += Upgrade;
    }
    private void BehindShot()
    {

    }

    private void DoubleBullets()
    {

    }



    // Pierce (ramping up damage?)
    // Shoot forward and bacward
    // ricocheting bullets (bouncy bullets )
    // upgrade damage?
    // Explosive ammo?
    // increase ammo count?
    // increase ammo size
    // Shoot twice








    //Blakes Demo code
    /*
     using AbilitySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(WeaponAbilityComponent))]
public class ThisClassMeansThisObjectCanAttack : MonoBehaviour
{
    private WeaponAbilityComponent weapon;

    BulletClass bull;

    private void Awake()
    {
        weapon = GetComponent<WeaponAbilityComponent>();
    }


    public void UseAttack()
    {


        BulletClass bullet = Instantiate(bull);
        bullet.OnInstantiation();

        abilityComponent.Use(bullet);
    }


}




public class BulletClass : MonoBehaviour
{

    public Vector3 direction;
    public float damage;
    public BulletModifier[] modifiers;
    public LayerMask hittableLayer;

    public void OnInstantiation(Transform fromTransform, BulletModifier[] mods = null)
    {
        this.transform.rotation = fromTransform.rotation;
        direction = transform.eulerAngles;

        if (mods != null)
        {
            modifiers = mods;
        }
    }


    private void OnDisable()
    {
        modifiers = null;
        hittableLayer = 0;
        damage = 0;
        direction = Vector3.zero;
    }
}

     */

}
