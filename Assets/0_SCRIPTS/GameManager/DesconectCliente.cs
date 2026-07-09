using UnityEngine;
using Mirror;

public class DesconectCliente : MonoBehaviour
{
    [SerializeField]
    private GameObject ManagerHud;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ParaCliente()
    {
        if (!ManagerHud)
            ManagerHud = GameObject.Find("NetworkManager");
        ManagerHud.GetComponent<NetworkManager>().StopClient();
    }
}
