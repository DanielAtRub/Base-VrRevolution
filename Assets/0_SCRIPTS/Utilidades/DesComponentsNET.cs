using UnityEngine;
using NodeCanvas.BehaviourTrees;
using UnityEngine.AI;
using Mirror;

public class DesComponentsNET : NetworkBehaviour
{
    //[SerializeField]
    //private bool DesInClient;
    [SerializeField]
    private GameObject Behavior;
    [SerializeField]
    private GameObject RicBody;
    [SerializeField]
    private GameObject NavMesh;
    [SerializeField]
    private BoxCollider[] colision;

    // Start is called before the first frame update
    void Start()
    {
        if (isClient)
            cliente();
    }

    [Client]
    void cliente()
    {
        //DESACTIVA EN EL CLIENTE
        if (Behavior)
            Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
        if (NavMesh)
            NavMesh.GetComponent<NavMeshAgent>().enabled = false;
        if (RicBody)
            RicBody.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < colision.Length; i++)
        {
            colision[i].enabled = false;
        }
    }
}
