using AbilitySystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletClass : MonoBehaviour
{

    public LayerMask defaultHittableLayer;
    public float moveSpeed;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float damage;
    [HideInInspector] public BulletModifier[] modifiers;
    [HideInInspector] public LayerMask hittableLayer;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    public void OnInstantiation(Transform fromTransform, BulletModifier[] mods = null)
    {
        this.transform.rotation = fromTransform.rotation;
        direction = fromTransform.forward;
        direction.y = 0;
        direction = direction.normalized;

        if (mods != null)
        {
            modifiers = mods;
        }

        transform.position = fromTransform.position;
        gameObject.SetActive(true);
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
        
    }


    private void OnDisable()
    {
        _rb.velocity = Vector3.zero;

        modifiers = null;
        hittableLayer = 0;
        damage = 0;
        direction = Vector3.zero;
    }
}
