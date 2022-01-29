using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlakeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //Stores all abilities found in resources folder
        List<AbilitySystem.AbilityModifier> abilities = new List<AbilitySystem.AbilityModifier>();



        //Loop through all assemblies and get all object types that inherit "AbilityModifier"    - Not as slow as you would think, still worth caching the results
        foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(AbilitySystem.AbilityModifier))))
        {


            //Loop through the resources folder to find all instances of the inherited types found in the outer loop
            foreach (var item in Resources.FindObjectsOfTypeAll(type))
            {

                //don't add duplicates - duplicates will occur because we're searching for all types(including the base type)
                if (!abilities.Contains(item))
                {
                    //Add ability to list stored as simple type
                    abilities.Add(item as AbilitySystem.AbilityModifier);
                }
            }
        }


        //only here to confirm that this works
        foreach (var item in abilities)
        {
            print(item);
        }
    }
}
