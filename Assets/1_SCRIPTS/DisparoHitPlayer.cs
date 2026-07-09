using UnityEngine;
using Mirror;
using System;
using TMPro;

public class DisparoHitPlayer : NetworkBehaviour
{
    [SyncVar]
    public bool GatilloPulsado;
    private float nextFire;
    [Header("ARMAS")]
    [SyncVar]
    public int ArmaId = 1;
    public GameObject[] Armas;
    [SerializeField]
    public GameObject[] IconosArmas;
    public Transform[] gunEnds;
    public Transform gunEnd;
    public GameObject[] Fogonazos;
    public GameObject Fogonazo;
    public GameObject ImpactEffectPrefabDecorado;//, ImpactEffectPrefabEnemigo;
    public AudioSource sinbalasAudio;
    [SyncVar]
    public float fireRate;
    [SyncVar]
    public int Daño;
    [SyncVar]
    public int BalasPorCargador;
    [SyncVar(hook = nameof(SetBalasEnCargador))]
    public int BalasEnCargador;
    [SyncVar]
    public bool recargando;
    [SerializeField]
    public GameObject MsjRecargar, MsjRecargando, MsjSinMunicion;
    [SerializeField]
    private int BalasArm1;//, BalasArm2, BalasArm3;
    [Header("INDICADORES")]
    [SerializeField]
    private TextMeshProUGUI textBalasEnCargador;
    [Header("OTROS")]
    //[SerializeField]
    //private GameObject GameManager;
    private GameObject impact;
    //[SerializeField]
    //private AudioSource AudioMuniciónCapturada;
    //[SerializeField]
    //private TextMeshProUGUI textDEBUG;
    private Quaternion rot;
    [Header("PUNTOS")]
    [SerializeField]
    private int pointsI;
    [SerializeField]
    private int pointsM;
    [Header("RECARGA MOV")]
    [SerializeField]
    private Transform RightHandAnchor;
    private bool trig = true;

    void Start()
    {
        BalasEnCargador = BalasPorCargador;
        textBalasEnCargador.text = BalasEnCargador.ToString();
    }

    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        //MOVIMIENTO DE RECARGA
        if (!recargando)
        {
            if (RightHandAnchor.eulerAngles.x > 280 && RightHandAnchor.eulerAngles.x < 310) 
            {
                if (trig)
                {
                    trig = false;
                    if (ArmaId == 1)
                    {
                        BalasPorCargador = BalasArm1;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 1f;
                    }
                    /*if (ArmaId == 2)
                    {
                        BalasPorCargador = BalasArm2;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 2f;
                    }
                    if (ArmaId == 3)
                    {
                        BalasPorCargador = BalasArm3;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 4f;
                    }*/
                    recargando = true;
                    MsjRecargando.SetActive(true);
                }
            }
            else
                trig = true;
        }

        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            GatilloPulsado = false;
            CmdGatilloPulsado(false);

