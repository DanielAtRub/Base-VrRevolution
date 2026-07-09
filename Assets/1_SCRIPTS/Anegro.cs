using UnityEngine;
using Mirror;

// COLOCADO EN EL Bind_Joint DEL JUGADOR
public class Anegro : NetworkBehaviour
{
    void Start()
    {
    }

    [ServerCallback]
    void OnTriggerStay(Collider co)
    {
        /*GetComponentInParent<Player>().tVista = false;
        if (co.CompareTag("Anegro"))
            GetComponentInParent<Player>().tVista = true;*/

        if (co.CompareTag("Anegro"))
        {
            //GetComponentInParent<Player>().Mensaje = "¡Estás llegando...!";
            GetComponentInParent<Player>().aNegro = true;
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider co)
    {
        if (co.CompareTag("Anegro"))
        {
            //GetComponentInParent<Player>().Mensaje = ".";
            GetComponentInParent<Player>().aNegro = false;
        }
    }
}
