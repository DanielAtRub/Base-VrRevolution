using UnityEngine;
using Mirror;

public class BalaEnemigo : NetworkBehaviour
{
    public float destroyAfter = 5;
    public Rigidbody rigidBody;
    public float force = 1000;
    public int daño = 1;
    public GameObject hitPrefab;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        if (isClientOnly)//PRUEBA
            desColider();

        rigidBody.AddForce(transform.forward * force);
    }
    
    //[Client]
    private void desColider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    
    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    // ServerCallback because we don't want a warning if OnTriggerEnter is
    // called on the client
    [ServerCallback]
    void OnCollisionEnter(Collision co)
    {
        //EFECTO COLISION
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            NetworkServer.Spawn(hitVFX);
        }

        // Quita vida al player
        if (co.collider.tag == "Player")
        {
            co.collider.GetComponentInParent<Player>().PlayerHealth -= daño;
            //co.collider.GetComponentInParent<Player>().FadeIn(); //PRUEBA
        }
        NetworkServer.Destroy(gameObject);
    }
}
