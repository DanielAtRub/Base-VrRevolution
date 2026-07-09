using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using NodeCanvas.BehaviourTrees;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class Enemigo : NetworkBehaviour
{
    [SyncVar]
    public bool isDead, destruir;
    [SyncVar(hook = nameof(SetEnemyVida))]
    public int EnemyHealth = 100;
    [SerializeField]
    private GameObject behavior, anim;
    [SerializeField]
    private ParticleSystem impactoExplosion;
    //[SerializeField]
    //private bool isDron;
    [SerializeField]
    private bool NoDestructible;
    [SerializeField]
    private float wairForDestroy, waitForAparece;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private AudioSource SonidoExplosion;
    /*[SerializeField]
    private GameObject ItemPrefab;
    private GameObject Item;*/

    //[SerializeField]
    //private bool isPool;

    //[SerializeField]
    //private GameObject Padre;
    private Vector3 Origen;
    [SyncVar]
    private int OriginaVida;

    [SerializeField]
    private AudioSource SonidoMovimiento;

    //[SerializeField]
    //private GameObject[] Caramelo;

    [SerializeField]
    private TextMeshProUGUI textVida;

    /*[SerializeField]
    private GameObject[] PuntosInstancia;
    [SerializeField]
    private string TagPuntosInstancia;*/

    //[SerializeField]
    //private bool isPiruleta;

    /*[SerializeField]
    private GameObject puntosImpactoPrefab;
    [SerializeField]
    private GameObject puntosMuertePrefab;
    [SerializeField]
    private Transform puntosPos;

    [SerializeField]
    private GameObject Ragdoll;*/

    [SerializeField]
    private GameObject ColCabeza;
    [SerializeField]
    private GameObject ColCuerpo;

    //[SerializeField]
    //private GameObject Manager;

    [ServerCallback]
    void Start()
    {
        //Manager = GameObject.Find("GameManager");

        /*if (isPiruleta)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        }*/
        /*if (TagPuntosInstancia != "")
        {
            PuntosInstancia = GameObject.FindGameObjectsWithTag(TagPuntosInstancia);
            transform.position = PuntosInstancia[Random.Range(0, PuntosInstancia.Length)].transform.position; //POSICION ALEATORIA ENTRE PUNTOS
        }
        else*/
            Origen = transform.localPosition;  //LA POSICION DE PARTIDA
        OriginaVida = EnemyHealth;
    }

    /*[Server]
    public void ActNavMesh()  //LLAMADO DESDE BEHAVIOUR
    {
        if (isPiruleta)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }*/

    [Server]
    public void Reinicio()
    {
        /*Ragdoll.GetComponent<RagdollToggle>().DisableRagDoll(); //REINICIA RAGDOLL SERVER
        RpcDisableRagdoll();*/

        /*if (TagPuntosInstancia != "")
            transform.position = PuntosInstancia[Random.Range(0, PuntosInstancia.Length)].transform.position; //POSICION ALEATORIA ENTRE PUNTOS
        else*/
            transform.localPosition = Origen; //LA POSICION DE PARTIDA
        isDead = false;
        EnemyHealth = OriginaVida;
        //activa Col
        ColCabeza.SetActive(true);
        ColCuerpo.SetActive(true);
        //activa la IA
        if (GetComponent<_LocomotionSimpleAgentNoRoot>())
            GetComponent<_LocomotionSimpleAgentNoRoot>().enabled = true;
        behavior.GetComponent<BehaviourTreeOwner>().enabled = true;
        if (GetComponent<NavMeshAgent>())
            GetComponent<NavMeshAgent>().enabled = true;
        //activa Ain anim
        if (GetComponent<AimAnimScript>())
        {
            GetComponent<AimAnimScript>().enabled = true;
            RpcReAim();
        }
        //animacion muerte
        if (anim)
            anim.GetComponent<Animator>().Rebind();
            //anim.GetComponent<Animator>().SetBool("muerte", false);
        //Cae al suelo si es Dron
        /*if (isDron)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }*/
        //Desactiva Caramelos
        /*for (int i = 0; i < Caramelo.Length; i++)
        {
            Caramelo[i].SetActive(false);
            RpcDesActiveCaramelo(i);
        }*/

        //StartCoroutine(WaitForAparece());
    }

    /*[Server]
    public IEnumerator WaitForAparece()
    {
        yield return new WaitForSeconds(waitForAparece);
        Padre.SetActive(true);
        RpcSetActive(true);
    }*/
    
    [ClientRpc]
    void RpcReAim()
    {
        GetComponent<AimAnimScript>().enabled = true;
    }
    [Server]
    public void impacto()
    {
        // INSTANCIA PUNTOS IMPACTO
        //GameObject puntosImpacto = Instantiate(puntosImpactoPrefab, puntosPos.position, puntosPos.rotation);
        //NetworkServer.Spawn(puntosImpacto);

        if (anim)
        {
            anim.GetComponent<Animator>().SetTrigger("impacto");
            GetComponent<NetworkAnimator>().SetTrigger("impacto");
        }
        if (impactoExplosion)
        {
            impactoExplosion.Play();
            RpcImpactoExplosion();
        }
    }

    [Server]
    public void sonidoMovimiento()
    {
        if (SonidoMovimiento)
        {
            SonidoMovimiento.Play();
            RpcSonidoMovimiento();
        }
    }
    [ClientRpc]
    void RpcSonidoMovimiento()
    {
        SonidoMovimiento.Play();
    }

    [Server]
    void muerte()
    {
        if (!isDead)
        {
            // INSTANCIA PUNTOS MUERTE
            //GameObject puntosMuerte = Instantiate(puntosMuertePrefab, puntosPos.position, puntosPos.rotation);
            //NetworkServer.Spawn(puntosMuerte);

            /*Ragdoll.GetComponent<RagdollToggle>().EnableRagDoll(); //ACTIVA RAGDOLL SERVER
            RpcRagdoll();*/

            isDead = true;
            //desactiva Col
            ColCabeza.SetActive(false);
            ColCuerpo.SetActive(false);
            //desactiva la IA
            if (GetComponent<_LocomotionSimpleAgentNoRoot>())
                GetComponent<_LocomotionSimpleAgentNoRoot>().enabled = false;
            behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
            if (GetComponent<NavMeshAgent>())
                GetComponent<NavMeshAgent>().enabled = false;
            //desactiva Ain anim
            /*if (GetComponent<AimAnimScript>())
            {
                GetComponent<AimAnimScript>().enabled = false;
                RpcAim();
            }*/
            //animacion muerte
            if (anim)
                anim.GetComponent<Animator>().Rebind();
                //anim.GetComponent<Animator>().SetBool("muerte", true);
                //explota
            if (explosion)
            {
                //explosion.Play(); //EXPLOTA EN SERVIDOR
                if (SonidoExplosion) SonidoExplosion.Play();
                RpcExplota();
            }
            //Cae al suelo si es Dron
            //if (isDron)
                //GetComponent<Rigidbody>().useGravity = true;
            //llama a destruye o desactiva
            StartCoroutine(WaitForDestroy());
        }
    }
    /*[ClientRpc]
    void RpcRagdoll()
    {
        Ragdoll.GetComponent<RagdollToggle>().EnableRagDoll(); //ACTIVA RAGDOLL CLIENTES
    }
    [ClientRpc]
    void RpcDisableRagdoll()
    {
        Ragdoll.GetComponent<RagdollToggle>().DisableRagDoll(); //DESACTIVA RAGDOLL CLIENTES
    }*/
    [ClientRpc]
    void RpcExplota()
    {
        //explosion.Play(); //EXPLOTA EN CLIENTES
        if (SonidoExplosion) SonidoExplosion.Play();
    }
    /*[ClientRpc]
    void RpcAim()
    {
        GetComponent<AimAnimScript>().enabled = false;
    }*/
    [ClientRpc]
    void RpcImpactoExplosion()
    {
        impactoExplosion.Play();
    }

    [Server]
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(wairForDestroy);
        //ITEM POWER
        /*if (ItemPrefab)
        {
            Item = Instantiate(ItemPrefab, transform.localPosition, Quaternion.identity);
            Item.transform.position = new Vector3(Item.transform.position.x, 0f, Item.transform.position.z);
            NetworkServer.Spawn(Item);
        }*/
        /*if (isPool)
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
        else*/
            if (!NoDestructible)
                NetworkServer.Destroy(gameObject);
    }

    /*[ClientRpc]   
    void RpcSetActive(bool act)
    {
        Padre.SetActive(act);
    }*/
    [ClientRpc]   
    void RpcSetActiveTOTAL(bool act)
    {
        gameObject.SetActive(act);
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {
        if (EnemyHealth <= 0)
        {
            EnemyHealth = 0;
            muerte();
        }
    }

    //FUNCIONES HOOK - LO HACE SOBRE TODOS LOS CLIENTES
    void SetEnemyVida(int oldEnemyVida, int newEnemyVida)
    {
        textVida.text = newEnemyVida.ToString();
        //CARAMELIZA
        /*for (int i = 0; i < Caramelo.Length - newEnemyVida; i++)
        {
            Caramelo[i].SetActive(true);
        }*/
    }

    //LO LLAMA BALAPLAYER PARA QUE SE EJECUTE EN EL SERVIDOR YA QUE LA HOOK NO LO HACE
    /*[Server]
    public void ActiveCarameloServer() 
    {
        textVida.text = EnemyHealth.ToString();
        //CARAMELIZA
        for (int i = 0; i < (Caramelo.Length - EnemyHealth); i++)
        {
            Caramelo[i].SetActive(true);
        }
    }

    //LAMADO DESDE REINICIO (AQUI) DESACTIVA CARAMELOS
    [ClientRpc]
    void RpcDesActiveCaramelo(int n)
    {
        Caramelo[n].SetActive(false);
    }*/
}
