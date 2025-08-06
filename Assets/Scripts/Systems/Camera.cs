using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Camera : MonoBehaviour
{

    public Transform player;
    public float speed;
    public Vector3 offest;
    private SpriteRenderer playerSprite;
    void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        Vector3 dynamicOffset = offest;

        if (playerSprite.flipX)
        {
            dynamicOffset.x = -offest.x;
        }
        Vector3 desiredPostion = player.position + dynamicOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPostion, speed * Time.deltaTime);
    }
}
