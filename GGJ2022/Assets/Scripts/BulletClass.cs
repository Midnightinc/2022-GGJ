using AbilitySystem;
using BulletHandlers;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletClass : MonoBehaviour
{

    public LayerMask defaultHittableLayer;
    public float moveSpeed;
    public float baseDamage;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float damage;
    [HideInInspector] public BulletModifier[] modifiers;
    [HideInInspector] public LayerMask hittableLayer;

    private bool shouldRicochet = false;
    private bool yepPenetrate = false; //Thank James for the name of this one ;)



    private Rigidbody _rb;
    private Collider _collider;
    private Collider fromCollider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<Collider>();
        damage = baseDamage;
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            transform.position += (direction * moveSpeed) * Time.deltaTime;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (fromCollider != null)
        {
            if (fromCollider.gameObject.name == "Player" && collision.gameObject.name == "Player")
            {
                return;
            }
        }


        if (yepPenetrate)
        {
            yepPenetrate = false;
            return;
        }

        if (shouldRicochet)
        {
            shouldRicochet = false;
            direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
        }


        var health = collision.gameObject.GetComponent<ICanHit>();
        if (health != null)
        {
            health.OnHit(damage);
        }

        BulletPool.Instance.ReturnToPool(this);
    }


    private void OnDisable()
    {
        _rb.velocity = Vector3.zero;

        shouldRicochet = false;
        yepPenetrate = false;

        modifiers = null;
        hittableLayer = 0;
        damage = 0;
        direction = Vector3.zero;

        if (fromCollider != null)
        {
            Physics.IgnoreCollision(_collider, fromCollider, false);
            fromCollider = null;
        }
    }


    public void OnInstantiation(Transform fromTransform, Collider fromCollider = null, BulletModifier[] mods = null, bool shouldRicochet = false, bool yepPenetrate = false)
    {
        this.transform.rotation = fromTransform.rotation;
        direction = fromTransform.forward;
        direction.y = 0;
        direction = direction.normalized;


        this.shouldRicochet = shouldRicochet;
        this.yepPenetrate = yepPenetrate;

        if (mods != null)
        {
            modifiers = mods;
        }

        this.fromCollider = fromCollider;
        Physics.IgnoreCollision(_collider, fromCollider);
        

        transform.position = fromTransform.position;
        gameObject.SetActive(true);
    }
}
