using UnityEngine;
using TMPro;
using Mirror;
using NodeCanvas.BehaviourTrees;

public class JuegoManager1 : NetworkBehaviour
{
    [Header("GLOBALES")]
    public float TiempoPartidaTotal;
    [SyncVar]
    public float TiempoPartida;
    [SyncVar]
    public bool GameOver;
    [SerializeField]
    private TextMeshPro textTiempo;
    [SerializeField]
    private TextMeshProUGUI textTiempoUI;
    [SerializeField]
    private GameObject CamaraL0, CamaraL1, CamaraL2, CamaraL3;
    public GameObject[] PlayersEnJuego;

    [Header("MENSAJES")]
    [SyncVar]
    public int MsjServerToPlayers;

    [Header("LINTERNA / ESCUDO PLAYERS")]
    [SyncVar]
    public bool OnLinterna;
    [SyncVar]
    public bool OnEscudo;

    [Header("MUNICION")]
    [SerializeField]
    private GameObject MunicionL1;
    [SerializeField]
    private GameObject MunicionPLATAFORMA, MunicionL2, MunicionL3;

    [Header("COMIENZO Y CARTELES NIVELES")]
    [SerializeField]
    private GameObject ComienzoyMsjLevel1;
    [SerializeField]
    private GameObject MsjLevel2;

    [Header("LEVELS")]
    [SerializeField]
    private GameObject L0;
    [SerializeField]
    private GameObject L1, L2, L3;

    [Header("LEVEL 0 - INICIO")]
    [SyncVar]
    public bool ActLevel0;
    [SyncVar]
    public bool ActAdquiereAvatar, ActSkyboxTierra;
    [SerializeField]
    private GameObject ZonaAdquiereAvatar;
    public int PlayersUbicadosL0;
    [SerializeField]
    private Material SkyboxMatEspacio, SkyboxMatPlaneta;
    [Header("LEVEL 1 - ENEMIGOS MANAGER")]
    [SyncVar]
    public bool ActLevel1;
    [SerializeField]
    private GameObject Orda1;
    [SerializeField]
    private GameObject Orda1Manager, Orda2, Orda2Manager, Orda3_1, Orda3_2;
    [SyncVar]
    public bool ActOrda1, ActOrda2, ActOrda3, ActUbicacionL1;
    [Header("          REUBICACION - LEVEL 1 AL 2")]
    [SerializeField]
    private GameObject ZonaUnicacionL1;
    public int PlayersUbicadosL1;
    [Header("LEVEL 2/1 - PLATAFORMA MANAGER")]
    [SyncVar]
    public bool ActLevel2;
    [SerializeField]
    private GameObject Plataforma, GraficoPlataforma;
    [SerializeField]
    private GameObject Orda4, Orda4Manager;
    [SyncVar]
    public bool ActPlataforma, ActOrda4;
    [Header("LEVEL 2/2 - ENEMIGOS MANAGER")]
    [SerializeField]
    private GameObject Orda5;
    [SerializeField]
    private GameObject Orda5Manager, Orda5_1, Orda5_1Manager, Orda6_1, Orda6_2, Orda7, Ordenador1, Ordenador2, Ordenador3,
        Ordenador4, Ordenador5, Ordenador6, OrdenadorCentral;
    [SyncVar]
    public bool ActOrda5, ActOrda5_1, ActOrda6, ActOrda7, ActOrdenadores, ActUbicacionL2;
    [Header("          REUBICACION - LEVEL 2 AL 3")]
    [SerializeField]
    private GameObject ZonaUnicacionL2;
    public int PlayersUbicadosL2;
    [Header("LEVEL 3 - ENEMIGOS MANAGER")]
    [SyncVar]
    public bool ActLevel3;
    [SerializeField]
    private GameObject Orda8Manager, Orda8, Orda9Manager, Orda9, Terminales, OrdenadorVirus, UiVirus;
    [SyncVar]
    public bool MuevePlataforma, ActOrda8, ActOrda9, ActVirus;

    [Header("COMPROBADORES ANTIREPETICION")]
    [SerializeField]
    private bool trig0;
    [SerializeField]
    private bool trig1, trig2, trig3, trig4, trig5, trig6, trig7, trig8, trig9, trig10, trig11,
        trig12, trig13, trig14, trig15, trig16, trig17, trig18, trig19, trig20;

    //void Start() //MEJOR ONSTARTSERVER??
    public override void OnStartServer()
    {
        TiempoPartida = TiempoPartidaTotal;
        ActLevel0 = true;
    }

