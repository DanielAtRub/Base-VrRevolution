using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
//using UnityEngine.AI;

public class DesEnemyCompTRANS : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}

    [SerializeField]
    private GameObject Behavior;

    // Start is called before the first frame update
    void Start()
    {
        if (isClientOnly)
        {
            //Behavior.SetActive(false);
            Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
            //Behavior.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            //GetComponent<NavMeshAgent>().enabled = false;
            //GetComponent<_LocomotionSimpleAgentNoRoot>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
