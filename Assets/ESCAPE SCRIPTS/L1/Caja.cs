using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class Caja : NetworkBehaviour
{
    [SerializeField]
    private bool Completado;
    [SerializeField]
    private int objetosDentro;
    [SerializeField]
    private int objetosNecesarios;
    //[SerializeField]
    //private TextMeshPro textOk;
    [SerializeField]
    private GameObject ManagerL1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {
        if (objetosDentro == objetosNecesarios)
        {
            Completado = true;
            ManagerL1.GetComponent<Manager_L1>().Objetos_Guardados_OK = true;
            //textOk.text = "OK";
            //RpcTexto("OK");
            //ENVIO A GAMEADMIN
        }
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Explosivo"))
        {
            objetosDentro++;
            //textOk.text = objetosDentro.ToString();
            //RpcTexto(objetosDentro.ToString());
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Explosivo"))
        {
            objetosDentro--;
            //textOk.text = objetosDentro.ToString();
            //RpcTexto(objetosDentro.ToString());
        }
    }

    /*[ClientRpc]
    void RpcTexto (string t)
    {
        textOk.text = t;
    }*/
}
