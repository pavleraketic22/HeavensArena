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
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 desiredPostion = player.position + offest;
        transform.position = Vector3.Lerp(transform.position, desiredPostion, speed * Time.deltaTime);
    }
}
