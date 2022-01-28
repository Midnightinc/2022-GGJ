using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Purpose: To extend the functionality of the MonoBehaviour class
/// Created Date: 29th of January 2022
/// Author/s: Jonathan Nemec
/// Major Revision History: 
///     - 29th of January 2022 - Added a feature to call a function with a given delay
/// </summary>
public static class MonoBehaviourExtentions
{
    /// <summary>
    /// Will call a function with a given delay on a monobehaviour script
    /// </summary>
    /// <param name="mono">The script that this will be called on</param>
    /// <param name="method">The method that will be called after the delay</param>
    /// <param name="delay">The amount of time before the method is called</param>
    public static void CallWithDelay(this MonoBehaviour mono, Action method, float delay)
    {
        mono.StartCoroutine(StartWithDelay(method, delay));
    }

    /// <summary>
    /// Calls a function after a delay
    /// </summary>
    /// <param name="method">The method to call</param>
    /// <param name="delay">The amount of time before the method is called</param>
    /// <returns>The amount of time the system must wait</returns>
    static IEnumerator StartWithDelay(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}
