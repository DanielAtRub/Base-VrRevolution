using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;

public class DesEnemyLANZALLAMAS : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [SerializeField]
    private GameObject Behavior;
    
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
        GetComponent<DisparoLlama>().enabled = false;
    }
}
