using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsObserver
{
    void OnHealthChanged(int currentHealth, int maxHealth);
    void OnGemAdded();
    void OnManaChanged(int currenMana, int maxMana);

    void OnCoinsChanged(int currentCoins);
   

}

