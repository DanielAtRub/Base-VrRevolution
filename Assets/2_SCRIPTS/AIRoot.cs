using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIRoot : MonoBehaviour
{
    NavMeshAgent myAgent;
    Animator MyAnimator;
    Vector3 worldDeltaPosition;

    [SerializeField]
    private float offset = 0.9f;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        MyAnimator = GetComponent<Animator>();
        myAgent.updatePosition = false;
    }

    private void Update()
    {
        worldDeltaPosition = myAgent.nextPosition - transform.position;

        if (worldDeltaPosition.magnitude > myAgent.radius)
            myAgent.nextPosition = transform.position + offset * worldDeltaPosition;
    }
}
