using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using NavMeshAgent = UnityEngine.AI.NavMeshAgent;

namespace NodeCanvas.Tasks.Actions
{
    public class NodePositionAround : ActionTask<NavMeshAgent>
    {
        public BBParameter<GameObject> target;
        public BBParameter<float> margen, rango;
        public BBParameter<float> speed = 1;
        public BBParameter<float> keepDistance = 0.1f;
        public Vector3 newPos;
        
        private Vector3? lastRequest;

        protected override void OnExecute()
        {
            agent.speed = speed.value;

            float multX = Random.Range(0f, 2f);
            if (multX <= 1f) multX = -1f;
            else multX = 1f;
            float multZ = Random.Range(0f, 2f);
            if (multZ <= 1f) multZ = -1f;
            else multZ = 1f;

            float x = Random.Range(margen.value, rango.value) * multX;
            float z = Random.Range(margen.value, rango.value) * multZ;
            
            newPos = new Vector3(target.value.transform.position.x + x,
                target.value.transform.position.y, target.value.transform.position.z + z);
            
            //if (elapsedTime > timeToWait.value)
            //EndAction(true);
        }

        protected override void OnUpdate()
        {
            if (lastRequest != newPos)
            {
                if (!agent.SetDestination(newPos))
                {
                    EndAction(false);
                    return;
                }
            }

            lastRequest = newPos;
            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + keepDistance.value)
            {
                EndAction(true);
            }
            /*
            agent.position = Vector3.MoveTowards(agent.position, newPos, speed.value * Time.deltaTime);
            if (!waitActionFinish)
            {
                EndAction();
            }
            */
        }

        protected override void OnStop()
        {
            if (lastRequest != null && agent.gameObject.activeSelf)
            {
                agent.ResetPath();
            }
            lastRequest = null;
        }

        protected override void OnPause()
        {
            OnStop();
        }

        public override void OnDrawGizmos()
        {
            if (target.value != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(target.value.transform.position, new Vector3(margen.value*2, 0.1f, margen.value*2));
                Gizmos.DrawWireCube(target.value.transform.position, new Vector3(rango.value*2, 0.1f, rango.value*2));
                Gizmos.color = Color.white;
            }
        }
        
    }
}