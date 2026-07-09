using UnityEngine;
using TMPro;
using Mirror;
using NodeCanvas.BehaviourTrees;

public class JuegoManager : NetworkBehaviour
{
    [Header("MARKETING")]
    public bool isMARKETING;

    [Header("BLOBAL SETTINGS")]
    [SyncVar]
    public float ScalaMovGlobal;

    [Header("GLOBALES")]
    public float TiempoPartidaTotal;
    [SyncVar]
    public float TiempoPartida;
    [SyncVar]
    public bool GameOver;
    [SerializeField]
    private GameObject GameOverServidor;
    //[SerializeField]
    //private TextMeshPro textTiempo;
    [SerializeField]
    private TextMeshProUGUI textTiempoUI;
    [SerializeField]
    private TextMeshPro textTiempoNivel0;
    [SerializeField]
    private TextMeshPro textTiempoNivel1;
    [SerializeField]
    private TextMeshPro textTiempoNivel2;
    [SerializeField]
    private TextMeshPro textTiempoNivel3;
    [SerializeField]
    private TextMeshPro textTiempoNivel4;
    [SerializeField]
    private TextMeshPro textTiempoNivel5;
    [SerializeField]
    private TextMeshPro textTiempoNivel61;
    [SerializeField]
    private TextMeshPro textTiempoNivel62;

    [SerializeField]
    private GameObject CamaraL0, CamaraL1, CamaraL2, CamaraL3, CamaraL4, CamaraL5, CamaraL6;
    [SerializeField]
    private GameObject CamaraL7, CamaraL8, CamaraL9, CamaraL10, CamaraL11, CamaraL12, CamaraL13;
    public GameObject[] PlayersEnJuego;
    [SerializeField]
    private bool jugando;
    [SerializeField]
    private GameObject BotonStop;
    [SerializeField]
    private int Jugadores;
    [SerializeField]
    private bool JuegoCompletado;

    /*[Header("SKYBOXS")]
    [SerializeField]
    private Material SkybArriba;
    [SerializeField]
    private Material SkybAbajo;*/

    [Header("MENSAJES")]
    [SyncVar]
    public int MsjServerToPlayers;

    [Header("COMIENZO Y CARTELES NIVELES")]
    [SerializeField]
    private GameObject ComienzoyMsjLevel1;
    //[SerializeField]
    //private GameObject ComienzoyMsjLevel2;

    [Header("LEVELS")]
    [SerializeField]
    private GameObject L0;
    [SerializeField]
    private GameObject L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13;

    [Header("PLATAFORMA")]
    [SerializeField]
    private GameObject Plataforma;
    [SerializeField]
    private GameObject LimitesRectangular, LimitesCircular;
    [SerializeField]
    private GameObject Helicoptero;
    [SerializeField]
    private GameObject CaidaCircular;
    //[SerializeField]
    //private GameObject Behav_Mov_A_L_S;

    /*[Header("TAPA A NEGRO BEHAVIOURS")]
    [SerializeField]
    private BehaviourTreeOwner BehavTapaMuevePASARELAS; // SOLO EN SERVER
    [SerializeField]
    private BehaviourTreeOwner BehavTapaMuevePLAZA; // SOLO EN SERVER
    */
    [Header("LEVEL 0 - INICIO")]
    [SyncVar]
    public bool ActLevel0;
    [SyncVar]
    public bool ActAdquiereAvatar;
    [SerializeField]
    private GameObject ZonaAdquiereAvatar;
    public int PlayersUbicadosL0;

    [Header("LEVEL 1 - HANGAR")]
    [SerializeField]
    private GameObject Level_1_Manager;
    [SyncVar]
    public bool ActLevel1;
    [Header("          REUBICACION A L2")]
    [SyncVar]
    public bool ActUbicacionL1;
    [SerializeField]
    private GameObject ZonaUnicacionL1;
    public int PlayersUbicadosL1;

    [Header("LEVEL 2 - BASE")]
    [SerializeField]
    private GameObject Level_2_Manager;
    [SyncVar]
    public bool ActLevel2;
    [Header("          REUBICACION A L3")]
    [SyncVar]
    public bool ActUbicacionL2;
    [SerializeField]
    private GameObject ZonaUnicacionL2;
    public int PlayersUbicadosL2;

    [Header("LEVEL 3 - PUERTA")]
    [SerializeField]
    private GameObject Level_3_Manager;
    [SyncVar]
    public bool ActLevel3;
    [Header("          REUBICACION A L4")]
    [SyncVar]
    public bool ActUbicacionL3;
    [SerializeField]
    private GameObject ZonaUnicacionL3;
    public int PlayersUbicadosL3;

