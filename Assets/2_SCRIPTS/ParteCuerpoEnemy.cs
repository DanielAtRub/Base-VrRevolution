using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ParteCuerpoEnemy : NetworkBehaviour
{
    //[SerializeField]
    //private GameObject PrefabCabeza;
    [SerializeField]
    private GameObject[] Oculta;
    private GameObject[] instanciaParte;
    [SerializeField]
    private ParticleSystem[] Efecto;
    [SerializeField]
    private AudioSource SonidoExplotaCabeza;
    private int d;

    // Start is called before the first frame update
    void Start()
    { 
    }

    [Server]
    public void Desmiembra(Collider parte) // LLAMADO DESDE DISPAROHITPLAYER (SERVER)
    {
        if (parte.tag == "EnemyCabeza") d = 0;
        //Desactiva collider
        parte.GetComponent<BoxCollider>().enabled = false;
        //Oculta Parte
        //for (int i = 0; i < Oculta.Length; i++)
        //{
            Oculta[d].SetActive(false);
        //}
        //efecto explota cabeza
        Efecto[d].Play();
        SonidoExplotaCabeza.Play();
        RpcOculta(d);
        //instancia Parte cuerpo
        //instanciaParte = Instantiate(PrefabCabeza, transform.position, transform.rotation);
        //NetworkServer.Spawn(instanciaParte);
    }

    [ClientRpc]
    private void RpcOculta(int fd) 
    {
        //for (int i = 0; i < Oculta.Length; i++)
        //{
            Oculta[fd].SetActive(false);
        //}
        Efecto[fd].Play();
        SonidoExplotaCabeza.Play();
    }
}
