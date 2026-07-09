using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class RevivePlayer : NetworkBehaviour
{
    [SerializeField]
    private TextMeshPro texDEBUG;
    [SerializeField]
    private GameObject Padre;
    private NetworkIdentity netPadre;

    // Start is called before the first frame update
    void Start()
    {
        netPadre = Padre.GetComponent<NetworkIdentity>();
        if (!netPadre.isLocalPlayer)
            enabled = false;
    }

    [ClientCallback]
    void OnTriggerEnter(Collider co)
    {
        //texDEBUG.text += " COLISION PLAYER REVIVE " + netPadre.isLocalPlayer;
        if (!netPadre.isLocalPlayer)
            return;

        if (co.CompareTag("ZonaRevive"))
            GetComponentInParent<Player>().ReviveLocal();
    }
    
}
