using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;

public class DesEnemyROCA : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    //[SerializeField]
    //private GameObject Behavior;

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
        //Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        //GetComponent<BoxCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<DisparoEnemigo>().enabled = false;
    }
}
