using UnityEngine;
using Mirror;

public class BalaTrazadora : NetworkBehaviour
{
    [SerializeField]
    private float destroyAfter = 1;
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private float velocidad = 1000;
    /*
    // se ejecuta cuando aparece este objeto en el servidor
    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }
    */
    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        Invoke(nameof(DestroySelf), destroyAfter);

        if (isClientOnly)
            GetComponent<BoxCollider>().enabled = false;

        rigidBody.AddForce(transform.forward * velocidad);
    }
   
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
    
    [ServerCallback]
    void OnCollisionEnter(Collision co)
    {
        NetworkServer.Destroy(gameObject);
    }
}