            nextFire = 0;
        }

        // DIPARO AUTO Id=1
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && Time.time > nextFire)
        {
            GatilloPulsado = true;
            CmdGatilloPulsado(true);

            if (BalasEnCargador > 0 && !recargando)
            {
                CmdFire(gunEnd.position, gunEnd.TransformDirection(Vector3.forward));
            }
            else
            {
                if (!recargando)
                {
                    if (ArmaId == 1)
                    {
                        BalasPorCargador = BalasArm1;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 1f;
                    }
                    /*if (ArmaId == 2)
                    {
                        BalasPorCargador = BalasArm2;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 2f;
                    }
                    if (ArmaId == 3)
                    {
                        BalasPorCargador = BalasArm3;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 4f;
                    }*/
                    recargando = true;
                    MsjRecargando.SetActive(true);
                }

                sinbalasAudio.Play();
            }
            nextFire = Time.time + fireRate;
        }

        // CAMBIO DE ARMA
        /*if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            if (ArmaId == 1)
                CmdSetArma1();
            if (ArmaId == 2)
                CmdSetArma3();
            if (ArmaId == 3)
                CmdSetArma1();
        }*/
    }

    [Command]
    void CmdGatilloPulsado(bool re)
    {
        GatilloPulsado = re;
    }

    // LLAMADA DESDE EL MSJ TIEMPO DE RECARGA
    public void Recargar()
    {
        recargando = false;
        BalasEnCargador = BalasPorCargador;
        textBalasEnCargador.text = BalasEnCargador.ToString();

        CmdRecargado(true);
    }
    
    [Command]
    void CmdRecargado(bool re)
    {
        BalasEnCargador = BalasPorCargador;
    }
    
    // this is ejecutado on the Player server
    [Command]
    void CmdFire(Vector3 firePos, Vector3 fireDirection)
    {
        RaycastHit hit;

        //if (Physics.Raycast(firePos, fireDirection, out hit, 500f, -1, QueryTriggerInteraction.Ignore)
        if (Physics.Raycast(firePos, fireDirection, out hit, 500f, -1)
        && !GetComponentInParent<Player>().aNegro && !GetComponentInParent<Player>().isDead)
        {
            BalasEnCargador--;

            //FOGONAZO
            fogonazoServidor();//SOLO PARA EL SERVIDOR POR EJECUTARSE DENTRO DEL COMMAND
            RpcFogonazo();//DESDE EL SERVIDOR PARA ALLS LOS CLIENTES

            //IMPACTO EN DECORADOS Y ESTATICOS
            if (!hit.collider.CompareTag("Enemy") && 
                !hit.collider.CompareTag("Finish") && 
                !hit.collider.CompareTag("Player"))// &&
                //!hit.collider.CompareTag("Fire"))
                //!hit.collider.CompareTag("Move"))
            {
                rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                impact = Instantiate(ImpactEffectPrefabDecorado, hit.point, rot);
                RpcSetImpactDecorado(hit.point, rot);  //ADMITE EL TRANSFOR PORQUE TIENE NETIDENT
            }

            if (hit.collider.CompareTag("Enemy")) //PISTOLERO
            {
                rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                //hit.collider.GetComponentInParent<SangreEnemy>().Sangre(hit.point, rot,
                //hit.collider.transform.parent.parent.parent.parent.parent.parent.parent.parent,
                //hit.collider.name); // IMPACTO SANGRE
                //impact = Instantiate(ImpactEffectPrefabEnemigo, hit.point, rot); //SOLO SONIDO
                //RpcSetImpactEnemigo(hit.point, rot);  //ADMITE EL TRANSFOR PORQUE TIENE NETIDENT
                if (!hit.collider.GetComponentInParent<Enemigo>().isDead)
                {
                    hit.collider.GetComponentInParent<Enemigo>().EnemyHealth -= Daño;
                    if (hit.collider.GetComponentInParent<Enemigo>().EnemyHealth <= 0)
                    {
                        GetComponentInParent<Player>().PlayerPuntos += pointsM;
                        GetComponentInParent<Player>().EnemigosMatados++;
                    }
                    else
                    {
                        hit.collider.GetComponentInParent<Enemigo>().impacto();
                        GetComponentInParent<Player>().PlayerPuntos += pointsI;
                    }
                }
            }

            /*if (hit.collider.CompareTag("EnemyCabeza")) //INDIO
            {
                rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                //hit.collider.GetComponentInParent<SangreEnemy>().Sangre(hit.point, rot,
                //hit.collider.transform.parent.parent.parent.parent.parent.parent.parent.parent,
                //hit.collider.name); // IMPACTO SANGRE
                if (!hit.collider.GetComponentInParent<EnemigoIndio>().isDead)
                {
                    hit.collider.GetComponentInParent<EnemigoIndio>().EnemyHealth -= Daño;
                    if (hit.collider.GetComponentInParent<EnemigoIndio>().EnemyHealth <= 0)
                    {
                        GetComponentInParent<Player>().PlayerPuntos += pointsM;
                        GetComponentInParent<Player>().EnemigosMatados++;
                    }
                    else
                    {
                        hit.collider.GetComponentInParent<EnemigoIndio>().impacto();
                        GetComponentInParent<Player>().PlayerPuntos += pointsI;
                    }
                }
            }*/

        }
        //else
            //canShoot = false;
    }

    [ClientRpc]
    void RpcSetImpactDecorado(Vector3 impactPos, Quaternion impactRot)
    {
        impact = Instantiate(ImpactEffectPrefabDecorado, impactPos, impactRot);
    }
    /*[ClientRpc]
    void RpcSetImpactEnemigo(Vector3 impactPos, Quaternion impactRot)
    {
        impact = Instantiate(ImpactEffectPrefabEnemigo, impactPos, impactRot);
    }*/

    [ClientRpc]
    void RpcFogonazo()
    {
        if (Fogonazo != null)
        {
            Fogonazo.GetComponent<ParticleSystem>().Play();
            Fogonazo.GetComponent<AudioSource>().Play();
        }
    }
    void fogonazoServidor()
    {
        if (Fogonazo != null)
        {
            Fogonazo.GetComponent<ParticleSystem>().Play();
            Fogonazo.GetComponent<AudioSource>().Play();
        }
    }

    //VARIABLES HOOK
    void SetBalasEnCargador(int oldBalasEnCargador, int newBalasEnCargador)
    {
        textBalasEnCargador.text = newBalasEnCargador.ToString();
    }
    //ARMA 1
    [Command]
    void CmdSetArma1()
    {
        ArmaId = 1;
    
        Armas[0].SetActive(true);
        //Armas[1].SetActive(false);
        //Armas[2].SetActive(false);
        Fogonazo = Fogonazos[0];
        gunEnd = gunEnds[0];
        BalasPorCargador = BalasArm1;
        BalasEnCargador = BalasArm1;
        fireRate = 0.3f;
        Daño = 1;
        RpcSetArma1();
    }
    [ClientRpc]
    void RpcSetArma1()
    {
        Armas[0].SetActive(true);
        //Armas[1].SetActive(false);
        //Armas[2].SetActive(false);
        Fogonazo = Fogonazos[0];
        gunEnd = gunEnds[0];
    }
    //ARMA 2
    /*[Command]
    void CmdSetArma2()
    {
        ArmaId = 2;

        Armas[0].SetActive(false);
        Armas[1].SetActive(true);
        Armas[2].SetActive(false);
        Fogonazo = Fogonazos[1];
        gunEnd = gunEnds[1];
        BalasPorCargador = BalasArm2;
        BalasEnCargador = BalasArm2;
        fireRate = 0.1f;
        Daño = 1;
        RpcSetArma2();
    }
    [ClientRpc]
    void RpcSetArma2()
    {
        Armas[0].SetActive(false);
        Armas[1].SetActive(true);
        Armas[2].SetActive(false);
        Fogonazo = Fogonazos[1];
        gunEnd = gunEnds[1];
    }
    //ARMA 3
    [Command]
    void CmdSetArma3()
    {
        ArmaId = 3;

        Armas[0].SetActive(false);
        Armas[1].SetActive(false);
        Armas[2].SetActive(true);
        Fogonazo = Fogonazos[2];
        gunEnd = gunEnds[2];
        BalasPorCargador = BalasArm3;
        BalasEnCargador = BalasArm3;
        fireRate = 1;
        Daño = 1;
        RpcSetArma3();
    }
    [ClientRpc]
    void RpcSetArma3()
    {
        Armas[0].SetActive(false);
        Armas[1].SetActive(false);
        Armas[2].SetActive(true);
        Fogonazo = Fogonazos[2];
        gunEnd = gunEnds[2];
    }*/
   
}
