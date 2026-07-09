using UnityEngine;
using Mirror;

public class ZonaUbicacionL0 : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ubicacion")
        {
            GameManager.GetComponent<JuegoManager>().PlayersUbicadosL0 += 1;
        }
    }
    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ubicacion")
        {
            GameManager.GetComponent<JuegoManager>().PlayersUbicadosL0 -= 1;
        }
    }

}
