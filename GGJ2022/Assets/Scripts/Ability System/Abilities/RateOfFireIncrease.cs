using UnityEngine;

namespace AbilitySystem
{

    // Each ability is required to delcare itself within this asset menu ("Abilities/{}")

    [CreateAssetMenu(fileName = "Example", menuName = "Abilities/Rate of Fire Increase", order = 1)]

    /// <summary>
    /// Example ability is an outline or boiler plate for what could be an ability class
    /// </summary>
    public class RateOfFireIncrease : WeaponModifier
    {
        /// <summary>
        /// SomeValue is an example of an ability having editable properties on a Per Scriptable Object Basis, allowing for greater variance between abilities
        /// </summary>
        public float SomeValue;


        /// <summary>
        /// IF REQUIRED 
        /// 
        /// Subscribe to OnAdded function in constructor to ensure class is setup properly
        /// </summary>
        public RateOfFireIncrease()
        {
            OnAdded += RateOfFireAbilityAdded;
        }


        /// <summary>
        /// Don't forget to unsubscribe to OnAdded or i'll break your kneecaps (and also Unity will throw errors every time these objects are removed)
        /// </summary>
        ~RateOfFireIncrease()
        {
            OnAdded -= RateOfFireAbilityAdded;
        }


        /// <summary>
        /// Add custom functionality to when this ability is given to the player and or AI
        /// </summary>
        /// <param name="obj"></param>
        private void RateOfFireAbilityAdded(WeaponAbilityComponent obj, bool isAI)
        {
            obj.GetComponent<IAtributeIncrease>().IncreaseRateOfFire(0.5f);
        }

        /// <summary>
        /// override base USE function to create custom logic for each ability
        /// </summary>
        /// <param name="bullet"></param>
        /// <returns></returns>
        public override void Use(BulletClass bullet)
        {
        }
    }
}