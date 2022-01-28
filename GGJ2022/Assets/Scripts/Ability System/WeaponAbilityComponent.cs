using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Contains a collection of abilities for use by a character controller
    /// </summary>
    public class WeaponAbilityComponent : MonoBehaviour
    {

        /// <summary>
        /// Enumerated list of modifiers
        /// </summary>
        private List<AbilityModifier> abilityModifiers = new List<AbilityModifier>();



        public void Use(GameObject bullet)
        {
            foreach (var mod in abilityModifiers)
            {
                mod.Use(bullet);
            }
        }

        public void Use(GameObject[] bullets)
        {
            foreach (var mod in abilityModifiers)
            {
                foreach (var bullet in bullets)
                {
                    mod.Use(bullet);
                }
            }
        }


        public bool ContainsAbilityType(Type type)
        {
            foreach (var ability in abilityModifiers)
            {
                if (ability.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Adds a modifer to this component based on the inherited type
        /// </summary>
        /// <param name="modifier"></param>
        public void AddModifier(AbilityModifier modifier)
        {
            //gaurd from adding duplicate abilities
            if (ContainsAbilityType(modifier.GetType()))
            {
                return;
            }

            abilityModifiers.Add(modifier);
            modifier.OnCollection(this);            
        }
    }



    public abstract class AbilityModifier : ScriptableObject
    {
        public event Action<WeaponAbilityComponent, bool> OnAdded;

        public AbilityModifier() { }

        public void OnCollection(WeaponAbilityComponent abilityComponent, bool isAI = false)
        {
            OnAdded?.Invoke(abilityComponent, isAI);
        }

        public abstract void Use(GameObject bullet);
    }


}