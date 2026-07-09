using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using NodeCanvas.BehaviourTrees;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class EnemigoPiruleta : NetworkBehaviour
{
    [SyncVar]
    public bool isDead, destruir;
    [SyncVar(hook = nameof(SetEnemyVida))]
    public int EnemyHealth = 100;
    [SerializeField]
    private GameObject behavior, anim;
    [SerializeField]
    private ParticleSystem impactoExplosion;
    [SerializeField]
    private bool isDron;
    [SerializeField]
    private bool NoDestructible;
    [SerializeField]
    private float wairForDestroy, waitForAparece;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private AudioSource SonidoExplosion;
    [SerializeField]
    private GameObject ItemPrefab;
    private GameObject Item;

    [SerializeField]
    private bool isPool;

    [SerializeField]
    private GameObject Padre;
    private Vector3 Origen;
    [SyncVar]
    private int OriginaVida;

    [SerializeField]
    private AudioSource SonidoMovimiento;

    [SerializeField]
    private GameObject[] Caramelo;

    [SerializeField]
    private TextMeshProUGUI textVida;

    [SerializeField]
    private GameObject[] PuntosInstancia;
    [SerializeField]
    private string TagPuntosInstancia;

    [ServerCallback]
    void Start()
    {
        if (TagPuntosInstancia != "")
        {
            PuntosInstancia = GameObject.FindGameObjectsWithTag(TagPuntosInstancia);
            transform.position = PuntosInstancia[Random.Range(0, PuntosInstancia.Length)].transform.position; //POSICION ALEATORIA ENTRE PUNTOS
        }
        else
            Origen = transform.position;  //LA POSICION DE PARTIDA
        OriginaVida = EnemyHealth;
    }

    [Server]
    public void Reinicio()
    {
        if (TagPuntosInstancia != "")
            transform.position = PuntosInstancia[Random.Range(0, PuntosInstancia.Length)].transform.position; //POSICION ALEATORIA ENTRE PUNTOS
        else
            transform.position = Origen; //LA POSICION DE PARTIDA
        isDead = false;
        EnemyHealth = OriginaVida;
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
            anim.GetComponent<Animator>().SetBool("muerte", false);
        //Cae al suelo si es Dron
        if (isDron)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
        //Desactiva Caramelos
        for (int i = 0; i < Caramelo.Length; i++)
        {
            Caramelo[i].SetActive(false);
            RpcDesActiveCaramelo(i);
        }

        StartCoroutine(WaitForAparece());
    }

    [Server]
    public IEnumerator WaitForAparece()
    {
        yield return new WaitForSeconds(waitForAparece);
        Padre.SetActive(true);
        RpcSetActive(true);
    }
    
    [ClientRpc]
    void RpcReAim()
    {
        GetComponent<AimAnimScript>().enabled = true;
    }
    [Server]
    public void impacto()
    {
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
            isDead = true;
            //desactiva la IA
            if (GetComponent<_LocomotionSimpleAgentNoRoot>())
                GetComponent<_LocomotionSimpleAgentNoRoot>().enabled = false;
            behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
            if (GetComponent<NavMeshAgent>())
                GetComponent<NavMeshAgent>().enabled = false;
            //desactiva Ain anim
            if (GetComponent<AimAnimScript>())
            {
                GetComponent<AimAnimScript>().enabled = false;
                RpcAim();
            }
            //animacion muerte
            if (anim)
                anim.GetComponent<Animator>().SetBool("muerte", true);
            //explota
            if (explosion)
            {
                explosion.Play(); //EXPLOTA EN SERVIDOR
                if (SonidoExplosion) SonidoExplosion.Play();
                RpcExplota();
            }
            //Cae al suelo si es Dron
            if (isDron)
                GetComponent<Rigidbody>().useGravity = true;
            //llama a destruye o desactiva
            StartCoroutine(WaitForDestroy());
        }
    }
    [ClientRpc]
    void RpcExplota()
    {
        explosion.Play(); //EXPLOTA EN CLIENTES
        if (SonidoExplosion) SonidoExplosion.Play();
    }
    [ClientRpc]
    void RpcAim()
    {
        GetComponent<AimAnimScript>().enabled = false;
    }
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
        if (ItemPrefab)
        {
            Item = Instantiate(ItemPrefab, transform.localPosition, Quaternion.identity);
            Item.transform.position = new Vector3(Item.transform.position.x, 0f, Item.transform.position.z);
            NetworkServer.Spawn(Item);
        }
        if (isPool)
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
        else
            if (!NoDestructible)
                NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]   
    void RpcSetActive(bool act)
    {
        Padre.SetActive(act);
    }
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
        for (int i = 0; i < Caramelo.Length - newEnemyVida; i++)
        {
            Caramelo[i].SetActive(true);
        }
    }

    //LO LLAMA BALAPLAYER PARA QUE SE EJECUTE EN EL SERVIDOR YA QUE LA HOOK NO LO HACE
    [Server]
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
    }
}
