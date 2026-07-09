using UnityEngine;
using Mirror;
using TMPro;

public class Athor : NetworkBehaviour {

    [SerializeField]
    private TextMeshPro textDEBUG;
    [SerializeField]
    private GameObject Jugador;

    void Start()
    {
        //if (!isLocalPlayer)
          //  enabled = false;
    }
    
    [ClientCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosivo") || other.CompareTag("Gema") || other.CompareTag("Conector"))
        {
            //if (!other.GetComponent<Cube>().TieneGravedad)
            Jugador.GetComponent<AthorPlayer>().PermisoOn(other);
            //else
                //Jugador.GetComponent<AthorPlayer>().PermisoOnG(other);
        }
    }

    [ClientCallback]
    void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Explosivo") || other.CompareTag("Gema") || other.CompareTag("Conector"))
        {
            //if (!other.GetComponent<Cube>().TieneGravedad)
            Jugador.GetComponent<AthorPlayer>().PermisoOff(other);
            //else
                //Jugador.GetComponent<AthorPlayer>().PermisoOffG(other);
        }
    }

}
