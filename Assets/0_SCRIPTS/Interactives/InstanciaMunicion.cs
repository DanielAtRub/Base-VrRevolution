using UnityEngine;
using Mirror;

public class InstanciaMunicion : NetworkBehaviour
{
    [SerializeField]
    private GameObject municionPrefab;
    [SerializeField]
    private Transform padre;

    void Start()
    {
    }

    // this is called on the server
    [Server]
    public void instanciaMunicion(GameObject punto)
    {
        GameObject municion = Instantiate(municionPrefab, punto.transform.position, 
            punto.transform.rotation, padre);
        NetworkServer.Spawn(municion);
    }

}
