using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void TakeDamage(int amount);
    void Heal(int amount);
    int CurrentHealth { get; }
    int MaxHealth { get; }
    void RegisterObserver(IHealthObserver observer);
    void UnregisterObserver(IHealthObserver observer);
    void AddGem();
}
