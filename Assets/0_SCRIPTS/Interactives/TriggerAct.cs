using UnityEngine;
using Mirror;

//OBJETO EN RED NO PLAYER DEL SERVIDOR PASA INFO AL SERVIDOR Y DE ESTE A LOS CLIENTES, DE OTRO OBJETO DE RED DISTINTO

public class TriggerAct : NetworkBehaviour
{
    [SerializeField]
    private GameObject ActivarNET;
    [SyncVar]
    public bool activoNET;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        ActivarNET.transform.Translate(Vector3.forward * Time.deltaTime*0.5f);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (ActivarNET.activeInHierarchy)
            activoNET = false;
        else
            activoNET = true;
        ActivarNET.SetActive(activoNET);
        RpcActivarNET(activoNET);
    }

    [ClientRpc]
    void RpcActivarNET(bool act)
    {
        ActivarNET.SetActive(act);
    }

}
