using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_L2 : MonoBehaviour
{
    [Header("ACCIONES NECESARIAS")]
    public bool Explosivos_Colocados_OK;
    public bool Detonador_Conectado_OK;
    [Header("ACCIONES NO NECESARIAS")]
    public bool Luz_Helicoptero_ACT;
    [Header("GLOBALES")]
    public bool Puerta_Abierta;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //CAMBIOS EN VERIABLES DESDE BEHAVIOURS
    //PUERTA
    public void PuertaAbiertaTRUE()
    {
        Puerta_Abierta = true;
    }
}
