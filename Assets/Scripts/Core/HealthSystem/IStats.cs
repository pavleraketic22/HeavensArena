using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    void TakeDamage(int amount);
    void Heal(int amount);
    int CurrentHealth { get; }
    int MaxHealth { get; }
    void RegisterObserver(IStatsObserver observer);
    void UnregisterObserver(IStatsObserver observer);
    void AddGem();

    void AddCoins(int amount);
    void UseCoins(int amount);
}
