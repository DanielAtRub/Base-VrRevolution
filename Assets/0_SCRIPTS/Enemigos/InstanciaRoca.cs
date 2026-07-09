using UnityEngine;
using Mirror;

public class InstanciaRoca : NetworkBehaviour
{
    [SerializeField]
    private GameObject rocaPrefab, behavior;
    [SerializeField]
    private Transform rocaPosition;
    [SerializeField]
    private Vector3 rocaPositionOriginal;
    [SerializeField]
    private float rangoX, rangoZ;
    //public bool OrdaKilled;

    void Start()
    {
    }

    // this is called on the server
    [Server]
    public void Fire()
    {
        float x = Random.Range(-rangoX, rangoX);
        float z = Random.Range(-rangoZ, rangoZ);
        rocaPosition.position = new Vector3(rocaPositionOriginal.x + x, 
            rocaPositionOriginal.y, rocaPositionOriginal.z + z);
        GameObject projectile = Instantiate(rocaPrefab, rocaPosition.position, rocaPosition.rotation);

        projectile.transform.localScale = new Vector3(Random.Range(0.05f, 0.2f),
            Random.Range(0.1f, 0.5f),
            Random.Range(0.1f, 0.5f));
        //projectile.GetComponent<Rigidbody>().AddTorque(100f, 100f, 100f);

        NetworkServer.Spawn(projectile);
    }

}
