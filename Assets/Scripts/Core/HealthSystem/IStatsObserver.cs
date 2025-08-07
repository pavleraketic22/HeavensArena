using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IHealthObserver.cs
public interface IStatsObserver
{
    void OnHealthChanged(int currentHealth, int maxHealth);
    void OnGemAdded();
}

