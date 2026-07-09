using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;

public class DesEnemyASPAS : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [SerializeField]
    private GameObject Behavior;
    [SerializeField]
    private BoxCollider[] colision;

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
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AspasEnemigo>().enabled = false;
        for (int i = 0; i < colision.Length; i++)
        {
            colision[i].enabled = false;
        }
    }
}
