using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;

public class GiroLaser : NetworkBehaviour
{
    [SerializeField]
    private Transform Laser;
    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    private float LimiteRotacion;
    [SerializeField]
    private AudioSource Sonido;

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    void Update()
    {
        // Obtiene la rotación local actual del objeto
        Vector3 currentRotation = Laser.transform.localEulerAngles;
        // Limita el ángulo en el eje Y a un rango de -45 a 45 grados, incluyendo valores negativos
        float clampedYRotation = currentRotation.x;
        if (clampedYRotation > 180f)
        {
            clampedYRotation -= 360f;
        }
        clampedYRotation = Mathf.Clamp(clampedYRotation, -LimiteRotacion, LimiteRotacion);
        // Crea una nueva rotación local limitando el ángulo en el eje Y
        Vector3 clampedLocalRotation = new Vector3(clampedYRotation, currentRotation.y, currentRotation.z);
        // Asigna la nueva rotación local al objeto
        Laser.transform.localEulerAngles = clampedLocalRotation;
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        Sonido.Play();
        RpcActSonido();
    }

    [ServerCallback]
    void OnTriggerStay(Collider other)
    {
        Laser.transform.Rotate(RotationSpeed * Time.deltaTime, 0, 0);
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        Sonido.Stop();
        RpcDesSonido();
    }

    [ClientRpc]
    void RpcActSonido()
    {
        Sonido.Play();
    }
    [ClientRpc]
    void RpcDesSonido()
    {
        Sonido.Stop();
    }

}
