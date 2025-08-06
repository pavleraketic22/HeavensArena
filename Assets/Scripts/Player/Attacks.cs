using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightAttack : IAttackStrategy
{
    private GameObject lightningPrefab;
    private UnityEngine.Camera cam;

    public LightAttack(GameObject prefab , UnityEngine.Camera camera)
    {
        lightningPrefab = prefab;
        cam = camera;
    }

    public void Attack(Vector3? targetPosition = null)
    {
        Debug.Log("Light Attack performed!");

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject.Instantiate(lightningPrefab, hit.point, Quaternion.identity);
        }
    }
}


public class FireballAttack : IAttackStrategy
{
    private GameObject fireball;
    private float direction;
    public float speed = 5f;

    public FireballAttack(GameObject fireball, float direction)
    {
        this.fireball = fireball;
        this.direction = direction;
    }

    public void Attack(Vector3? targetPosition = null)
    {
        
        // pomeranje metka desno ili levo, zavisno gde igrač gleda
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * speed, 0);
    }
}


