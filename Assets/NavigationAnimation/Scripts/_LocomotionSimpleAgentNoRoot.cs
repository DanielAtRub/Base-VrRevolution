using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class _LocomotionSimpleAgentNoRoot : MonoBehaviour
{
	Animator anim;
	NavMeshAgent agent;
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;
    bool disparando;
    [SerializeField]
    float radiusMargin = 0f;

    [SerializeField]
    float TiempoImpacto = 0.6f;

    [SerializeField]
    float AgentSpeed = 1;

    void Start ()
    {
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		agent.updatePosition = false;
	}
	
	void Update () {
        if (agent.isActiveAndEnabled)
        {
            Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

            // Map 'worldDeltaPosition' to local space
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            // Low-pass filter the deltaMove
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f); //0.15f
            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

            // Update velocity if delta time is safe
            if (Time.deltaTime > 1e-5f)
                velocity = smoothDeltaPosition / Time.deltaTime;

            bool shouldMove = velocity.magnitude > 0.01f && agent.remainingDistance >
                (agent.radius + radiusMargin); //0.1f

            //if (shouldMove) disparando = false; //PRUEBA
            //bool disparando = !shouldMove; //PRUEBA

            // Update animation parameters
            //anim.SetBool("disparando", disparando);  //PRUEBA
            //anim.SetInteger("ataque", 0);  //PRUEBA
            anim.SetBool("move", shouldMove);
            anim.SetFloat("velx", velocity.x);
            anim.SetFloat("vely", velocity.y);

            //_LookAt lookAt = GetComponent<_LookAt> ();
            //if (lookAt)
            //	lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

            //		// Pull character towards agent
            //		if (worldDeltaPosition.magnitude > agent.radius)
            //			transform.position = agent.nextPosition - 0.9f*worldDeltaPosition;

            //		// Pull agent towards character
            //		if (worldDeltaPosition.magnitude > agent.radius)
            //			agent.nextPosition = transform.position + 0.9f*worldDeltaPosition;

            Vector3 position = agent.nextPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;

            /*if (anim.GetBool("impacto")) //SE PARA SI SE ACTIVA LA ANIMACIÓN DE IMPACTO
            {
                agent.speed = 0;
                StartCoroutine(WaitForImpacto());
            }*/
        }
    }
    /*private IEnumerator WaitForImpacto()
    {
        yield return new WaitForSeconds(TiempoImpacto);
        agent.speed = AgentSpeed;
    }*/
    /*
    void OnAnimatorMove () {
        // Update postion to agent position
        //transform.position = agent.nextPosition;

        // Update position based on animation movement using navigation surface height
        //Vector3 position = anim.rootPosition;

        Vector3 position = agent.nextPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }
    */
}
