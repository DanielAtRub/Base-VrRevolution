using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MandoCliente : NetworkBehaviour
{
    [SerializeField]
    private bool Disparando;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        // shoot
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            if (!Disparando)
            {
                Disparando = true; //SOLO 1 VEZ HASTA QUE NO SE SUELTE EL GATILLO
                //GetComponent<DisparoPlayer>().Disparando = Disparando;
                CmdServer(Disparando);
            }
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            if (Disparando)
            {
                Disparando = false; //SOLO 1 VEZ
                //GetComponent<DisparoPlayer>().Disparando = Disparando;
                CmdServer(Disparando);
            }
        } 
    }

    [Command]
    void CmdServer(bool Dis)
    {
        //GetComponent<DisparoHitPlayer>().Disparando = Dis;
        Debug.Log(Dis);
    }

}
