using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_L3 : MonoBehaviour
{
    [Header("GLOBALES")]
    public bool Esferas_Azules_Ok;
    public ManagerEsferasPuerta managerPuerta;

    void Update()
    {
        if (managerPuerta.EsferasPuertaOk)
        {
            EsferasAzulesOk();
        }
    }

    //CAMBIOS EN VERIABLES DESDE MANAGERESFERAPUERTAS
    public void EsferasAzulesOk()
    {
        Esferas_Azules_Ok = true;
    }
}