    [Header("LEVEL 4 - ")]
    [SerializeField]
    private GameObject Level_4_Manager;
    [SyncVar]
    public bool ActLevel4;
    [Header("          REUBICACION A L5")]
    [SyncVar]
    public bool ActUbicacionL4;
    [SerializeField]
    private GameObject ZonaUnicacionL4;
    public int PlayersUbicadosL4;

    [Header("LEVEL 5 - ")]
    [SerializeField]
    private GameObject Level_5_Manager;
    [SyncVar]
    public bool ActLevel5;
    [Header("          REUBICACION A L6")]
    [SyncVar]
    public bool ActUbicacionL5;
    [SerializeField]
    private GameObject ZonaUnicacionL5;
    public int PlayersUbicadosL5;

    [Header("LEVEL 6 - TRAMPAS 2 NIVELES")]
    [SerializeField]
    private GameObject Level_6_Manager;
    [SerializeField]
    private GameObject BehavSubirNivel2, PlataformaRectangular;
    [SyncVar]
    public bool ActLevel6;

    [Header("          REUBICACION A L7")]
    [SyncVar]
    public bool ActUbicacionL6_1;
    [SyncVar]
    public bool ActUbicacionL6_2;
    [SerializeField]
    private GameObject ZonaUnicacionL6_1, ZonaUnicacionL6_2;
    public int PlayersUbicadosL6_1, PlayersUbicadosL6_2;

        [Header("--------FINAL PRIMERA PARTE--------")]
    [SyncVar]
    public bool ActContinuara;

    [Header("LEVEL 7 - TRAMPAS PINCHOS")]
    [SerializeField]
    private GameObject Level_7_Manager;
    [SyncVar]
    public bool ActLevel7;
    [Header("          REUBICACION A L8")]
    [SyncVar]
    public bool ActUbicacionL7_1;
    [SyncVar]
    public bool ActUbicacionL7_2;
    [SerializeField]
    private GameObject ZonaUnicacionL7_1, ZonaUnicacionL7_2;
    public int PlayersUbicadosL7_1, PlayersUbicadosL7_2;

    [Header("LEVEL 8 - ")]
    [SerializeField]
    private GameObject Level_8_Manager;
    [SyncVar]
    public bool ActLevel8;
    [Header("          REUBICACION A L9")]
    [SyncVar]
    public bool ActUbicacionL8;
    [SerializeField]
    private GameObject ZonaUnicacionL8;
    public int PlayersUbicadosL8;

    [Header("LEVEL 9 - ")]
    [SerializeField]
    private GameObject Level_9_Manager;
    [SyncVar]
    public bool ActLevel9;
    [Header("          REUBICACION A L10")]
    [SyncVar]
    public bool ActUbicacionL9_1;
    [SyncVar]
    public bool ActUbicacionL9_2;
    [SerializeField]
    private GameObject ZonaUnicacionL9_1, ZonaUnicacionL9_2;
    public int PlayersUbicadosL9_1, PlayersUbicadosL9_2;

    [Header("LEVEL 10 - ")]
    [SerializeField]
    private GameObject Level_10_Manager;
    [SerializeField]
    private GameObject BehavCaida, PlataformaCircular;
    [SyncVar]
    public bool ActLevel10;
    [Header("          REUBICACION A L11")]
    [SyncVar]
    public bool ActUbicacionL10;
    [SerializeField]
    private GameObject ZonaUnicacionL10;
    public int PlayersUbicadosL10;

    [Header("LEVEL 11 - ")]
    [SerializeField]
    private GameObject Level_11_Manager;
    [SerializeField]
    private GameObject PathCueva;
    //[SerializeField]
    //private GameObject FuegoGusano;
    [SyncVar]
    public bool ActLevel11;
    [Header("          REUBICACION A L12")]
    [SyncVar]
    public bool ActUbicacionL11;
    [SerializeField]
    private GameObject ZonaUnicacionL11;
    public int PlayersUbicadosL11;

    [Header("LEVEL 12 - ")]
    [SerializeField]
    private GameObject Level_12_Manager;
    [SyncVar]
    public bool ActLevel12;
    [Header("          REUBICACION A L13")]
    [SyncVar]
    public bool ActUbicacionL12;
    [SerializeField]
    private GameObject ZonaUnicacionL12;
    public int PlayersUbicadosL12;

    [Header("LEVEL 13 - FINAL")]
    [SerializeField]
    private GameObject Level_13_Manager;
    [SyncVar]
    public bool ActLevel13;
    /*[Header("          REUBICACION A FINAL")]
    [SyncVar]
    public bool ActUbicacionL13;
    [SerializeField]
    private GameObject ZonaUnicacionL13;
    public int PlayersUbicadosL13;*/

    [Header("MUEVE PLATAFORMA A NIVELES")]
    [SerializeField]
    private GameObject BehavMueveL11;

