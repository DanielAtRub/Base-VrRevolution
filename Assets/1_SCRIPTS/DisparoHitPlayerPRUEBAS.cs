using UnityEngine;
using Mirror;
using System;
using TMPro;

public class DisparoHitPlayerPRUEBAS : NetworkBehaviour
{
    [SyncVar]
    public bool canShoot;
    private float nextFire;
    [Header("ARMAS")]
    //[SyncVar(hook = nameof(SetArmaId))]
    [SyncVar]
    public int ArmaId;
    public GameObject[] Armas;
    [SerializeField]
    public GameObject[] IconosArmas;
    public Transform[] gunEnds;
    public Transform gunEnd;
    public GameObject[] Fogonazos;
    public GameObject Fogonazo;
    public GameObject ImpactEffectPrefab, ImpactEffectPrefabBlood;
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
    [SyncVar(hook = nameof(SetCapturaMunicion))]
    public int CapturaMunicion;
    [Header("INDICADORES")]
    [SerializeField]
    private TextMeshProUGUI textBalasEnCargador;
    /*[Header("BALA TRAZADORA")]
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject projectilePrefabSERVER;
    private GameObject projectile;*/
    [Header("OTROS")]
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject GameManager;
    private GameObject impact;
    [SerializeField]
    private AudioSource AudioMuniciónCapturada;
    [SerializeField]
    private TextMeshProUGUI textDEBUG;
    private Quaternion rot;
    [Header("PUNTOS")]
    [SerializeField]
    private int pointsI;
    [SerializeField]
    private int pointsM;

    private bool soyYo;

