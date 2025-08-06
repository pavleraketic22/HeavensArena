using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private IAttackStrategy currentAttack;
    
    public Transform firePoint;
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        currentAttack = strategy;
    }

    public void PerformAttack(Vector3? position=null)
    {
        if (currentAttack != null)
            currentAttack.Attack(position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // levi klik
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            float direction = transform.localScale.x > 0 ? 1 : -1; 
            SetAttackStrategy(new FireballAttack(fireball, direction));
            PerformAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) // desni klik
        {
            UnityEngine.Camera cam = UnityEngine.Camera.main;
            SetAttackStrategy(new LightAttack(lightningPrefab, cam));
            //PerformAttack(hit.point);

        }
    }
}

