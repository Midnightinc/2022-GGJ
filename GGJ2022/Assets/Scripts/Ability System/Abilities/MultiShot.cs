using BulletHandlers;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "New Multishot", menuName = "Abilities/New MultiShot Ability", order = 1)]

    public class MultiShot : WeaponModifier
    {
        [Tooltip("Amount of extra bullets to spawn by this Multishot")] [SerializeField] private int quantity = 0;
        [Tooltip("The angle offset between each bullet spawned")] [SerializeField] private float angleOffset;

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

        public override void Use(BulletClass bullet)
        {
            for (int i = -(quantity / 2); i < quantity / 2; i++)
            {
                var newBullet = BulletPool.Instance.GetBullet();
                newBullet.modifiers = bullet.modifiers;

                newBullet.transform.Rotate(0, angleOffset * i, 0);
            }
        }

    }
}