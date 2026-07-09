using UnityEngine;
using Mirror;
using System.Collections;

public class DeadZone : NetworkBehaviour
{
    [SerializeField]
    private bool EstoySaltando;
    [SerializeField]
    private float SpeedLimit = 1.5f;

    void Start()
    {
        if (isClientOnly)//DESACTIVA COLLIDER EN CLIENTES
            desColider();
    }

    private void desColider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    [ServerCallback]
    void OnTriggerEnter(Collider co)
    {
        if (co.tag == "Ubicacion")
        {
            if (co.GetComponentInParent<Player>().isDead || co.GetComponentInParent<Player>().Inmortal)
                return;

            //SALTO
            if (co.transform.parent.parent.GetComponentInParent<VelPlayer>().Speed > SpeedLimit)
            {
                EstoySaltando = true;
                StartCoroutine(Coroutine());
            }

            if (!EstoySaltando)
                co.GetComponentInParent<Caida>().cayendo = true;
        }
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(1);  //TIEMPO HASTA CAER O MORIR 
        EstoySaltando = false;
    }

    [ServerCallback]
    void OnTriggerStay(Collider co)
    {
        if (co.tag == "Ubicacion")
        {
            if (co.GetComponentInParent<Player>().isDead || co.GetComponentInParent<Player>().Inmortal)
                return;

            if (!EstoySaltando)
            {
                co.GetComponentInParent<Caida>().cayendo = true;
                // Quita vida al player
                co.GetComponentInParent<Player>().PlayerHealth -= co.GetComponentInParent<Player>().PlayerHealth; //MEJOR PONER =0???
                co.GetComponentInParent<Player>().Mensaje = "¡Estás Muerto, has caído al vacío!";
            }
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider co)
    {
        if (co.tag == "Ubicacion")
        {
            if (co.GetComponentInParent<Player>().isDead || co.GetComponentInParent<Player>().Inmortal)
                return;

            co.GetComponentInParent<Player>().Mensaje = ".";
            EstoySaltando = false;
        }
    }
    
    /*[ClientRpc]
    void RpcObj(bool act)
    {
        obj.SetActive(act);
    }*/

}
