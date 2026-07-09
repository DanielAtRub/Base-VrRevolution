using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using NodeCanvas.BehaviourTrees;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class Enemigo0 : NetworkBehaviour
{
    [SyncVar]
    public bool isDead, destruir;
    [SyncVar(hook = nameof(SetEnemyVida))]
    public int EnemyHealth = 100;
    [SerializeField]
    private GameObject behavior, anim;
    //[SerializeField]
    //private string animOrigen;
    //[SerializeField]
    //private ParticleSystem impactoExplosion;
    //[SerializeField]
    //private bool isDron;
    [SerializeField]
    private bool NoDestructible;
    [SerializeField]
    private float wairForDestroy, waitForAparece, waitForReAparece;  // T desaparece despues de morir - T aparece al inicio - T reaparece despues de morir
    [SerializeField]
    private bool AnimPrevia;
    [SerializeField]
    private float WaitAnimPrevia; // T animación inicial al aparecer o reaparecer
    //[SerializeField]
    //private ParticleSystem EfectoExplosion;

    [SerializeField]
    private bool isPool;

    [SerializeField]
    private GameObject Padre;
    [SerializeField]
    private Vector3 OrigenPos;
    [SerializeField]
    private Quaternion OrigenRot;
    [SyncVar]
    private int OriginaVida;

    [SerializeField]
    private AudioSource SonidoAtaque, SonidoMuerte, SonidoMovimiento;

    [SerializeField]
    private GameObject[] PartesCuerpo;
    [SerializeField]
    private BoxCollider[] ColPartesDesmemb;

    [SerializeField]
    private TextMeshPro textVida;

    private Coroutine C1, C2, C3, C4, C5;

    [ServerCallback]
    void Start()
    {
        OrigenPos = transform.position;  //LA POSICION DE PARTIDA
        OrigenRot = transform.rotation;  //LA ROTACION DE PARTIDA
        OriginaVida = EnemyHealth;

        C1 = StartCoroutine(WaitForAparece(waitForAparece));

        if (AnimPrevia)
            C2 = StartCoroutine(WaitAnimPrev(WaitAnimPrevia));
        else
            behavior.GetComponent<BehaviourTreeOwner>().enabled = true;
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

    [Server]
    public void Reinicio()
    {
        transform.position = OrigenPos; //LA POSICION DE PARTIDA
        transform.rotation = OrigenRot; //LA ROTACION DE PARTIDA
        isDead = false;
        EnemyHealth = OriginaVida;

        //Desactiva animacion y muerte
        if (anim)
        {
            anim.GetComponent<Animator>().SetBool("muerte", false);
            anim.GetComponent<Animator>().speed = 0;
        }   
        //Activa Partes cuerpo
        for (int i = 0; i < PartesCuerpo.Length; i++)
        {
            PartesCuerpo[i].SetActive(true);
        }
        RpcActPartesCuerpo();
        for (int i = 0; i < ColPartesDesmemb.Length; i++)
        {
            ColPartesDesmemb[i].enabled = true;
        }

        C3 = StartCoroutine(WaitForAparece(waitForReAparece));
    }

    [Server]
    public IEnumerator WaitForAparece(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Padre.SetActive(true); // activa al Padre
        RpcSetActive(true);

        if (AnimPrevia)
            C4 = StartCoroutine(WaitAnimPrev(WaitAnimPrevia));
        else
            behavior.GetComponent<BehaviourTreeOwner>().enabled = true;
        if (GetComponent<NavMeshAgent>())
            GetComponent<NavMeshAgent>().enabled = true;

        //Activa animacion para la aparición inicial
        if (anim)
        {
            anim.GetComponent<Animator>().speed = 1;
            anim.GetComponent<Animator>().Rebind();
        }
    }

    [Server]
    public IEnumerator WaitAnimPrev(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        behavior.GetComponent<BehaviourTreeOwner>().enabled = true;
    }

    [Server]
    public void impacto() //LLAMADO DESDE DISPAROGITPLAYER
    {
        if (anim)
        {
            anim.GetComponent<Animator>().SetTrigger("impacto");
            GetComponent<NetworkAnimator>().SetTrigger("impacto");
        }
        /*if (impactoExplosion)
        {
            impactoExplosion.Play();
            RpcImpactoExplosion();
        }*/
    }
    /*[Server]
    public IEnumerator WaitForParaImpacto()
    {
        yield return new WaitForSeconds(1f);
        if (GetComponent<NavMeshAgent>())
            GetComponent<NavMeshAgent>().enabled = true;
    }*/

    [Server]
    void muerte()
    {
        if (!isDead)
        {
            isDead = true;

            // PARA TODAS LAS COROUTINAS
            if (C1 != null)
                StopCoroutine(C1);
            if (C2 != null)
                StopCoroutine(C2);
            if (C3 != null)
                StopCoroutine(C3);
            if (C4 != null)
                StopCoroutine(C4);
            if (C5 != null)
                StopCoroutine(C5);

            //desactiva la IA
            /*if (EnemigoRoot)
                if (GetComponent<AIRoot>())
                    GetComponent<AIRoot>().enabled = false;*/
            behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            //animacion muerte
            if (anim)
                anim.GetComponent<Animator>().SetBool("muerte", true);

            if (SonidoMuerte)
            {
                SonidoMovimiento.Stop();
                SonidoMuerte.Play();
                RpcSonidos(2);
            }
            //explota
            /*if (EfectoExplosion)
            {
                EfectoExplosion.Play(); //EXPLOTA EN SERVIDOR
            }*/
            //Cae al suelo si es Dron
            //if (isDron)
            //    GetComponent<Rigidbody>().useGravity = true;
            //llama a destruye o desactiva
            C5 = StartCoroutine(WaitForDestroy());
        }
    }
   
    [Server]
    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(wairForDestroy);
        if (isPool)
        {
            if (destruir)
            {
                gameObject.SetActive(false);
                RpcSetActiveTOTAL(false);
            }
            else
            {
                Padre.SetActive(false); //desactiva al Padre
                RpcSetActive(false); 
                Reinicio();
            }
        }
        else
            if (!NoDestructible)
                NetworkServer.Destroy(gameObject);
    }


    [Server]
    public void Ataque() // LLAMADO DESDE BEHAVIOUR
    {
        SonidoAtaque.Play();
        RpcAtaque();
    }
    [ClientRpc]
    void RpcAtaque()
    {
        SonidoAtaque.Play();
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

    [ClientRpc]
    void RpcSonidos(int n)
    {
        if (n == 1)
            SonidoAtaque.Play();
        if (n == 2)
        {
            SonidoMovimiento.Stop();
            SonidoMuerte.Play();
        }
    }

    //LAMADO DESDE REINICIO (AQUI) ACTIVA PARTES CUERPO
    [ClientRpc]
    void RpcActPartesCuerpo()
    {
        for (int i = 0; i < PartesCuerpo.Length; i++)
        {
            PartesCuerpo[i].SetActive(true);
        }
    }

    //FUNCIONES HOOK - LO HACE SOBRE TODOS LOS CLIENTES
    void SetEnemyVida(int oldEnemyVida, int newEnemyVida)
    {
        textVida.text = newEnemyVida.ToString();
    }
}
