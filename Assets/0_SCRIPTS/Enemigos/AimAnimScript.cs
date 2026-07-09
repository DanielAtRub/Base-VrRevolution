using UnityEngine;
using Mirror;

public class AimAnimScript : NetworkBehaviour
{
    [SyncVar]
    public Vector3 target;
    [SerializeField]
    private GameObject behavior;
    //public Transform bone;
    public Vector3 offset;
    public Animator anim;
    [SerializeField]
    private float maxRotateY = 0.3f;
    public bool ikActive;
    private Quaternion newRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnAnimatorIK()
    {
        if (!anim)
        {
            return;
        }

        if (ikActive)
        {
            if (isServerOnly)
            {
                if (behavior.GetComponent<GetTarget>().N_target)
                    target = behavior.GetComponent<GetTarget>().N_target.transform.position;
            }

            //lookObj is the Player camera
            if (target != null)
            {
                //anim.SetLookAtWeight(1);
                Transform spineTransform = anim.GetBoneTransform(HumanBodyBones.Spine);
                spineTransform.LookAt(target);
                //spineTransform.LookAt(target.transform.position);
                newRotation = spineTransform.localRotation * Quaternion.Euler(offset);
                if (newRotation.x < maxRotateY && newRotation.x > -maxRotateY)
                    anim.SetBoneLocalRotation(HumanBodyBones.Spine, newRotation);
            }
            //else
                //anim.SetLookAtWeight(0);
        }
    }

}
