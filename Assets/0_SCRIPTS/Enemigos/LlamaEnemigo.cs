using UnityEngine;
using Mirror;

public class LlamaEnemigo : NetworkBehaviour
{
    [SerializeField]
    private int daño = 30;
   
    void Start()
    {
        if (isClientOnly)//PRUEBA
            desColider();
    }
    
    //[Client]
    private void desColider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    [ServerCallback]
    void OnTriggerEnter(Collider co)
    {
        // Quita vida al player
        if (co.tag == "Player")
            co.GetComponentInParent<Player>().PlayerHealth -= daño;
    }
}
