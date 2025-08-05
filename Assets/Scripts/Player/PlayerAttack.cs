using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private IAttackStrategy currentAttack;

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        currentAttack = strategy;
    }

    public void PerformAttack()
    {
        if (currentAttack != null)
            currentAttack.Attack();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // levi klik
        {
            SetAttackStrategy(new LightAttack());
            PerformAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) // desni klik
        {
            SetAttackStrategy(new HeavyAttack());
            PerformAttack();
        }
    }
}