    [Header("LEE B.D ZONAS CAPTURA")]
    [SerializeField]
    private GameObject ZonaCapturaUsers;
    [SerializeField]
    private GameObject ZonaCapturaUsersBD;
    public int PlayersUbicadosCapturaUser;

    [Header("GRABA B.D")]
    [SerializeField]
    private GameObject GrabaBD;

    private bool trigActAdquiereAvatar;
    private bool trigL0, trigL1, trigL2, trigL3, trigL4, trigL5, trigL6, trigL7, trigL8, trigL9, trigL10, trigL11, trigL12, trigL13;
    private bool trigZU1, trigZU2, trigZU3, trigZU4, trigZU5, trigZU6_1, trigZU6_2, trigZU7_1, trigZU7_2, trigZU8, trigZU9_1, trigZU9_2, trigZU10, trigZU11, trigZU12;
    private bool trigGameover = false;
    private float timer = 0f;

    void Start()
    {
        //LEE LA ESCALA DE GLOBAL SETTINGS
        Escala();

        /*L0.SetActive(true);
        L1.SetActive(false);
        RpcLevels(0);*/

        TiempoPartida = TiempoPartidaTotal;
        ActLevel0 = true;

        MsjServerToPlayers = 1; //capturaPlayers

        Invoke(nameof(SetStates), 3f);
    }
    private void SetStates()
    {
        L0.SetActive(true);
        L1.SetActive(false);
        L2.SetActive(false);
        L3.SetActive(false);
        L4.SetActive(false);
        L5.SetActive(false);
        L6.SetActive(false);
        L7.SetActive(false);
        L8.SetActive(false);
        L9.SetActive(false);
        L10.SetActive(false);
        L11.SetActive(false);
        L12.SetActive(false);
        L13.SetActive(false);
    }

    [Server]
    private void Escala()
    {
        //LEE LA ESCALA DE GLOBAL SETTINGS
        ScalaMovGlobal = PlayerPrefs.GetFloat("SettingsScale", 1.4f);
    }

