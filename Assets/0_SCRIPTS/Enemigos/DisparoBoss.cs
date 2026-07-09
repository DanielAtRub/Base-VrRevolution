using UnityEngine;
using Mirror;

public class DisparoBoss : NetworkBehaviour
{
    [Header("Firing")]
    public GameObject projectilePrefab;
    public Transform projectileMount1, projectileMount2, projectileMount3;
    public GameObject Fogonazo1, Fogonazo2, Fogonazo3;

    void Start()
    {    
    }

    [Server]
    public void Dispara()
    {
        Fire();
    }
    
    // this is called on the server
    [Server]
    void Fire()
    {
        GameObject projectile1 = Instantiate(projectilePrefab, projectileMount1.position, projectileMount1.rotation);
        NetworkServer.Spawn(projectile1);
        GameObject projectile2 = Instantiate(projectilePrefab, projectileMount2.position, projectileMount2.rotation);
        NetworkServer.Spawn(projectile2);
        GameObject projectile3 = Instantiate(projectilePrefab, projectileMount3.position, projectileMount3.rotation);
        NetworkServer.Spawn(projectile3);
        fogonazoServidor();
        RpcOnFire();
    }

    void fogonazoServidor()
    {
        Fogonazo1.GetComponent<ParticleSystem>().Play();
        Fogonazo2.GetComponent<ParticleSystem>().Play();
        Fogonazo3.GetComponent<ParticleSystem>().Play();
    }
    // this is ejecutado on the Enemy that fired for all observers
    [ClientRpc]
    void RpcOnFire()
    {
        Fogonazo1.GetComponent<ParticleSystem>().Play();
        Fogonazo2.GetComponent<ParticleSystem>().Play();
        Fogonazo3.GetComponent<ParticleSystem>().Play();
    }
   
}
