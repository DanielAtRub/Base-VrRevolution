using UnityEngine;
using Mirror;

public class BalaPlayer : NetworkBehaviour
{
    [SerializeField]
    private float destroyAfter = 5;
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private float force = 1000;
    [SerializeField]
    private int pointsM, pointsI;
    [SerializeField]
    private int daño = 1;
    public GameObject PlayerPropietario; // Recibe el Gameobject del Player que la disparo

    public GameObject hitPrefab;
    private GameObject impact;

    // se ejecuta cuando aparece este objeto en el servidor
    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        if (isClientOnly)
            GetComponent<BoxCollider>().enabled = false;

        rigidBody.AddForce(transform.forward * force);
    }
    
    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    [ServerCallback]
    void OnCollisionEnter(Collision co)
    {
        //EFECTO COLISION
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (hitPrefab != null)
        {
            impact = Instantiate(hitPrefab, pos, rot);
            NetworkServer.Spawn(impact);
        }

        // Quita vida al enemigo
        if (co.collider.tag == "PartEnemy")
        {
            co.collider.GetComponentInParent<Enemigo0>().impacto();
            if (!co.collider.GetComponentInParent<Enemigo0>().isDead)
            {

                //DESMIEMBRA
                //co.collider.GetComponent<ParteCuerpoEnemy>().Desmiembra();
                //DESMIEMBRA

                co.collider.GetComponentInParent<Enemigo0>().EnemyHealth -= daño;
                //co.collider.GetComponentInParent<Enemigo>().ActiveCarameloServer(); //ACTUALIZA EN SERVER
                if (co.collider.GetComponentInParent<Enemigo0>().EnemyHealth <= 0)
                {
                    PlayerPropietario.GetComponentInParent<Player>().PlayerPuntos += pointsM;
                    PlayerPropietario.GetComponentInParent<Player>().EnemigosMatados++;
                }
                else
                {
                    PlayerPropietario.GetComponentInParent<Player>().PlayerPuntos += pointsI;
                }
            }
        }
        if (co.collider.tag == "Enemy")
        {
            co.collider.GetComponentInParent<Enemigo0>().impacto();
            if (!co.collider.GetComponentInParent<Enemigo0>().isDead)
            {
                co.collider.GetComponentInParent<Enemigo0>().EnemyHealth -= daño;
                //co.collider.GetComponentInParent<Enemigo>().ActiveCarameloServer(); //ACTUALIZA EN SERVER
                if (co.collider.GetComponentInParent<Enemigo0>().EnemyHealth <= 0)
                {
                    PlayerPropietario.GetComponentInParent<Player>().PlayerPuntos += pointsM;
                    PlayerPropietario.GetComponentInParent<Player>().EnemigosMatados++;
                }
                else
                {
                    PlayerPropietario.GetComponentInParent<Player>().PlayerPuntos += pointsI;
                }
            }
        }

        NetworkServer.Destroy(gameObject);
    }
    
    [ClientRpc]
    void RpcSetImpact(Vector3 impactPos, Quaternion impactRot, GameObject targetID) 
    {
        impact = Instantiate(hitPrefab, impactPos, impactRot);
        impact.transform.SetParent(targetID.transform);
    }
    
}
