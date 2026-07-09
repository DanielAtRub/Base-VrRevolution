using UnityEngine;
using Mirror;

public class PuñoBoss : NetworkBehaviour
{
    [SerializeField]
    private int daño = 50;
    //[SerializeField]
    //private Animator Anim;

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
    void OnTriggerEnter (Collider co)
    {
        //Debug.Log("GOLPEADO");
        // Quita vida al player
        //if (co.collider.tag == "Player" && Anim.GetInteger("ataque") != 0)
        if (co.CompareTag ("Player"))
            co.GetComponentInParent<Player>().PlayerHealth -= daño;
    }

    /*[ServerCallback]
    void OnCollisionEnter(Collision co)
    {
        Debug.Log("GOLPEADO");
        // Quita vida al player
        //if (co.collider.tag == "Player" && Anim.GetInteger("ataque") != 0)
        if (co.collider.tag == "Player")
            co.collider.GetComponentInParent<Player>().PlayerHealth -= daño;
    }*/
}
