using UnityEngine;
using Mirror;

public class BalaEnemigoPlasmaPlus : NetworkBehaviour
{
    public float destroyAfter = 5;
    public Rigidbody rigidBody;
    public float force = 1000;
    public int daño = 1;
    [SerializeField]
    private bool NoDestroyCol;
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
    void OnTriggerEnter(Collider co)
    {
        // Quita vida al player
        if (co.tag == "Player")
              co.GetComponentInParent<Player>().PlayerHealth -= daño;
        if (!NoDestroyCol)
            NetworkServer.Destroy(gameObject);
    }
    /*
    [ServerCallback]
    void OnCollisionEnter (Collision co)
    {
        // Quita vida al player
        if (co.collider.tag == "Player")
            co.collider.GetComponentInParent<Player>().PlayerHealth -= daño;
        if (!NoDestroyCol)
            NetworkServer.Destroy(gameObject);
    }
    */
}
