using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Explosivo : NetworkBehaviour
{
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private GameObject GraficoExplosivo;

    // Start is called before the first frame update
    void Start()
    { 
    }

    [Server]
    public void Explota()
    {
        GraficoExplosivo.SetActive(false);
        Explosion.SetActive(true);
        RpcExplota();
    }

    [ClientRpc]
    void RpcExplota()
    {
        GraficoExplosivo.SetActive(false);
        Explosion.SetActive(true);
    }
}
