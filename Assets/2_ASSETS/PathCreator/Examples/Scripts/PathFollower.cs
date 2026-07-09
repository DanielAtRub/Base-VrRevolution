using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        [SerializeField]
        private float distanceTravelled;

        public float a;
        Quaternion rot;

        private Vector3 pos;
        public float offsetY;

        [SerializeField]
        private bool isPlataforma;

        [SerializeField]
        private float DesDistance;
        [SerializeField]
        private GameObject GameManager;

        void Start() {
           /*if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }*/
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;

                pos = pathCreator.path.GetPointAtDistance(a + distanceTravelled, endOfPathInstruction);
                pos.y += offsetY;
                //transform.position = pathCreator.path.GetPointAtDistance(a + distanceTravelled, endOfPathInstruction);
                transform.position = pos;

                if (!isPlataforma)
                {
                    rot = pathCreator.path.GetRotationAtDistance(a + distanceTravelled, endOfPathInstruction);
                    rot.x = 0;
                    rot.z = 0;
                    transform.rotation = rot;
                }

                if (distanceTravelled > DesDistance)
                {
                    GameManager.GetComponent<JuegoManager>().ActivaUbicaL11();
                    this.enabled = false;
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}