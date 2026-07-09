using UnityEngine;
using Mirror;

public class ConectServidor : MonoBehaviour
{
    [SerializeField]
    private GameObject ManagerHud;

    [SerializeField]
    private GameObject LICENCIA, textLoading;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ComienzaServidor()
    {
        if (!ManagerHud)
            ManagerHud = GameObject.Find("NetworkManager");
        else
        {
            if (LICENCIA.GetComponent<licencia>().LicenciaOk)//COMPRUEBA LICENCIA
            {
                if (!NetworkClient.isConnected && !NetworkServer.active)
                    if (!NetworkClient.active)
                    {
                        textLoading.SetActive(true);
                        Invoke("comienzaServer", 0.5f);
                        //ManagerHud.GetComponent<NetworkManager>().StartServer();
                    }
            }
        }
    }

    void comienzaServer()
    {
        ManagerHud.GetComponent<NetworkManager>().StartServer();
    }
}