    void Start()
    {
        BalasEnCargador = BalasPorCargador;
        textBalasEnCargador.text = BalasEnCargador.ToString();
        //textCargadores.text = Cargadores.ToString();
    }

    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        // DIPARO AUTO Id=1
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && Time.time > nextFire)
        {
            if (BalasEnCargador > 0)
            {
                soyYo = true; 
                CmdFire(gunEnd.position, gunEnd.TransformDirection(Vector3.forward));



                impactoDesdeLocal(gunEnd.position, gunEnd.TransformDirection(Vector3.forward));



                //TRAZADORA
                //if (canShoot)
                //projectile = Instantiate(projectilePrefab, gunEnd.position, gunEnd.rotation); //LOCAL PLAYER
            }
            else
            {
                if (!recargando)
                {
                    //MsjRecargar.SetActive(true); //LAMA A RECARGAR
                    if (ArmaId == 1)
                    {
                        BalasPorCargador = 20;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 3f;
                    }
                    if (ArmaId == 2)
                    {
                        BalasPorCargador = 60;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 4f;
                    }
                    if (ArmaId == 3)
                    {
                        BalasPorCargador = 6;
                        MsjRecargando.GetComponent<MsjRecargando>().ValorIniRef = 6f;
                    }
                    recargando = true;
                    //MsjRecargar.SetActive(false);
                    MsjRecargando.SetActive(true);
                }
                /*else
                {
                    if (!recargando)
                    {
                        //SIN BalasEnCargador Y SIN Cargadores
                        MsjSinMunicion.SetActive(true); //SIN MUNICION
                    }
                }*/
                sinbalasAudio.Play();
            }
            nextFire = Time.time + fireRate;
        }

        // CAMBIO DE ARMA
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            if (ArmaId == 1)
                CmdSetArma2();
            if (ArmaId == 2)
                CmdSetArma3();
            if (ArmaId == 3)
                CmdSetArma1();
            //if (BalasEnCargador <= 0/* && Cargadores > 0*/)
            //{  
            //}
        }
    }



    //PRUEBA
    private void impactoDesdeLocal(Vector3 firePos, Vector3 fireDirection)
    {
        RaycastHit hit;

        if (Physics.Raycast(firePos, fireDirection, out hit, 500f, -1, QueryTriggerInteraction.Ignore)
        && !GetComponentInParent<Player>().aNegro && !GetComponentInParent<Player>().isDead)
        {
            if (hit.collider.tag == "PartEnemy") //RESTO MENOS CABEZA
            {
                rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                hit.collider.GetComponentInParent<SangreEnemy>().Sangre(hit.point, rot,
                    hit.collider.transform.parent.parent.parent.parent.parent.parent.parent.parent,
                    hit.collider.name); // IMPACTO SANGRE
            }
        }
    }



    // this is ejecutado on the Player server
    [Command]
    void CmdFire(Vector3 firePos, Vector3 fireDirection)
    {
        RaycastHit hit;

        //if (!GameManager)
            //GameManager = GameObject.Find("GameManager");

        if (Physics.Raycast(firePos, fireDirection, out hit, 500f, -1, QueryTriggerInteraction.Ignore)
        && !GetComponentInParent<Player>().aNegro && !GetComponentInParent<Player>().isDead
        /*&& !GameManager.GetComponent<Manager>().GameOver
        && (GameManager.GetComponent<Manager>().RondaActiva
        || GameManager.GetComponent<Manager>().Mision == 0)*/)
        {
            canShoot = true;

            BalasEnCargador--;
                
            //FOGONAZO
            fogonazoServidor();//SOLO PARA EL SERVIDOR POR EJECUTARSE DENTRO DEL COMMAND
            RpcFogonazo();//DESDE EL SERVIDOR PARA ALLS LOS CLIENTES
                     
            //TRAZADORA
            //projectile = Instantiate(projectilePrefabSERVER, firePos, gunEnd.rotation); //EN SERVIDOR
            //RpcTrazadora(firePos, gunEnd.rotation);

            //IMPACTO EN DECORADOS Y ESTATICOS
            //if (!hit.collider.CompareTag ("Finish")) //PAREDES DEL LIMITE
            //{
                if (!hit.collider.CompareTag("PartEnemy") && !hit.collider.CompareTag("EnemyCabeza"))
                {
                    rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    impact = Instantiate(ImpactEffectPrefab, hit.point, rot/*, hit.collider.transform*/);
                    RpcSetImpact(hit.point, rot/*, hit.collider.transform, false*/);  //ADMITE EL TRANSFOR PORQUE TIENE NETIDENT
                }
            //}

            //if (!GameManager)
                //GameManager = GameObject.Find("GameManager");

            // Quita vida al enemigo
            if (hit.collider.tag == "PartEnemy") //RESTO MENOS CABEZA
            {
                rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                hit.collider.GetComponentInParent<SangreEnemy>().Sangre(hit.point, rot, 
                    hit.collider.transform.parent.parent.parent.parent.parent.parent.parent.parent, 
                    hit.collider.name); // IMPACTO SANGRE
                hit.collider.GetComponentInParent<Enemigo0>().impacto();
                if (!hit.collider.GetComponentInParent<Enemigo0>().isDead)
                {
                    hit.collider.GetComponentInParent<Enemigo0>().EnemyHealth -= Daño;
                    if (hit.collider.GetComponentInParent<Enemigo0>().EnemyHealth <= 0)
                    {
                        GetComponentInParent<Player>().PlayerPuntos += pointsM;
                        GetComponentInParent<Player>().EnemigosMatados++;
                    }
                    else
                    {
                        GetComponentInParent<Player>().PlayerPuntos += pointsI;
                    }
                }
            }
            if (hit.collider.tag == "EnemyCabeza") //CABEZA
            {
                //rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                //hit.collider.GetComponentInParent<SangreEnemy>().Sangre(hit.point, rot); // IMPACTO EXPLOTA CABEZA, EN ESTE
                //LA EXPLOSION ESTA DENTRO DE LA CABEZA POR NO NECESITAR COORDENADAS DE IMPACTO Y LA EJECUTA PARTECUERPOENEMY
                hit.collider.GetComponentInParent<Enemigo0>().impacto();
                if (!hit.collider.GetComponentInParent<Enemigo0>().isDead)
                {
                    hit.collider.GetComponentInParent<ParteCuerpoEnemy>().Desmiembra(hit.collider); //DESMIEMBRA CABEZA
                    hit.collider.GetComponentInParent<Enemigo0>().EnemyHealth = 0;
                
                    GetComponentInParent<Player>().PlayerPuntos += pointsM;
                    GetComponentInParent<Player>().EnemigosMatados++;
                }
            }
            /*if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponentInParent<Enemigo0>().impacto();

                if (!hit.collider.GetComponentInParent<Enemigo0>().isDead)
                {
                    hit.collider.GetComponentInParent<Enemigo0>().EnemyHealth -= Daño;
                    //co.collider.GetComponentInParent<Enemigo>().ActiveCarameloServer(); //ACTUALIZA EN SERVER
                    if (hit.collider.GetComponentInParent<Enemigo0>().EnemyHealth <= 0)
                    {
                        GetComponentInParent<Player>().PlayerPuntos += pointsM;
                        GetComponentInParent<Player>().EnemigosMatados++;
                    }
                    else
                    {
                        GetComponentInParent<Player>().PlayerPuntos += pointsI;
                    }
                }
            }*/
        }
        else
            canShoot = false;
    }

    [Command]
    void CmdLaser(bool re)
    {
        laser.SetActive(re);
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
        //Cargadores--;
        BalasEnCargador = BalasPorCargador;
    }

    /*[ClientRpc]
    void RpcTrazadora(Vector3 fPos, Quaternion fRot)
    {
        if (!soyYo)
            projectile = Instantiate(projectilePrefab, fPos, fRot); // EN TODOS CLIENTES MENOS YO
        else
            soyYo = false;
    }*/

    [ClientRpc]
    void RpcSetImpact(Vector3 impactPos, Quaternion impactRot/*, Transform targetID, bool sangre*/)
    {
        //if (!soyYo)  //PRUEBAS
        //{
            //if (sangre)
                //impact = Instantiate(ImpactEffectPrefabBlood, impactPos, impactRot, targetID);
            //else
                impact = Instantiate(ImpactEffectPrefab, impactPos, impactRot/*, targetID*/);
        //}
    }

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
    void SetCapturaMunicion(int oldCapturaMunicion, int newCapturaMunicion)
    {       
        AudioMuniciónCapturada.Play(); // SONIDO DE MUNICIÓN CAPTURADA
    }
    void SetBalasEnCargador(int oldBalasEnCargador, int newBalasEnCargador)
    {
        textBalasEnCargador.text = newBalasEnCargador.ToString();
    }
    /*void SetCargadores(int oldCargadores, int newCargadores)
    {
        textCargadores.text = newCargadores.ToString();
    }*/

    /*void SetArmaId(int newArmaId)
    {
        //if (isLocalPlayer)
        //{
            if (newArmaId == 1)
            {
                Armas[0].SetActive(true);
                Armas[1].SetActive(false);
                Armas[2].SetActive(false);
                IconosArmas[0].SetActive(true);
                IconosArmas[1].SetActive(false);
                IconosArmas[2].SetActive(false);
                Fogonazo = Fogonazos[0];
                gunEnd = gunEnds[0];
                CmdSetArma1();
            }
            if (newArmaId == 2)
            {
                Armas[0].SetActive(false);
                Armas[1].SetActive(true);
                Armas[2].SetActive(false);
                IconosArmas[0].SetActive(false);
                IconosArmas[1].SetActive(true);
                IconosArmas[2].SetActive(false);
                Fogonazo = Fogonazos[1];
                gunEnd = gunEnds[1];
                CmdSetArma2();
            }
            if (newArmaId == 3)
            {
                Armas[0].SetActive(false);
                Armas[1].SetActive(false);
                Armas[2].SetActive(true);
                if (isLocalPlayer)
                    ZoomRifle.SetActive(true); // RIFLE CAMERA
                IconosArmas[0].SetActive(false);
                IconosArmas[1].SetActive(false);
                IconosArmas[2].SetActive(true);
                Fogonazo = Fogonazos[2];
                gunEnd = gunEnds[2];
                CmdSetArma3();
            }
        //}
    }*/
    //ARMA 1
    [Command]
    void CmdSetArma1()
    {
        ArmaId = 1;
        Debug.Log("ARMA: " + ArmaId);

        Armas[0].SetActive(true);
        Armas[1].SetActive(false);
        Armas[2].SetActive(false);
        Fogonazo = Fogonazos[0];
        gunEnd = gunEnds[0];
        BalasPorCargador = 20;
        BalasEnCargador = 0;
        fireRate = 0.3f;
        Daño = 2;
        RpcSetArma1();
    }
    [ClientRpc]
    void RpcSetArma1()
    {
        Armas[0].SetActive(true);
        Armas[1].SetActive(false);
        Armas[2].SetActive(false);
        Fogonazo = Fogonazos[0];
        gunEnd = gunEnds[0];
    }
    //ARMA 2
    [Command]
    void CmdSetArma2()
    {
        ArmaId = 2;
        Debug.Log("ARMA: " + ArmaId);

        Armas[0].SetActive(false);
        Armas[1].SetActive(true);
        Armas[2].SetActive(false);
        Fogonazo = Fogonazos[1];
        gunEnd = gunEnds[1];
        BalasPorCargador = 60;
        BalasEnCargador = 0;
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
        Debug.Log("ARMA: " + ArmaId);

        Armas[0].SetActive(false);
        Armas[1].SetActive(false);
        Armas[2].SetActive(true);
        Fogonazo = Fogonazos[2];
        gunEnd = gunEnds[2];
        BalasPorCargador = 6;
        BalasEnCargador = 0;
        fireRate = 1;
        Daño = 3;
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
    }

}
