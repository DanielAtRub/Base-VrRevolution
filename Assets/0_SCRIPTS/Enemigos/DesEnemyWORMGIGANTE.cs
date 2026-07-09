using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;

public class DesEnemyWORMGIGANTE : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [SerializeField]
    private GameObject Behavior, col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11;

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
        col1.GetComponent<BoxCollider>().enabled = false;
        col2.GetComponent<BoxCollider>().enabled = false;
        col3.GetComponent<BoxCollider>().enabled = false;
        col4.GetComponent<BoxCollider>().enabled = false;
        col5.GetComponent<BoxCollider>().enabled = false;
        col6.GetComponent<BoxCollider>().enabled = false;
        col7.GetComponent<BoxCollider>().enabled = false;
        col8.GetComponent<BoxCollider>().enabled = false;
        col9.GetComponent<BoxCollider>().enabled = false;
        col10.GetComponent<BoxCollider>().enabled = false;
        col11.GetComponent<BoxCollider>().enabled = false;
        GetComponent<DisparoEnemigo>().enabled = false;
    }
}
