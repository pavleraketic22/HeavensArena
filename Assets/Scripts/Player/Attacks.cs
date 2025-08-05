using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : IAttackStrategy
{
    public void Attack()
    {
        Debug.Log("Light Attack performed!");
        // Dodaj animaciju, štetu itd.
    }
}

public class HeavyAttack : IAttackStrategy
{
    public void Attack()
    {
        Debug.Log("Heavy Attack performed!");
        // Dodaj animaciju, jaču štetu itd.
    }
}