    [ServerCallback]
    void Update()
    {
        if (jugando) TiempoPartida -= Time.deltaTime;
        if (JuegoCompletado)
        {
            GameOver = true;
            jugando = false;

            if (!trigGameover)
            {
                trigGameover = true;
                MsjServerToPlayers = 16; //FIN DEL JUEGO MISIÓN CUMPLIDA, PUNTOS EXTRA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                }
                if (!GameOverServidor.activeInHierarchy)
                    GameOverServidor.SetActive(true); //SCORE EN SERVIDOR
                // GRABA B.D. 1 VEZ
                GrabaBD.GetComponent<GrabaBd>().GrabaDatosBd();
            }
        }
        if (TiempoPartida <= 0)
        {
            TiempoPartida = 0;
            GameOver = true;
            jugando = false;
            
            if (!trigGameover)
            {
                trigGameover = true;
                MsjServerToPlayers = 17; //FIN DEL JUEGO - FINALIZÓ EL TIEMPO
                if (!GameOverServidor.activeInHierarchy)
                    GameOverServidor.SetActive(true); //SCORE EN SERVIDOR
                // GRABA B.D. 1 VEZ
                GrabaBD.GetComponent<GrabaBd>().GrabaDatosBd();
            }
        }
        manager();
        ControlPlayers(); //QUE SOLO SE EJECUTE CON LA GUI DE PLAYERS ACTIVA

        //TIEMPO TOTAL
        int tp = (int)TiempoPartida;
        int minutos = tp / 60;
        int segundos = tp - (minutos * 60);
        //textTiempo.text = tp.ToString();
        textTiempoUI.text = minutos.ToString("00") + ":" + segundos.ToString("00");
        textTiempoNivel0.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    void LateUpdate()
    {
        ActualizarRelojes();
    }

    private void ActualizarRelojes()
    {
        // TIEMPO TOTAL
        int tp = (int)Mathf.Max(0, TiempoPartida); // Max(0) para evitar que muestre -1 por un frame
        int minutos = tp / 60;
        int segundos = tp % 60; // Usar el módulo (%) es un poco más limpio y eficiente

        string tiempoFormateado = minutos.ToString("00") + ":" + segundos.ToString("00");

        if (textTiempoNivel0 != null)
            textTiempoNivel0.text = tiempoFormateado;
        if (textTiempoNivel1 != null)
            textTiempoNivel1.text = tiempoFormateado;
        if (textTiempoNivel2 != null)
            textTiempoNivel2.text = tiempoFormateado;
        if (textTiempoNivel3 != null)
            textTiempoNivel3.text = tiempoFormateado;
        if (textTiempoNivel4 != null)
            textTiempoNivel4.text = tiempoFormateado;
        if (textTiempoNivel5 != null)
            textTiempoNivel5.text = tiempoFormateado;
        if (textTiempoNivel61 != null)
            textTiempoNivel61.text = tiempoFormateado;
        if (textTiempoNivel62 != null)
            textTiempoNivel62.text = tiempoFormateado;
    }

    [Server]
    void paraPartida()
    {
        BotonStop.GetComponent<StopPartida>().ParaPartida();
    }

    [Server]
    public void capturaPlayers() //LO LLAMA EL BOTON PLAYERS, SOLO UNA VEZ
    {
        if (!jugando && !GameOver)
        {
            ZonaCapturaUsers.SetActive(false); //INTEGRACION
            ZonaCapturaUsersBD.SetActive(false);
            RpcSetActive(1000, false);
        }
    }

    [Server]
    public void jugar() //LO LLAMA EL BOTON READY
    {
        if (!jugando)
        {
            ActAdquiereAvatar = true;
            jugando = true;
        }
    }

    /*[Server]
    public void StartHANGAR() //START HANGAR... LO LLAMA CUENTA ATRAS DEL MENSAJE LEVEL 1 
    {
        Jugadores = PlayersEnJuego.Length; // PARA QUE EL NUMERO SEA FIJO DESDE EL PRINCIPIO
        trigZ1 = true;
    }*/

    [Server]
    public void Fin()  // LA ACTIVA LA LLEGADA DEL TREN o Zona2Manager o TiempoJuego
    {
        // FIN 
        TiempoPartida = 0;
    }

    [Server]
    public void manager()
    {
        //////////////////////////////////////////////  LEVEL 0 /////////////////////////////////////
        if (ActLevel0)
        {
            //ACT/DES LEVELS
            if (!trigL0)
            {
                Debug.Log("L0");
                L0.SetActive(true);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(0);

                /*
                // A NEGRO
                for (int i = 0; i < PlayersEnJuego.Length; i++)  //PONE TODO EN NEGRO
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().aNegro = true;
                }
                */

                CamaraL0.SetActive(true);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                //POSICIONA PLATAFORMA L0
                //Plataforma.transform.position = new Vector3(-15.5f, 0f, 0f);

                trigL0 = true;
            }
             
            if (ActAdquiereAvatar) // LO ACTIVA LA FUNCION JUGAR() EN ESTE SCRIPT
            {
                if (!trigActAdquiereAvatar)
                {
                    MsjServerToPlayers = 2; //REUBICACION
                    ZonaAdquiereAvatar.SetActive(true);
                    ZonaCapturaUsersBD.SetActive(false);
                    RpcSetActive(100, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL0)
                {
                    ActLevel0 = false;
                    ActAdquiereAvatar = false;
                    ZonaAdquiereAvatar.SetActive(false);
                    RpcSetActive(100, false);
                    ActLevel1 = true;
                    //ActLevel10 = true; 

                    //CUENTA ATRAS PARA COMIENZO DEL JUEGO L1
                    //ComienzoyMsjLevel1.SetActive(true);
                    //ComienzoyMsjLevel1.GetComponentInParent<TiempoComienzo>().enabled = true;
                    //RpcActMsjLevel(1);
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 1 - HANGAR //////////////////////////////////////
        if (ActLevel1)
        {
            if (!trigL1)
            {
                trigL1 = true;

                Jugadores = PlayersEnJuego.Length; // PARA QUE EL NUMERO SEA FIJO DESDE EL PRINCIPIO

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                //MUEVE PLATAFORMA A L1
                Vector3 newPos = new Vector3(300f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("HANGAR");
                L0.SetActive(false);
                L1.SetActive(true);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(true);
                RpcLevels(1);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(true);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 4; // L1
            }

            if (Level_1_Manager.GetComponent<Manager_L1>().Objetos_Guardados_OK)
            {
                ActUbicacionL1 = true;
            }

            //REUBICACION PLAYERS
            if (ActUbicacionL1) 
            {
                if (!trigZU1)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU1 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL1.SetActive(true);
                    RpcUbicacionSetActive(1, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL1)
                {
                    ActUbicacionL1 = false;
                    ActLevel1 = false;
                    
                    ZonaUnicacionL1.SetActive(false);
                    RpcUbicacionSetActive(1, false);

                    ActLevel2 = true;
                }
            }
        }

        /////////////////////////////////////////////  LEVEL 2 - BASE //////////////////////////////////
        if (ActLevel2)
        {
            if (!trigL2)
            {
                trigL2 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(600f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("BASE");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(true);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(2);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(true);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 5; // LEVEL 2
            }

            if (Level_2_Manager.GetComponent<Manager_L2>().Puerta_Abierta)
            {
                ActUbicacionL2 = true;
            }

            //REUBICACION PLAYERS
            if (ActUbicacionL2)
            {
                if (!trigZU2)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU2 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL2.SetActive(true);
                    RpcUbicacionSetActive(2, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL2)
                {
                    ActUbicacionL2 = false;
                    ActLevel2 = false;

                    ZonaUnicacionL2.SetActive(false);
                    RpcUbicacionSetActive(2, false);

                    ActLevel3 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 3 - GEMAS AZULES //////////////////////////////////
        if (ActLevel3)
        {
            if (!trigL3)
            {
                trigL3 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(900f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("GEMAS AZULES");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(true);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(3);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(true);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 6; // LEVEL 3
            }

            if (Level_3_Manager.GetComponent<Manager_L3>().Esferas_Azules_Ok)
            {
                ActUbicacionL3 = true;
            }

            //REUBICACION PLAYERS
            if (ActUbicacionL3)
            {
                if (!trigZU3)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU3 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL3.SetActive(true);
                    RpcUbicacionSetActive(3, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL3)
                {
                    ActUbicacionL3 = false;
                    ActLevel3 = false;

                    ZonaUnicacionL3.SetActive(false);
                    RpcUbicacionSetActive(3, false);

                    ActLevel4 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 4 - ANTORCHAS //////////////////////////////////
        if (ActLevel4)
        {
            if (!trigL4)
            {
                trigL4 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = true;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(1200f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("ANTORCHAS");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(true);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(4);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(true);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 7; // LEVEL 4
            }

            if (Level_4_Manager.GetComponent<Manager_L4>().Antorchas_Encendidas_Ok)
            {
                ActUbicacionL4 = true;
            }

            //REUBICACION PLAYERS
            if (ActUbicacionL4)
            {
                if (!trigZU4)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU4 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL4.SetActive(true);
                    RpcUbicacionSetActive(4, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL4)
                {
                    ActUbicacionL4 = false;
                    ActLevel4 = false;

                    ZonaUnicacionL4.SetActive(false);
                    RpcUbicacionSetActive(4, false);

                    ActLevel5 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 5 - RAYOS //////////////////////////////////
        if (ActLevel5)
        {
            if (!trigL5)
            {
                trigL5 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(1500f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("RAYOS");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(true);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(5);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(true);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 8; // LEVEL 5
            }

            if (Level_5_Manager.GetComponent<Manager_L5>().Esfera_Rayo_Ok)
            {
                ActUbicacionL5 = true;
            }

            //REUBICACION PLAYERS
            if (ActUbicacionL5)
            {
                if (!trigZU5)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU5 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL5.SetActive(true);
                    RpcUbicacionSetActive(5, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL5)
                {
                    ActUbicacionL5 = false;
                    ActLevel5 = false;

                    ZonaUnicacionL5.SetActive(false);
                    RpcUbicacionSetActive(5, false);

                    ActLevel6 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 6 - TRAMPAS 2 NIVELES //////////////////////////////////
        if (ActLevel6)
        {
            if (!trigL6)
            {
                trigL6 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(1800f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("TRAMPAS 2 NIVELES");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(true);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(true);
                Helicoptero.SetActive(false);
                RpcLevels(6);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(true);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 9; // LEVEL 6
            }

            //if (Level_6_Manager.GetComponent<Manager_L6>().Esfera_Rayo_Ok)
            //{
                ActUbicacionL6_1 = true;
            //}

            //REUBICACION PLAYERS
            //IDA
            if (ActUbicacionL6_1)
            {
                if (!trigZU6_1)
                {
                    trigZU6_1 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    ZonaUnicacionL6_1.SetActive(true);
                    RpcUbicacionSetActive(61, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL6_1)
                {
                    ActUbicacionL6_1 = false;

                    ZonaUnicacionL6_1.SetActive(false);
                    RpcUbicacionSetActive(61, false);

                    ActUbicacionL6_2 = true;

                    BehavSubirNivel2.SetActive(true); //SUBE LA PLATAFORMA AL NIVEL SUPERIOR
                }
            }
            //VUELTA
            if (ActUbicacionL6_2)
            {
                if (!trigZU6_2)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU6_2 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL6_2.SetActive(true);
                    RpcUbicacionSetActive(62, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL6_2)
                {
                    ActUbicacionL6_2 = false;
                    ActLevel6 = false;

                    ZonaUnicacionL6_2.SetActive(false);
                    RpcUbicacionSetActive(62, false);

                    ////////////////////////////////////////////////////////////////////////////////// AQUI VA EL CONTINUARÁ
                    ActContinuara = true;

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                    }

                    //ActLevel7 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 7 - TRAMPAS PINCHOS //////////////////////////////////
        if (ActLevel7)
        {
            if (!trigL7)
            {
                trigL7 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(2100f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("TRAMPAS PINCHOS");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(true);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(7);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(true);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 10; // LEVEL 7
            }

            //if (Level_7_Manager.GetComponent<Manager_L7>().Esfera_Rayo_Ok)
            //{
                ActUbicacionL7_1 = true;
            //}

            //REUBICACION PLAYERS
            //IDA
            if (ActUbicacionL7_1)
            {
                if (!trigZU7_1)
                {
                    trigZU7_1 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    ZonaUnicacionL7_1.SetActive(true);
                    RpcUbicacionSetActive(71, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL7_1)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    ActUbicacionL7_1 = false;
                    ActLevel7 = false;

                    ZonaUnicacionL7_1.SetActive(false);
                    RpcUbicacionSetActive(71, false);

                    //ActUbicacionL7_2 = true;
                    ActLevel8 = true;
                }
            }
            //VUELTA
            /*if (ActUbicacionL7_2)
            {
                if (!trigZU7_2)
                {
                    trigZU7_2 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL7_2.SetActive(true);
                    RpcUbicacionSetActive(72, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL7_2)
                {
                    ActUbicacionL7_2 = false;
                    ActLevel7 = false;

                    ZonaUnicacionL7_2.SetActive(false);
                    RpcUbicacionSetActive(72, false);

                    ActLevel8 = true;
                }
            }*/
        }
        /////////////////////////////////////////////  LEVEL 8 - CAMINO COLUMNAS //////////////////////////////////
        if (ActLevel8)
        {
            if (!trigL8)
            {
                trigL8 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(2400f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("RAYOS");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(true);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(8);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(true);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 11; // LEVEL 8
            }

            //if (Level_8_Manager.GetComponent<Manager_L8>().Esfera_Rayo_Ok)
            //{
                ActUbicacionL8 = true;
            //}

            //REUBICACION PLAYERS
            if (ActUbicacionL8)
            {
                if (!trigZU8)
                {
                    trigZU8 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL8.SetActive(true);
                    RpcUbicacionSetActive(8, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL8)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    ActUbicacionL8 = false;
                    ActLevel8 = false;

                    ZonaUnicacionL8.SetActive(false);
                    RpcUbicacionSetActive(8, false);

                    ActLevel9 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 9 - LABERINTO //////////////////////////////////
        if (ActLevel9)
        {
            if (!trigL9)
            {
                trigL9 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(2700f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("LABERINTO");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(true);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(9);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(true);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 12; // LEVEL 9
            }

            //if (Level_9_Manager.GetComponent<Manager_L9>().Esfera_Rayo_Ok)
            //{
            ActUbicacionL9_1 = true;
            //}

            //REUBICACION PLAYERS
            //IDA
            if (ActUbicacionL9_1)
            {
                if (!trigZU9_1)
                {
                    trigZU9_1 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    ZonaUnicacionL9_1.SetActive(true);
                    RpcUbicacionSetActive(91, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL9_1)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    ActUbicacionL9_1 = false;
                    ActLevel9 = false;

                    ZonaUnicacionL9_1.SetActive(false);
                    RpcUbicacionSetActive(91, false);

                    //ActUbicacionL9_2 = true;
                    ActLevel10 = true;
                }
            }
            //VUELTA
            /*if (ActUbicacionL9_2)
            {
                if (!trigZU9_2)
                {
                    trigZU9_2 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL9_2.SetActive(true);
                    RpcUbicacionSetActive(92, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL9_2)
                {
                    ActUbicacionL9_2 = false;
                    ActLevel9 = false;

                    ZonaUnicacionL9_2.SetActive(false);
                    RpcUbicacionSetActive(92, false);

                    ActLevel10 = true;
                }
            }*/
        }
        /////////////////////////////////////////////  LEVEL 10 - CAIDA //////////////////////////////////
        if (ActLevel10)
        {
            if (!trigL10)
            {
                trigL10 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = true;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                Vector3 newPos = new Vector3(3000f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("CAIDA");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(true);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(false);
                LimitesCircular.SetActive(true);
                PlataformaCircular.SetActive(true);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(10);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(true);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 13; // LEVEL 10
            }

            //if (Level_10_Manager.GetComponent<Manager_L10>().Esfera_Rayo_Ok)
            //{
            ActUbicacionL10 = true;
            //}

            //REUBICACION PLAYERS
            if (ActUbicacionL10)
            {
                if (!trigZU10)
                {
                    trigZU10 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL10.SetActive(true);
                    RpcUbicacionSetActive(10, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL10)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    ActUbicacionL10 = false;
                    ActLevel10 = false;

                    ZonaUnicacionL10.SetActive(false);
                    RpcUbicacionSetActive(10, false);

                    BehavCaida.SetActive(true); //CAIDA DE LA PLATAFORMA AL LEVEL 11

                    //ACTIVA CAIDA PLATAFORMA CIRCULAR
                    CaidaCircular.SetActive(true);

                    //ActLevel11 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 11 - CUEVA //////////////////////////////////
        if (ActLevel11)
        {
            if (!trigL11)
            {
                trigL11 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = true;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                BehavMueveL11.SetActive(true);
                //Vector3 newPos = new Vector3(3300f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                //Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                //Plataforma.transform.position = newPos;

                Debug.Log("CUEVA");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(true);
                L12.SetActive(false);
                L13.SetActive(false);
                LimitesRectangular.SetActive(false);
                LimitesCircular.SetActive(true);
                PlataformaCircular.SetActive(true);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);

                //FuegoGusano.SetActive(false);

                RpcLevels(11);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(true);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 14; // LEVEL 11

                //ACTIVA PATH
                PathCueva.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
            }

            //if (Level_10_Manager.GetComponent<Manager_L10>().Esfera_Rayo_Ok)
            //{
            //ActUbicacionL11 = true;
            //}

            //REUBICACION PLAYERS
            if (ActUbicacionL11)
            {
                if (!trigZU11)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    trigZU11 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    //DESACTIVA CAIDA PLATAFORMA CIRCULAR
                    CaidaCircular.SetActive(false);

                    ZonaUnicacionL11.SetActive(true);
                    LimitesRectangular.SetActive(true);
                    LimitesCircular.SetActive(false);
                    RpcUbicacionSetActive(11, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL11)
                {
                    ActUbicacionL11 = false;
                    ActLevel11 = false;

                    ZonaUnicacionL11.SetActive(false);
                    RpcUbicacionSetActive(11, false);

                    ActLevel12 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 12 - SALTOS //////////////////////////////////
        if (ActLevel12)
        {
            if (!trigL12)
            {
                trigL12 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //BehavIrL12.SetActive(true); //LLEVA LA PLATAFORMA AL LEVEL 12
                Vector3 newPos = new Vector3(3600f, 0f, 0f);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("SALTOS");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(true);
                L13.SetActive(false);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(12);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(true);
                CamaraL13.SetActive(false);

                MsjServerToPlayers = 15; // LEVEL 12
            }

            //if (Level_10_Manager.GetComponent<Manager_L10>().Esfera_Rayo_Ok)
            //{
                ActUbicacionL12 = true;
            //}

            //REUBICACION PLAYERS
            if (ActUbicacionL12)
            {
                if (!trigZU12)
                {
                    trigZU12 = true;

                    MsjServerToPlayers = 2; //REUBICACION

                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().PlayerPuntos += 1000;
                    }

                    ZonaUnicacionL12.SetActive(true);
                    RpcUbicacionSetActive(12, true);
                }
                if (PlayersEnJuego.Length == PlayersUbicadosL12)
                {
                    //REINICIA FADEINOUT
                    for (int i = 0; i < PlayersEnJuego.Length; i++)
                    {
                        PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = false;
                    }

                    ActUbicacionL12 = false;
                    ActLevel12 = false;

                    ZonaUnicacionL12.SetActive(false);
                    RpcUbicacionSetActive(12, false);

                    ActLevel13 = true;
                }
            }
        }
        /////////////////////////////////////////////  LEVEL 13 - TESORO FINAL //////////////////////////////////
        if (ActLevel13)
        {
            if (!trigL13)
            {
                trigL13 = true;

                //FADEINOUT
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().FadeInFadeOut = true;
                }

                //ANTORCHA
                for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnLinterna = false;
                }

                //PISTOLA
                /*for (int i = 0; i < PlayersEnJuego.Length; i++)
                {
                    PlayersEnJuego[i].GetComponentInParent<Player>().OnPistola = false;
                }*/

                //MUEVE AL SIGUIENTE LEVEL +300
                //Behav_Mov_A_L_S.SetActive(true);
                Vector3 newPos = new Vector3(3900f, Plataforma.transform.position.y, Plataforma.transform.position.z);
                Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, newPos, 300f);
                Plataforma.transform.position = newPos;

                Debug.Log("TESORO FINAL");
                L0.SetActive(false);
                L1.SetActive(false);
                L2.SetActive(false);
                L3.SetActive(false);
                L4.SetActive(false);
                L5.SetActive(false);
                L6.SetActive(false);
                L7.SetActive(false);
                L8.SetActive(false);
                L9.SetActive(false);
                L10.SetActive(false);
                L11.SetActive(false);
                L12.SetActive(false);
                L13.SetActive(true);
                LimitesRectangular.SetActive(true);
                LimitesCircular.SetActive(false);
                PlataformaCircular.SetActive(false);
                PlataformaRectangular.SetActive(false);
                Helicoptero.SetActive(false);
                RpcLevels(13);

                CamaraL0.SetActive(false);
                CamaraL1.SetActive(false);
                CamaraL2.SetActive(false);
                CamaraL3.SetActive(false);
                CamaraL4.SetActive(false);
                CamaraL5.SetActive(false);
                CamaraL6.SetActive(false);
                CamaraL7.SetActive(false);
                CamaraL8.SetActive(false);
                CamaraL9.SetActive(false);
                CamaraL10.SetActive(false);
                CamaraL11.SetActive(false);
                CamaraL12.SetActive(false);
                CamaraL13.SetActive(true);

                MsjServerToPlayers = 16; // LEVEL 13 - JUEGO COMPLETADO CON EXITO

                JuegoCompletado = true;
                //FINAL
            }
        }

    }
    
    [Server]
    public void ActivaL11() // LO ACTIVA EL BAHAVIOUR DE LA CAIDA
    {
        ActLevel11 = true;
    }
    [Server]
    public void ActivaUbicaL11() // LO ACTIVA EL BAHAVIOUR DEL PATH DE LA CUEVA
    {
        ActUbicacionL11 = true;
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
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 1)
        {
            L0.SetActive(false);
            L1.SetActive(true);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(true);
        }
        if (L == 2)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(true);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 3)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(true);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 4)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(true);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 5)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(true);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 6)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(true);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(true);
            Helicoptero.SetActive(false);
        }
        if (L == 7)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(true);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 8)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(true);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 9)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(true);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 10)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(true);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(false);
            LimitesCircular.SetActive(true);
            PlataformaCircular.SetActive(true);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 11)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(true);
            L12.SetActive(false);
            L13.SetActive(false);
            LimitesRectangular.SetActive(false);
            LimitesCircular.SetActive(true);
            PlataformaCircular.SetActive(true);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);

            //FuegoGusano.SetActive(false);
        }
        if (L == 12)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(true);
            L13.SetActive(false);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
        if (L == 13)
        {
            L0.SetActive(false);
            L1.SetActive(false);
            L2.SetActive(false);
            L3.SetActive(false);
            L4.SetActive(false);
            L5.SetActive(false);
            L6.SetActive(false);
            L7.SetActive(false);
            L8.SetActive(false);
            L9.SetActive(false);
            L10.SetActive(false);
            L11.SetActive(false);
            L12.SetActive(false);
            L13.SetActive(true);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
            PlataformaCircular.SetActive(false);
            PlataformaRectangular.SetActive(false);
            Helicoptero.SetActive(false);
        }
    }

    [ClientRpc]
    void RpcActMsjLevel(int msL)
    {
        if (msL == 1)
        {
            ComienzoyMsjLevel1.SetActive(true);
            ComienzoyMsjLevel1.GetComponentInParent<TiempoComienzo>().enabled = true;
        }
    }
    
    [ClientRpc]
    void RpcSetActive(int n, bool act)
    {
        //ZONAS CAPTURA USER
        if (n == 1000) ZonaCapturaUsers.SetActive(act);
        //LEVEL 0
        if (n == 100) ZonaAdquiereAvatar.SetActive(act);
    }

    [ClientRpc]
    void RpcUbicacionSetActive(int nUbica, bool act)
    {
        if (nUbica == 1) ZonaUnicacionL1.SetActive(act);
        if (nUbica == 2) ZonaUnicacionL2.SetActive(act);
        if (nUbica == 3) ZonaUnicacionL3.SetActive(act);
        if (nUbica == 4) ZonaUnicacionL4.SetActive(act);
        if (nUbica == 5) ZonaUnicacionL5.SetActive(act);
        if (nUbica == 61) ZonaUnicacionL6_1.SetActive(act);
        if (nUbica == 62) ZonaUnicacionL6_2.SetActive(act);
        if (nUbica == 71) ZonaUnicacionL7_1.SetActive(act);
        if (nUbica == 72) ZonaUnicacionL7_2.SetActive(act);
        if (nUbica == 8) ZonaUnicacionL8.SetActive(act);
        if (nUbica == 91) ZonaUnicacionL9_1.SetActive(act);
        if (nUbica == 92) ZonaUnicacionL9_2.SetActive(act);
        if (nUbica == 10) ZonaUnicacionL10.SetActive(act);
        if (nUbica == 11)
        {
            ZonaUnicacionL11.SetActive(act);
            LimitesRectangular.SetActive(true);
            LimitesCircular.SetActive(false);
        }
        if (nUbica == 12) ZonaUnicacionL12.SetActive(act);
    }

    /*[ClientRpc]
    void RpcFuegosFinal()
    {
        FuegosArtificiales.GetComponent<ParticleSystem>().Play();
    }*/

    [Server]
    public void ControlPlayers()
    {
        PlayersEnJuego = GameObject.FindGameObjectsWithTag("Jugador");
    }
}