    [ServerCallback] //SI NO SE MUESTRA EL TIEMPO EN CLIENTES PUES QUE LO EJECUTE SOLO EL SERVIDOR
    void Update()
    {
        TiempoPartida -= Time.deltaTime;
        if (TiempoPartida <= 0)
        {
            TiempoPartida = 0;
            GameOver = true;
            Debug.Log("GAME OVER");
        }
        manager(); //INTENTAR QUE SE EJECUTE CUANDO SE ACTIVAL LOS ACT BOOL
        ControlPlayers(); //QUE SOLO SE EJECUTE CON LA GUI DE PLAYERS ACTIVA

        int tp = (int)TiempoPartida;
        //textTiempo.text = tp.ToString();
        textTiempoUI.text = tp.ToString();
    }

    [Server]
    public void jugar() //LO LLAMA EL BOTON READY
    {
        ActAdquiereAvatar = true;
    }

    [Server]
    public void StartOrda1() //START ORDAS... LO LLAMA CUENTA ATRAS DEL MENSAJE LEVEL 1 DE PASARELA
    {
        ActOrda1 = true;
    }

    [Server]
    public void manager()
    {
        //////////////////////////////////////////////  LEVEL 0 /////////////////////////////////////
        if (ActLevel0)
        {
            if (!trig0) //ANTIREPETICION
            {
                trig0 = true;
                //ACT/DES LEVELS
                L0.SetActive(true);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                RpcLevels(0);
                CamaraL0.SetActive(true);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                OnLinterna = true;
                OnEscudo = true;
                /*ActSkyboxTierra = false;
                if (ActSkyboxTierra)
                    RenderSettings.skybox = SkyboxMatPlaneta;
                else
                    RenderSettings.skybox = SkyboxMatEspacio;
                RpcSkybox(ActSkyboxTierra);*/
                GraficoPlataforma.SetActive(false);
                RpcActivePlataforma(false);
                Plataforma.transform.position = new Vector3(Plataforma.transform.position.x, 2000f,
                Plataforma.transform.position.z);
            }
            if (ActAdquiereAvatar)
            {
                if (!trig1) //ANTIREPETICION
                {
                    trig1 = true;
                    MsjServerToPlayers = 1; //ADQUIERE AVATAR
                    ZonaAdquiereAvatar.SetActive(true);
                    RpcSetActive(100, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL0)
                {
                    ActLevel0 = false;
                    ActAdquiereAvatar = false;
                    ZonaAdquiereAvatar.SetActive(false);
                    RpcSetActive(100, false);
                    ActLevel1 = true;
                    //CUENTA ATRAS PARA COMIENZO DEL JUEGO (orda1)
                    ComienzoyMsjLevel1.SetActive(true);
                    RpcActMsjLevel(1);
                    //Invoke("StartOrda1", 5.0f); //DELAY - PARA COMIENZO DEL JUEGO (orda1)
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 1 //////////////////////////////////////
        if (ActLevel1)
        {
            if (!trig2) //ANTIREPETICION
            {
                trig2 = true;
                CamaraL0.SetActive(false);
                CamaraL1.SetActive(true);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                ActSkyboxTierra = true;
                if (ActSkyboxTierra)
                    RenderSettings.skybox = SkyboxMatPlaneta;
                else
                    RenderSettings.skybox = SkyboxMatEspacio;
                RpcSkybox(ActSkyboxTierra);
                GraficoPlataforma.SetActive(true);
                RpcActivePlataforma(true);
                Plataforma.transform.position = new Vector3(Plataforma.transform.position.x, 0f,
                    Plataforma.transform.position.z);
                //ACT/DES LEVELS
                L0.SetActive(false);
                L1.SetActive(true);
                L2.SetActive(false);
                L3.SetActive(false);
                RpcLevels(1);
                MsjServerToPlayers = 2; //LEVEL 1
            }
            //LEVEL 1 - LA LLEGADA
            //ORDA1 - MINI DRONES 1
            if (ActOrda1)
            {
                if (!trig3) //ANTIREPETICION
                {
                    trig3 = true;
                    OnLinterna = false;
                    OnEscudo = false;
                    MunicionL1.GetComponent<BehaviourTreeOwner>().enabled = true;
                    Orda1.SetActive(true);
                    Orda1Manager.SetActive(true);
                    RpcSetActive(1, true);
                }
                if (Orda1Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    ActOrda1 = false;
                    ActOrda2 = true;
                }
            }
            //ORDA2 - DROIDES
            if (ActOrda2)
            {
                if (!trig4) //ANTIREPETICION
                {
                    trig4 = true;
                    Orda2.SetActive(true);
                    Orda2Manager.SetActive(true);
                    RpcSetActive(2, true);
                }
                if (Orda2Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    ActOrda2 = false;
                    ActOrda3 = true;
                }
            }
            //ORDA3 - GUSANOS GIGANTES
            if (ActOrda3)
            {
                if (!trig5) //ANTIREPETICION
                {
                    trig5 = true;
                    Orda3_1.GetComponent<Animator>().SetBool("activo", true);
                    Orda3_2.GetComponent<Animator>().SetBool("activo", true);
                }
                if (Orda3_1.GetComponent<Enemigo>().isDead && Orda3_2.GetComponent<Enemigo>().isDead)
                {
                    MunicionL1.GetComponent<BehaviourTreeOwner>().enabled = false;
                    ActOrda3 = false;
                    ActUbicacionL1 = true;
                    //PASA AL LEVEL 2
                    ActLevel1 = false;
                    ActLevel2 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 2 //////////////////////////////////////
        if (ActLevel2)
        {
            if (!trig6) //ANTIREPETICION
            {
                trig6 = true;
                //ACT/DES LEVELS
                L0.SetActive(false);
                L1.SetActive(true);
                L2.SetActive(true);
                L3.SetActive(false);
                RpcLevels(2);
            }
            //LEVEL 2/1 - LA PLATAFORMA
            //REUBICACION PLAYERS
            if (ActUbicacionL1)
            {
                if (!trig7) //ANTIREPETICION
                {
                    trig7 = true;
                    MsjServerToPlayers = 6; //REHUBICACION
                    ZonaUnicacionL1.SetActive(true);
                    RpcUbicacionSetActive(1, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL1)
                {
                    ActUbicacionL1 = false;
                    ActPlataforma = true;
                }
            }
            //ACTIVACION PLATAFORMA
            if (ActPlataforma)
            {
                if (!trig8) //ANTIREPETICION
                {
                    trig8 = true;
                    ZonaUnicacionL1.SetActive(false);
                    RpcUbicacionSetActive(1, false);
                    ComienzoyMsjLevel1.SetActive(false);
                    MsjLevel2.SetActive(true);
                    RpcActMsjLevel(2);
                    MunicionPLATAFORMA.GetComponent<BehaviourTreeOwner>().enabled = true;
                    CamaraL0.SetActive(false);
                    CamaraL1.SetActive(false);
                    CamaraL2.SetActive(true);
                    CamaraL3.SetActive(false);
                    //LEVEL 3
                    MsjServerToPlayers = 3;
                    Plataforma.GetComponent<BehaviourTreeOwner>().enabled = true;
                }
            }
            //ORDAS DE ENEMIGOS
            //ORDA4 - MOSCAS DRON
            if (ActOrda4)  //LA ACTIVA LA PLATAFORMA EN BEHAVIOR
            {
                if (!trig9) //ANTIREPETICION
                {
                    trig9 = true;
                    //ACT/DES LEVELS
                    L0.SetActive(false);
                    L1.SetActive(false);
                    L2.SetActive(true);
                    L3.SetActive(false);
                    RpcLevels(3);
                    ActPlataforma = false;
                    MsjServerToPlayers = 8; //ESCUDO
                    OnLinterna = false;
                    OnEscudo = true;
                    MsjLevel2.SetActive(false);
                    RpcActMsjLevel(3);
                    Orda4.SetActive(true);
                    Orda4Manager.SetActive(true);
                    RpcSetActive(4, true);
                }
                if (Orda4Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    MunicionPLATAFORMA.GetComponent<BehaviourTreeOwner>().enabled = false;
                    ActOrda4 = false;
                    ActOrda5 = true;
                    ActOrda5_1 = true;
                    ActOrda6 = true;
                }
            }
            //LEVEL 2/2 - ORDENADOR CENTRAL
            //ORDA5 - DRONES ESCUDO Y DRONES MINI 2
            if (ActOrda5) //ESTA ORDA CONTROLA EL TIEMPO DEL LEVEL
            {
                if (!trig10) //ANTIREPETICION
                {
                    trig10 = true;
                    MsjServerToPlayers = 5; //LEVEL 3
                    MunicionL2.GetComponent<BehaviourTreeOwner>().enabled = true;
                    Orda5.SetActive(true);
                    Orda5Manager.SetActive(true);
                    RpcSetActive(5, true);
                }
                if (Orda5Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    ActOrda5 = false;
                    Orda6_1.GetComponent<BehaviourTreeOwner>().enabled = false;
                    Orda6_2.GetComponent<BehaviourTreeOwner>().enabled = false;
                    ActOrda7 = true; //ACT ROCAS
                    ActOrdenadores = true;
                }
            }
            //ORDA5 - DROIDES 2 //IGUAL DURACIÓN O MENOS QUE LA ORDA DE CONTROL 5
            if (ActOrda5_1)
            {
                if (!trig11) //ANTIREPETICION
                {
                    trig11 = true;
                    Orda5_1.SetActive(true);
                }
                Orda5_1Manager.SetActive(true);
                RpcSetActive(51, true);
                if (Orda5_1Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    ActOrda5_1 = false;
                }
            }
            //ORDA6 - LANZALLAMAS (VIVA CONTINUAMENTE)
            if (ActOrda6)
            {
                if (!trig12) //ANTIREPETICION
                {
                    trig12 = true;
                    //Orda6.SetActive(true);
                    Orda6_1.GetComponent<BehaviourTreeOwner>().enabled = true;
                    Orda6_2.GetComponent<BehaviourTreeOwner>().enabled = true;
                    //RpcSetActive(6, true);
                }
            }
            //ORDA7 - ROCAS CAEN (VIVA CONTINUAMENTE)
            if (ActOrda7)
            {
                if (!trig13) //ANTIREPETICION
                {
                    trig13 = true;
                    Orda7.SetActive(true);
                    Orda7.GetComponent<BehaviourTreeOwner>().enabled = true;
                    RpcSetActive(7, true);
                }
            }
            //ORDENADOR CENTRAL - DESACTIVAR 6 TERMINALES (VIVA CONTINUAMENTE)
            if (ActOrdenadores)
            {
                if (!trig14) //ANTIREPETICION
                {
                    trig14 = true;
                    OrdenadorCentral.GetComponent<BehaviourTreeOwner>().enabled = true;
                    Ordenador1.SetActive(true);
                    Ordenador2.SetActive(true);
                    Ordenador3.SetActive(true);
                    Ordenador4.SetActive(true);
                    Ordenador5.SetActive(true);
                    Ordenador6.SetActive(true);
                    RpcSetActive(71, true);
                }
                if (Ordenador1.GetComponent<InteractiveObjSimple>().Activado &&
                    Ordenador2.GetComponent<InteractiveObjSimple>().Activado &&
                    Ordenador3.GetComponent<InteractiveObjSimple>().Activado &&
                    Ordenador4.GetComponent<InteractiveObjSimple>().Activado &&
                    Ordenador5.GetComponent<InteractiveObjSimple>().Activado &&
                    Ordenador6.GetComponent<InteractiveObjSimple>().Activado)
                {
                    MunicionL2.GetComponent<BehaviourTreeOwner>().enabled = false;
                    ActOrda6 = false;
                    Orda6_1.GetComponent<BehaviourTreeOwner>().enabled = false;
                    Orda6_2.GetComponent<BehaviourTreeOwner>().enabled = false;
                    ActOrda7 = false;
                    Orda7.SetActive(false);
                    Orda7.GetComponent<BehaviourTreeOwner>().enabled = false;
                    RpcSetActive(7, false);
                    Ordenador1.SetActive(false);
                    Ordenador2.SetActive(false);
                    Ordenador3.SetActive(false);
                    Ordenador4.SetActive(false);
                    Ordenador5.SetActive(false);
                    Ordenador6.SetActive(false);
                    RpcSetActive(71, false);
                    ActOrdenadores = false;
                    ActUbicacionL2 = true;
                    //PASA AL LEVEL 3
                    ActLevel2 = false;
                    ActUbicacionL2 = true;
                    ActLevel3 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 3 //////////////////////////////////////
        if (ActLevel3)
        {
            if (!trig15) //ANTIREPETICION
            {
                trig15 = true;
                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(true);
            }
            //LEVEL 3 - DESCONEXION
            //REUBICACION PLAYERS
            if (ActUbicacionL2)
            {
                if (!trig16) //ANTIREPETICION
                {
                    trig16 = true;
                    MsjServerToPlayers = 6; //REHUBICACION
                    ZonaUnicacionL2.SetActive(true);
                    RpcUbicacionSetActive(2, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL2)
                {
                    ActUbicacionL2 = false;
                    MuevePlataforma = true;
                }
            }
            //MUEVE PLATAFORMA AL LEVEL 3
            if (MuevePlataforma)
            {
                if (!trig17) //ANTIREPETICION
                {
                    trig17 = true;
                    MsjServerToPlayers = 4; //CARGA LEVEL 4
                    ZonaUnicacionL2.SetActive(false);
                    RpcUbicacionSetActive(2, false);
                    Plataforma.GetComponent<BehaviourTreeOwner>().enabled = false;
                    GraficoPlataforma.SetActive(false);
                    RpcActivePlataforma(false);
                    Plataforma.transform.position = new Vector3(Plataforma.transform.position.x, -114.62f,
                        Plataforma.transform.position.z);
                    MunicionL3.GetComponent<BehaviourTreeOwner>().enabled = true;
                    ActOrda8 = true;
                    ActOrda9 = true;
                    ActVirus = true;
                    //ACT/DES LEVELS
                    L0.SetActive(false);
                    L1.SetActive(false);
                    L2.SetActive(false);
                    L3.SetActive(true);
                    RpcLevels(4);
                }
            }
            //ORDA8 - MINI WORMS
            if (ActOrda8)
            {
                if (!trig18) //ANTIREPETICION
                {
                    trig18 = true;
                    OnLinterna = true;
                    OnEscudo = false;
                    Orda8.SetActive(true);
                    Orda8Manager.SetActive(true);
                    RpcSetActive(8, true);
                }
                if (Orda8Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    ActOrda8 = false;
                }
            }
            //ORDA9 - ALIENS
            if (ActOrda9)
            {
                if (!trig19) //ANTIREPETICION
                {
                    trig19 = true;
                    Orda9.SetActive(true);
                    Orda9Manager.SetActive(true);
                    RpcSetActive(9, true);
                }
                if (Orda9Manager.GetComponent<PoolInstantiateEnemy>().OrdaKilled)
                {
                    ActOrda9 = false;
                    //PASA AL LEVEL FIN DE JUEGO
                    ActLevel3 = false;
                }
            }
            //VIRUS - DESACTIVAR TERMINALES (VIVA CONTINUAMENTE)
            if (ActVirus)
            {
                if (!trig20) //ANTIREPETICION
                {
                    trig20 = true;
                    OrdenadorVirus.GetComponent<VirusManager>().enabled = true;
                    OrdenadorVirus.GetComponent<BehaviourTreeOwner>().enabled = true;
                    UiVirus.SetActive(true);
                    Terminales.SetActive(true);
                    RpcSetActive(91, true);
                }
                if (OrdenadorVirus.GetComponent<VirusManager>().VirusKilled)
                {
                    ActOrda8 = false;
                    Orda8.SetActive(false);
                    RpcSetActive(8, false);
                    ActOrda9 = false;
                    Orda9.SetActive(false);
                    RpcSetActive(9, false);
                    ActVirus = false;
                    Terminales.SetActive(false);
                    RpcSetActive(91, false);
                    //FIN JUEGO
                }
            }
        }
    }

    [Server]
    public void activaOrda4() //SE ACTIVA DESDE BAHAVIOR PLATAFORMA
    {
        ActOrda4 = true;
    }

    [ClientRpc]
    void RpcLevels(int L)
    {
        if (L == 0)
        {
            L0.SetActive(true);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
        }
        if (L == 1)
        {
            L0.SetActive(false);
            L1.SetActive(true);
            L2.SetActive(false);
            L3.SetActive(false);
        }
        if (L == 2)
        {
            L0.SetActive(false);
            L1.SetActive(true);
            L2.SetActive(true);
            L3.SetActive(false);
        }
        if (L == 3)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(true);
            L3.SetActive(false);
        }
        if (L == 4)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(true);
        }
    }

    [ClientRpc]
    void RpcActMsjLevel(int msL)
    {
        if (msL == 1)
        {
            ComienzoyMsjLevel1.SetActive(true);
        }
        if (msL == 2)
        {
            ComienzoyMsjLevel1.SetActive(false);
            MsjLevel2.SetActive(true);
        }
        if (msL == 3)
        {
            ComienzoyMsjLevel1.SetActive(false);
            MsjLevel2.SetActive(false);
        }
    }

    [ClientRpc]
    void RpcSetActive(int nOrda, bool act)
    {
        //LEVEL 0
        if (nOrda == 100) ZonaAdquiereAvatar.SetActive(act);
        //LEVEL 1
        if (nOrda == 1) Orda1Manager.SetActive(act);
        if (nOrda == 2) Orda2Manager.SetActive(act);
        if (nOrda == 4) Orda4Manager.SetActive(act);
        //LEVEL 2
        if (nOrda == 5) Orda5Manager.SetActive(act);
        if (nOrda == 51) Orda5_1Manager.SetActive(act);
        //if (nOrda == 6) Orda6.SetActive(act);
        if (nOrda == 7) Orda7.SetActive(act);
        if (nOrda == 71)
        {
            Ordenador1.SetActive(act);
            Ordenador2.SetActive(act);
            Ordenador3.SetActive(act);
            Ordenador4.SetActive(act);
            Ordenador5.SetActive(act);
            Ordenador6.SetActive(act);
        }
        //LEVEL 3
        if (nOrda == 8) Orda8Manager.SetActive(act);
        if (nOrda == 9) Orda9Manager.SetActive(act);
        if (nOrda == 91)
        {
            OrdenadorVirus.GetComponent<VirusManager>().enabled = true;
            UiVirus.SetActive(act);
            Terminales.SetActive(act);
        }
    }

    [ClientRpc]
    void RpcUbicacionSetActive(int nUbica, bool act)
    {
        if (nUbica == 1) ZonaUnicacionL1.SetActive(act);
        if (nUbica == 2) ZonaUnicacionL2.SetActive(act);
    }

    [ClientRpc]
    void RpcActivePlataforma(bool act)
    {
        GraficoPlataforma.SetActive(act);
    }

    [ClientRpc]
    void RpcSkybox(bool Ast)
    {
        if (Ast)
            RenderSettings.skybox = SkyboxMatPlaneta;
        else
            RenderSettings.skybox = SkyboxMatEspacio;
    }

    [Server]
    public void ControlPlayers()
    {
        PlayersEnJuego = GameObject.FindGameObjectsWithTag("Jugador");
    }

    //STOP CLIENTES
    [Server]
    public void ParaCliente1()
    {
        //if (PlayersEnJuego.Length > 0)
        //{
        NetworkIdentity opponentIdentity = PlayersEnJuego[0].GetComponent<NetworkIdentity>();
        TargetDoStop(opponentIdentity.connectionToClient, 0);
        //}
    }
    [Server]
    public void ParaCliente2()
    {
        //if (PlayersEnJuego.Length > 1)
        //{
        NetworkIdentity opponentIdentity = PlayersEnJuego[1].GetComponent<NetworkIdentity>();
        TargetDoStop(opponentIdentity.connectionToClient, 1);
        //}
    }
    [Server]
    public void ParaCliente3()
    {
        //if (PlayersEnJuego.Length == 3)
        //{
        NetworkIdentity opponentIdentity = PlayersEnJuego[2].GetComponent<NetworkIdentity>();
        TargetDoStop(opponentIdentity.connectionToClient, 2);
        //}
    }
    [Server]
    public void ParaCliente4()
    {
        //if (PlayersEnJuego.Length == 4)
        //{
        NetworkIdentity opponentIdentity = PlayersEnJuego[3].GetComponent<NetworkIdentity>();
        TargetDoStop(opponentIdentity.connectionToClient, 3);
        //}
    }
    [Server]
    public void ParaCliente5()
    {
        //if (PlayersEnJuego.Length == 5)
        //{
        NetworkIdentity opponentIdentity = PlayersEnJuego[4].GetComponent<NetworkIdentity>();
        TargetDoStop(opponentIdentity.connectionToClient, 4);
        //}
    }
    [Server]
    public void ParaCliente6()
    {
        //if (PlayersEnJuego.Length == 6)
        //{
        NetworkIdentity opponentIdentity = PlayersEnJuego[5].GetComponent<NetworkIdentity>();
        TargetDoStop(opponentIdentity.connectionToClient, 5);
        //}
    }
    [TargetRpc]
    public void TargetDoStop(NetworkConnection target, int cliente)
    {
        target.Disconnect();
        PlayersEnJuego[cliente].GetComponent<NetworkManager>().StopClient();
        PlayersEnJuego[cliente].GetComponent<NetworkManagerHUD>().QuestC = false;
    }

}
