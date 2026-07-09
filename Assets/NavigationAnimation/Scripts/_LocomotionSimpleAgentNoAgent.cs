using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class _LocomotionSimpleAgentNoAgent : MonoBehaviour
{
	Animator anim;
    [SerializeField]
    Vector3 velocity = Vector2.zero;
    Vector3 prevPos;
    [SerializeField]
    float Margin = 0.01f;

    void Start ()
    {
		anim = GetComponent<Animator> ();
	}

    void Update () {
        velocity = (transform.localPosition - prevPos) / Time.deltaTime;
        prevPos = transform.localPosition;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, velocity);
        float dy = Vector3.Dot(transform.forward, velocity);
        Vector2 deltaPosition = new Vector2(dx, dy);

        var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
        var rightDotProduct = Vector3.Dot(transform.right, velocity);
        Vector2 velocityVector = new Vector2(rightDotProduct, fwdDotProduct);

        bool shouldMove = velocity.magnitude > Margin;

        anim.SetBool("move", shouldMove);
        anim.SetFloat("velx", velocity.x);
        anim.SetFloat("vely", velocity.z);
    }
}
