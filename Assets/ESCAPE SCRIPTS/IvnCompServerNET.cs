using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class IvnCompServerNET : NetworkBehaviour
{
    /*[SerializeField]
    private GameObject RicBody;
    [SerializeField]
    private SphereCollider[] SphereColision;*/

    //[SerializeField]
    //private NetworkIdentity Padre;

    public UnityEvent Componente;

    // Start is called before the first frame update
    public override void OnStartServer()
    {
        //if (isServer /*|| Padre.isServer*/)
            server();
    }

    [Server]
    void server()
    {
        //DES/ACT EN EL SERVIDOR
        /*if (RicBody)
        {
            RicBody.GetComponent<Rigidbody>().isKinematic = false;
            RicBody.GetComponent<Rigidbody>().useGravity = true;
        }
        if (SphereColision.Length>0)
        {
            for (int i = 0; i < SphereColision.Length; i++)
            {
                SphereColision[i].enabled = false;
            }
        }*/

        // Call event
        if (Componente != null)
        {
            Componente.Invoke();
        }
    }
}
