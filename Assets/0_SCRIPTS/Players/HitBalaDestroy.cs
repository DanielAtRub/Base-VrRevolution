using UnityEngine;
using Mirror;

public class HitBalaDestroy : NetworkBehaviour
{
    [SerializeField]
    private float destroyAfter = 1f;

    // se ejecuta cuando aparece este objeto en el servidor
    //public override void OnStartServer()
    void OnEnable()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // destroy for everyone on the server
    //[Server]
    void DestroySelf()
    {
        //NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    }

    void Start()
    {   
    }
}
