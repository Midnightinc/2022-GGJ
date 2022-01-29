using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoundUI : MonoBehaviour
{
    // Reference for UI buttons, images for the abilities, and description for it 
    [System.Serializable]
    private struct EndRoundReferences
    {
        public GameObject firstChoice;
        public GameObject secondChoice;
        public Image firstImageChoice;
        public Image secondImageChoice;
        public Text firstTextChoice;
        public Text secondTextChoice;
    }

    [SerializeField] EndRoundReferences endRoundChoice;
    // To be tested (Changing images for specic ability inside index)
    [SerializeField] Sprite choiceImages;
    //Stores all abilities found in resources folder
    List<AbilitySystem.AbilityModifier> abilities = new List<AbilitySystem.AbilityModifier>();
    // Make new list for displayed abilities
    List<int> DisplayedAbilities = new List<int>();
    // Make new list for ability images
    List<Sprite> abilityImages = new List<Sprite>();
    bool choiceTaken = false;
    // Set to true if player completes a room for  endround
    public bool endround;

    void Start()
    {
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
        // Testing below on changing the ability image
        abilityImages.Add(choiceImages);

    }

    private void LateUpdate()
    {
        RoundCheck();
    }

    private void RoundCheck()
    {
         // Check if the end round is over
        if (endround == true)
        {
            // Set both the gameobject UI's to true
            endRoundChoice.firstChoice.SetActive(true);
            endRoundChoice.secondChoice.SetActive(true);
            // Set time to "pause"
            Time.timeScale = 0;
        }
        // Checks if player took a choice
        if (choiceTaken == true)
        {
            // Disables both the UI after player took a choice
            endRoundChoice.firstChoice.SetActive(false);
            endRoundChoice.secondChoice.SetActive(false);
            // Enables back time incrementation
            Time.timeScale = 1;
        }

    }

    public void FirstChoice()
    {
        if (!choiceTaken)
        {
            bool acceptableIndex = false;
            // Set firstIndex to -1 so that the Random goes through the list of abilities
            int firstIndex = -1;
            if (DisplayedAbilities.Count == abilities.Count)
            {
                // Returns if both the displayed abilities and the abilities inside the ability modifier is equaled to each other
                return;
            }
            while (!acceptableIndex)
            {
                // Sets the first index to a random range between different abilities stored in the modifier class   
                firstIndex = UnityEngine.Random.Range(0, abilities.Count);
                // Checks if the displayed abilities was already used
                foreach (var usedIndex in DisplayedAbilities)
                {
                    if (usedIndex == firstIndex)
                    {
                        acceptableIndex = false;
                        break;
                    }
                }
                // Set true to display the acceptable index
                acceptableIndex = true;
            }
            // Add the picked first index from the list to the displayed abilities
            DisplayedAbilities.Add(firstIndex);
            AbilitySystem.AbilityModifier abilityChoice = abilities[firstIndex];
            // Displays the Ability in the game hud itself
            endRoundChoice.firstTextChoice.text = $"Ability: {abilityChoice}";

            // Instantiate Image prefab
            Image firstIndexImage = Instantiate(endRoundChoice.firstImageChoice);
            // Assign the image in the selected number
            firstIndexImage.sprite = abilityImages[firstIndex];
            // Set choice taken to true to go to next round
            choiceTaken = true;
        }
    }
    public void SecondChoice()
    {
        if (!choiceTaken)
        {
            bool acceptableIndex = false;
            // Set secondIndex to -1 so that the Random goes through the list of abilities
            int secondIndex = -1;
            if (DisplayedAbilities.Count == abilities.Count)
            {
                // Returns if both the displayed abilities and the abilities inside the ability modifier is equaled to each other 
                return;
            }
            while (!acceptableIndex)
            {
                // Sets the second index to a random range between different abilities stored in the modifier class   
                secondIndex = UnityEngine.Random.Range(0, abilities.Count);
                // Checks if the displayed abilities was already used
                foreach (var usedIndex in DisplayedAbilities)
                {
                    if (usedIndex == secondIndex)
                    {
                        acceptableIndex = false;
                        break;
                    }
                }
                // Set true to display the acceptable index
                acceptableIndex = true;
            }
            // Add the picked second index from the list to the displayed abilities
            DisplayedAbilities.Add(secondIndex);
            AbilitySystem.AbilityModifier abilityChoice = abilities[secondIndex];
            // Displays the Ability in the game hud itself
            endRoundChoice.secondTextChoice.text = $"Ability: {abilityChoice}";

            // Instantiate Image prefab
            Image secondIndexImage = Instantiate(endRoundChoice.secondImageChoice);
            // Assign the image in the selected number
            secondIndexImage.sprite = abilityImages[secondIndex];
            // Set choice taken to true to go to next round
            choiceTaken = true;
        }
    }
}
