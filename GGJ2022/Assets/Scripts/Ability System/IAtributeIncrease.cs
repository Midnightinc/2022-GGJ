using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAtributeIncrease
{
    void IncreaseSpeed(float increasePercentage);

    void IncreaseHealth(float increasePercentage);

    void IncreaseDamage(float increasePercentage);

    void IncreaseRateOfFire(float increasePercentage);

    void IncreaseProjectileSpeed(float increasePercentage);
}
