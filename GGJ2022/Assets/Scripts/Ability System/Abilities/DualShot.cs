using UnityEngine;

namespace AbilitySystem
{
    public class DualShot : WeaponModifier
    {
        public override void Use(BulletClass bullet)
        {
        }
    }


    [CreateAssetMenu(fileName = "New Multishot", menuName = "Abilities/New MultiShot Ability", order = 1)]

    public class MultiShot : WeaponModifier
    {
        [Tooltip("Amount of extra bullets to spawn by this Multishot")] [SerializedField] private int quantity = 0;
        [Tooltip("The angle offset between each bullet spawned")] [SerializedField] private float angleOffset;

        public MultiShot()
        {
            OnAdded += MultiShotAdded;
        }

        ~MultiShot()
        {
            OnAdded -= MultiShotAdded;
        }

        private void MultiShotAdded(WeaponAbilityComponent obj, bool isAI)
        {
            if (isAI)
            {

            }
        }

        public override void Use(GameObject bullet)
        {
            for (int i = 0; i < quantity; i++)
            {
                var b = BulletPool.Instance.GetBullet();
                //setup bullet
            }
        }

    }
}