using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VelPlayer : NetworkBehaviour
{
    private Vector3 previousPosition;
    [SyncVar]
    public float Speed;

    [ServerCallback]
    void Start()
    {
        previousPosition = transform.position;
    }

    [ServerCallback]
    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;
        Speed = velocity.magnitude;
        //Debug.Log("Collision speed: " + Speed);

        previousPosition = currentPosition;
    }
}
