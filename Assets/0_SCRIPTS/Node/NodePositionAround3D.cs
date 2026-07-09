using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
//using NavMeshAgent = UnityEngine.AI.NavMeshAgent;

namespace NodeCanvas.Tasks.Actions
{
    public class NodePositionAround3D : ActionTask<Transform>
    {
        public BBParameter<GameObject> target;
        public BBParameter<float> margen, rango;
        public BBParameter<float> speed = 1;
        public BBParameter<float> keepDistance = 0.1f;
        //public BBParameter<float> timeToWait;
        public float offsetY;
        public Vector3 newPos;
        public bool waitActionFinish;

        //private Vector3? lastRequest;

        protected override void OnExecute()
        {
            float multX = Random.Range(0f, 2f);
            if (multX <= 1f) multX = -1f;
            else multX = 1f;
            float multY = Random.Range(0f, 2f);
            if (multY <= 1f) multY = -1f;
            else multY = 1f;
            float multZ = Random.Range(0f, 2f);
            if (multZ <= 1f) multZ = -1f;
            else multZ = 1f;

            float x = Random.Range(margen.value, rango.value) * multX;
            float y = Random.Range(margen.value, rango.value) * multY;
            float z = Random.Range(margen.value, rango.value) * multZ;
           
            newPos = new Vector3(target.value.transform.position.x + x,
                target.value.transform.position.y + y + offsetY, target.value.transform.position.z + z);
        }

        protected override void OnUpdate()
        {

            if ((agent.position - newPos).magnitude <= keepDistance.value)
            {
                EndAction();
                return;
            }

            //lastRequest = newPos;

            //agent.position = Vector3.SmoothDamp(agent.position, target.value.transform.position, ref velocity, 2f);

            agent.position = Vector3.MoveTowards(agent.position, newPos, speed.value * Time.deltaTime);
            if (!waitActionFinish)
            {
                EndAction(true);
            }
            /*
            if (elapsedTime > timeToWait.value)
                EndAction(true);
            */
        }
        
        public override void OnDrawGizmos()
        {
            if (target.value != null)
            {
                Gizmos.color = Color.blue;

                Gizmos.DrawSphere(newPos, 0.2f);

                Vector3 GizmonewPos = new Vector3(target.value.transform.position.x,
                target.value.transform.position.y + offsetY, target.value.transform.position.z);

                Gizmos.DrawWireCube(GizmonewPos, new Vector3(margen.value*2, margen.value * 2, margen.value*2));
                Gizmos.DrawWireCube(GizmonewPos, new Vector3(rango.value*2, rango.value * 2, rango.value*2));
                Gizmos.color = Color.white;
            }
        }
        
    }
}