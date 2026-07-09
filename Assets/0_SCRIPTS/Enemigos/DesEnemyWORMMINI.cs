using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;

public class DesEnemyWORMMINI : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [SerializeField]
    private GameObject Behavior, colision;

    public override void OnStartClient()
    {
        //if (isServer)
        //  servidor();
        if (isClient)
            cliente();
    }

    void Update()  //OJO CON LA POSICION DEL UPDATE, ESTAR DONDE DEBE
    {
    }
    /*
    [Server]
    void servidor()
    {
        //DESACTIVA EN EL SERVIDOR
    }
    */
    [Client]
    void cliente()
    {
        //DESACTIVA EN EL CLIENTE
        Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
        GetComponent<_LocomotionSimpleAgentNoRoot>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        colision.GetComponent<BoxCollider>().enabled = false;
        GetComponent<AtaqueEnemigo>().enabled = false;
    }
}
