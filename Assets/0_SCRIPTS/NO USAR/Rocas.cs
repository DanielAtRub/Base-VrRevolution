using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Mirror;

public class Rocas : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [SyncVar]
    public bool isDead, destruir;
    [SerializeField]
    private bool NoDestructible;
    [SerializeField]
    private float wairForDestroy, waitForAparece;
    [SerializeField]
    private float timelifeOriginal, timelife;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private bool isDisolve;
    [SerializeField]
    private GameObject Original, ClonDisolve;

    [SerializeField]
    private bool isPool;

    [SerializeField]
    private GameObject Padre;
    private Vector3 Origen;

    [SerializeField]
    private int daño = 1;

    [ServerCallback]
    void Start()
    {
        Origen = transform.position;  //LA POSICION DE PARTIDA
    }

    [ServerCallback]
    void OnCollisionEnter(Collision co)
    {
        // Quita vida al player
        if (co.collider.tag == "Player")
        {
            co.collider.GetComponentInParent<Player>().PlayerHealth -= daño;
            muerte();
        }
    }

    [Server]
    public void Reinicio()
    {
        transform.position = Origen; //LA POSICION DE PARTIDA

        //REdisuelve
        if (isDisolve)
        {
            Original.SetActive(true);
            ClonDisolve.SetActive(false);
            RpcReDisolve();
        }

        StartCoroutine(WaitForAparece());
    }

    [Server]
    public IEnumerator WaitForAparece()
    {
        yield return new WaitForSeconds(waitForAparece);
        Padre.SetActive(true); 
        RpcSetActive(true);

        isDead = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
    
    [ClientRpc]
    void RpcReDisolve()
    {
        Original.SetActive(true);
        ClonDisolve.SetActive(false);
    }

    [Server]
    void muerte()
    {
        if (!isDead)
        {
            isDead = true;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

            //explota
            if (explosion)
            {
                explosion.Play(); //EXPLOTA EN SERVIDOR
                RpcExplota();
            }
            //Se disuelve
            if (isDisolve)
            {
                Original.SetActive(false);
                ClonDisolve.SetActive(true); //DISUELVE EN SERVIDOR
                RpcDisolve();
            }
            //llama a destruye o desactiva
            StartCoroutine(WaitForDestroy());
        }
    }
    [ClientRpc]
    void RpcExplota()
    {
        explosion.Play(); //EXPLOTA EN CLIENTES
    }
    [ClientRpc]
    void RpcDisolve()
    {
        Original.SetActive(false);
        ClonDisolve.SetActive(true);
    }

    [Server]
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(wairForDestroy);
        if (isPool)
        { 
            if (destruir)
            {
                gameObject.SetActive(false); 
                RpcSetActiveTOTAL(false);
            }
            else
            {
                Padre.SetActive(false); 
                RpcSetActive(false); 
                Reinicio();
            }
        }
        else
        {
            if (!NoDestructible)
                NetworkServer.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            timelife -= Time.deltaTime;
            if (timelife <= 0)
            {
                timelife = timelifeOriginal;
                muerte();
            }
        }
    }

    [ClientRpc]  
    void RpcSetActive(bool act)
    {
        Padre.SetActive(act);
    }
    [ClientRpc]   
    void RpcSetActiveTOTAL(bool act)
    {
        gameObject.SetActive(act);
    }

}
