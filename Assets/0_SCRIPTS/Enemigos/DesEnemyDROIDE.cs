using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;

public class DesEnemyDROIDE : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [SerializeField]
    private GameObject Behavior;
    [SerializeField]
    private GameObject ColCabeza;
    [SerializeField]
    private GameObject ColCuerpo;
    //[SerializeField]
    //private BoxCollider[] colision;

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
        //GetComponent<AIRoot>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        ColCabeza.SetActive(false);
        ColCuerpo.SetActive(false);

        //GetComponent<Animator>().enabled = false; // PRUEBA

        /*if (colision.Length > 0)
        {
            for (int i = 0; i < colision.Length; i++)
            {
                colision[i].enabled = false;
            }
        }*/
    }
}
