using UnityEngine;
using System.Collections;
using Mirror;

public class Roca : NetworkBehaviour
{
    [SerializeField]
    private float destroyAfter, wairForDestroy;
    //public Rigidbody rigidBody;
    public int daño = 1;
    [SerializeField]
    private bool NoDestroyCol;
    [SerializeField]
    private bool isDisolve;
    [SerializeField]
    private GameObject Original, ClonDisolve;
    [SerializeField]
    private AudioSource SonidoRoca;
    [SerializeField]
    private bool trig, trig2;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    [ServerCallback]
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(100f, 100f, 100f);
    }
    
    [Server]
    void DestroySelf()
    {
        //Se disuelve
        if (isDisolve)
        {
            Original.SetActive(false);
            ClonDisolve.SetActive(true);
            RpcDisolve();
        }
        StartCoroutine(WaitForDestroy());
    }
    [Server]
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(wairForDestroy);
        NetworkServer.Destroy(gameObject);
    }

    [ServerCallback]
    void OnTriggerEnter(Collider co)
    {
        // Quita vida al player
        if (!trig && co.tag == "Player")
        {
            co.GetComponentInParent<Player>().PlayerHealth -= daño;
            trig = true;
        }
        if (!NoDestroyCol)
            DestroySelf();
    }

    [ServerCallback]
    void OnCollisionEnter(Collision co)
    {
        if (!trig2)
        {
            SonidoRoca.Play();
            RpcSonidoRoca();
            trig2 = true;
        }
    }

    [ClientRpc]
    void RpcSonidoRoca()
    {
        SonidoRoca.Play();
    }

    [ClientRpc]
    void RpcDisolve()
    {
        Original.SetActive(false);
        ClonDisolve.SetActive(true);
    }
    
}
