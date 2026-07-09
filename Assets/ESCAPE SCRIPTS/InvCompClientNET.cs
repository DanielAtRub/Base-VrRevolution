using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class InvCompClientNET : NetworkBehaviour
{
    /*[SerializeField]
    private GameObject RicBody;
    [SerializeField]
    private SphereCollider[] SphereColision;*/

    [SerializeField]
    private NetworkIdentity Padre;

    public UnityEvent Componente;

    // Start is called before the first frame update
    void Start()
    {
        if (isClient || Padre.isClient)
            client();
    }

    [Client]
    void client()
    {
        //DESACTIVA EN EL CLIENTE
        /*if (RicBody)
            RicBody.GetComponent<Rigidbody>().isKinematic = true;
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
