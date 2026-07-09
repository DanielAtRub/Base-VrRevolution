using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class ExplosionPhysics : MonoBehaviour
{
    [SerializeField]
    private float radius = 5.0F;
    [SerializeField]
    private float power = 10.0F;
    [SerializeField]
    private Collider[] colliders;

    void Start()
    {
    }

    void OnEnable()
    {
        Vector3 explosionPos = transform.position;
        colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }
}