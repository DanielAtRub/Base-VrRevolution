using UnityEngine;
using Mirror;

// COLOCADO EN EL LIMITE DEL JUGADOR
public class Rejilla : NetworkBehaviour
{
    private GameObject RejillaObj;
    [SerializeField]
    private GameObject Padre;

    [SerializeField]
    private GameObject ObjetoAlarma;

    void Start()
    {
    }

    [ClientCallback]
    void OnTriggerStay(Collider co)
    {
        if (co.CompareTag ("Limite"))
        {
            //Padre.GetComponent<Player>().Mensaje = "¡CUIDADO, Mantén la distancia de seguridad!";
            //Padre.GetComponent<Player>().Alarma = true;

            ObjetoAlarma.SetActive(true);

            if (Padre.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                // ACTIVA LA REJILLA AL JUGADOR CON EL QUE COLISIONAS
                RejillaObj = co.transform.Find("Rejilla").gameObject;
                RejillaObj.SetActive(true);
            }
        }
    }

    [ClientCallback]
    void OnTriggerExit(Collider co)
    {
        if (co.CompareTag ("Limite"))
        {
            //Padre.GetComponent<Player>().Mensaje = ".";
            //Padre.GetComponent<Player>().Alarma = false;

            ObjetoAlarma.SetActive(false);

            // DESACTIVA LA REJILLA AL JUGADOR CON EL QUE COLISIONAS
            RejillaObj = co.transform.Find("Rejilla").gameObject;
            RejillaObj.SetActive(false);
        }
    }
    
}
