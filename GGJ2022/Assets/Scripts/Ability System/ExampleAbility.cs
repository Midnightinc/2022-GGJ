using UnityEngine;

namespace AbilitySystem
{
    public class ExampleAbility : WeaponModifier
    {

        public ExampleAbility()
        {
            OnAdded += ExampleAbilityAdded;
        }

        private void ExampleAbilityAdded(WeaponAbilityComponent obj)
        {

        }



        /// <summary>
        /// Multishot fires 3 bullets instead of one in an evenly distributed mannaer
        /// </summary>
        /// <param name="bullet"></param>
        /// <returns></returns>
        public override float Use(GameObject bullet)
        {

            //get direction of bullet
            //instantiate two extra bullets at N angles offset

            return 0;
        }
    }
}