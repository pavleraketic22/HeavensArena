using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IHealthObserver.cs
public interface IHealthObserver
{
    void OnHealthChanged(int currentHealth, int maxHealth);
}

