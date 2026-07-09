using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube : NetworkBehaviour
{
    [SyncVar]
    public GameObject Parent;
    [SyncVar]
    public bool TieneGravedad;

    //Vuelve al original
    [SerializeField]
    private Transform Plataforma;
    [SerializeField]
    private Vector3 PosOriginal;
    [SerializeField]
    private Quaternion RotOriginal;
    [SerializeField]
    private float LimiteX, LimiteAbajo, LimitArriba, LimiteZ;

    //[SerializeField]
    //private Vector3 PosGlobal, PosLocal; //PRUEBAS

    void Start()
    {
        PosOriginal = transform.position;
        RotOriginal = transform.localRotation;
        LimiteX = 5f;
        LimiteAbajo = 3f;
        LimitArriba = 6f;
        LimiteZ = 10f;
    }

    void Update()
    {
        //ACTUALIZACION POSICION
        if (Parent != null)
        {
            transform.position = Parent.transform.position;
            transform.rotation = Parent.transform.rotation;
        }

        //PosGlobal = transform.position; // PRUEBAS
        //PosLocal = transform.localPosition; // PRUEBAS

        //Vuelve al original si se sale del limite
        if (Plataforma.position.x - transform.position.x > LimiteX || Plataforma.position.x - transform.position.x < -LimiteX)
        {
            //transform.position = Vector3.MoveTowards(transform.position, PosOriginal, 3f);
            transform.position = PosOriginal;
            transform.rotation = RotOriginal;
            if (TryGetComponent<RegresaPos>(out RegresaPos RegresaP))
            {
                RegresaP.InSnap = false;
            }
            //transform.position = PosOriginal;
            //transform.localRotation = RotOriginal;
        }
        if (Plataforma.position.y - transform.position.y > LimiteAbajo || Plataforma.position.y - transform.position.y < -LimitArriba)
        {
            //transform.position = Vector3.MoveTowards(transform.position, PosOriginal, 3f);
            transform.position = PosOriginal;
            transform.rotation = RotOriginal;
            if (TryGetComponent<RegresaPos>(out RegresaPos RegresaP))
            {
                RegresaP.InSnap = false;
            }
            //transform.position = PosOriginal;
            //transform.localRotation = RotOriginal;
        }
        if (Plataforma.position.z - transform.position.z > LimiteZ || Plataforma.position.z - transform.position.z < -LimiteZ)
        {
            //transform.position = Vector3.MoveTowards(transform.position, PosOriginal, 3f);
            transform.position = PosOriginal;
            transform.rotation = RotOriginal;
            if (TryGetComponent<RegresaPos>(out RegresaPos RegresaP))
            {
                RegresaP.InSnap = false;
            }
            //transform.position = PosOriginal;
            //transform.localRotation = RotOriginal;
        }
    }
}