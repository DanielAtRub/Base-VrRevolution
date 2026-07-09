using UnityEngine;
using Mirror;

public class DisparoGusano : NetworkBehaviour
{
    [Header("Firing")]
    public GameObject projectilePrefab;
    public GameObject projectilePrefabPlus;
    public Transform projectileMount;
    [Header("0-Exacta, 10-Mala")]
    public int punteria;
    public GameObject Fogonazo;

    void Start()
    {    
    }

    [Server]
    public void Dispara()
    {
        Fire();
    }
    [Server]
    public void DisparaPlus()
    {
        FirePlus();
    }

    // this is called on the server
    [Server]
    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile);
        fogonazoServidor();
        RpcOnFire();
    }
    [Server]
    void FirePlus()
    {
        GameObject projectile = Instantiate(projectilePrefabPlus, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile);
        fogonazoServidor();
        RpcOnFire();
    }

    void fogonazoServidor()
    {
        if (Fogonazo != null)
            Fogonazo.GetComponent<ParticleSystem>().Play();
    }
    // this is ejecutado on the Enemy that fired for all observers
    [ClientRpc]
    void RpcOnFire()
    {
        if (Fogonazo != null)
            Fogonazo.GetComponent<ParticleSystem>().Play();
    }
   
}
