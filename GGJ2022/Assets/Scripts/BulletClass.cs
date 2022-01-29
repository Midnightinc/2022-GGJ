using AbilitySystem;
using UnityEngine;

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
