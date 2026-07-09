using UnityEngine;
using Mirror;

public class DisparoEnemigo : NetworkBehaviour
{
    [Header("Firing")]
    public GameObject projectilePrefab;
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
    
    // this is called on the server
    [Server]
    void Fire()
    {
        //projectileMount.localEulerAngles = new Vector3(Random.Range(0, punteria), Random.Range(0, punteria), Random.Range(0, punteria)); // PUNTERIA
        //projectileMount.localEulerAngles = new Vector3(
          //  projectileMount.localEulerAngles.x + Random.Range(0, punteria),
            //projectileMount.localEulerAngles.y + Random.Range(0, punteria),
            //projectileMount.localEulerAngles.z + Random.Range(0, punteria)); // PUNTERIA

        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile);
        fogonazoServidor();
        RpcOnFire();
    }

    void fogonazoServidor()
    {
        if (Fogonazo != null)
        {
            Fogonazo.GetComponent<ParticleSystem>().Play();
            Fogonazo.GetComponent<AudioSource>().Play();
        }
    }
    // this is ejecutado on the Enemy that fired for all observers
    [ClientRpc]
    void RpcOnFire()
    {
        if (Fogonazo != null)
        {
            Fogonazo.GetComponent<ParticleSystem>().Play();
            Fogonazo.GetComponent<AudioSource>().Play();
        }
        //animator.SetTrigger("Shoot");
    }
   
}
