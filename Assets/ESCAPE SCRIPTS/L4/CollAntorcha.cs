using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CollAntorcha : NetworkBehaviour
{
    [SyncVar]
    public bool LuzEncendida;

    [SerializeField]
    private string CollOkTag;
    [SerializeField]
    private GameObject Luz;
    [SerializeField]
    private GameObject Manager_Antorchas;

    [SerializeField]
    private GameObject DecoradoDes, DecoradoAct;

    // Start is called before the first frame update
    void Start()
    {
        if (LuzEncendida)
        {
            Luz.SetActive(true);
        }
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollOkTag) && !LuzEncendida)
        {
            LuzEncendida = true;
            Luz.SetActive(true);
            Manager_Antorchas.GetComponent<ManagerAntorchas>().Refresh();

            DecoradoAct.SetActive(true);
            DecoradoDes.SetActive(false);

            RpcLuzEncendida();
        }
    }

    [ClientRpc]
    private void RpcLuzEncendida()
    {
        Luz.SetActive(true);

        DecoradoAct.SetActive(true);
        DecoradoDes.SetActive(false);
    }
}
