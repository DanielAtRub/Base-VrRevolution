using UnityEngine;
using Mirror;

public class AimScript : NetworkBehaviour
{
    [SyncVar]
    public GameObject target;
    [SerializeField]
    private GameObject behavior;
    public Transform bone;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (isServerOnly)
            target = behavior.GetComponent<GetTarget>().N_target;

        if (target)
        {
            //GIRA EL HUESO
            bone.LookAt(target.transform.position);
            bone.localRotation = bone.localRotation * Quaternion.Euler(offset);
            //GIRA EL PERSONAJE SOLO EN EL EJE Y
            Vector3 targetPostition = new Vector3(target.transform.position.x, transform.position.y,
                target.transform.position.z);
            transform.LookAt(targetPostition);
        }
    }
  
 }
