using UnityEngine;
using System.Collections;
using Mirror;
using TMPro;
using NodeCanvas.BehaviourTrees;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public GameObject camaraServer;
    [Header("GLOBALES")]
    [SyncVar(hook = nameof(SetName))]
    public string Name = "NoName";
    [SyncVar(hook = nameof(SetTeam))]
    public string Team = "NoTeam";
    [SyncVar(hook = nameof(SetVida))]
    public int PlayerHealth = 100;
    [SyncVar(hook = nameof(SetPuntos))]
    public int PlayerPuntos = 0;
    [SyncVar(hook = nameof(SetMatados))]
    public int EnemigosMatados = 0;
    [SyncVar]
    public bool estoyMuerto = false;
    [SyncVar(hook = nameof(SetMensaje))]
    public string Mensaje = ".";
    //[SyncVar]
    //public uint MisMuertes = 0;
    [Header("AVATAR")]
    public bool Zurdo = false; //SINCRONIZADOS???
    [SyncVar(hook = nameof(SetMiAltura))]
    public float MiAltura = 1.8f; //SINCRONIZADOS???
    [SerializeField]
    private Transform avatarForScale, avatarForScale2, limiteForUnescale;
    [SerializeField]
    private float alturaAvatar = 1.75f;  //LA ESCALA DEL AVATAR ESTANDAR, ES FIJA PARA EL AVATAR CONCRETO
    [SyncVar(hook = nameof(SetAvatar))]
    public bool Avatar;

    [Header("MUERTE")]
    [SyncVar]
    public bool isDead;
    [SerializeField]
    private GameObject AvatarChico, AvatarChica, deadAvatarChico, deadAvatarChica, MsjDeadAvatar;

    [Header("INMORTAL")] //EN SERVIDOR
    [SyncVar]
    public bool Inmortal;
    [SerializeField]
    private BoxCollider Piernas, Cuerpo, Cabeza;
    //[SerializeField]
    //private GameObject Caerse;

    [Header("INDICADORES")]
    [SerializeField]
    private TextMeshProUGUI textVida;
    [SerializeField]
    private TextMeshProUGUI textPuntos, textMatados, textNombre, textMensaje;
    [SerializeField]
    private TextMeshPro textEquipoTOP, textNombreTOP;
    [Header("OTROS")]
    [SerializeField]
    private int restaPuntosMorir = 50;
    [SerializeField] GameObject CabezaFem, CabezaFemDead, CabezaMasc, CabezaMascDead;
    [Header("A NEGRO")]
    [SerializeField]
    private Camera camara;
    [SerializeField]
    private LayerMask mascaraMINIMO, mascaraTODO;
    [SyncVar(hook = nameof(SetAnegro))]
    public bool aNegro;
    [SyncVar(hook = nameof(SetFadeInFadeOut))]
    public bool FadeInFadeOut;
    [SerializeField]
    private GameObject cuboNegro;
    [SerializeField]
    private BehaviourTreeOwner TapaFade;
    [SyncVar(hook = nameof(SetAlarma))]
    public bool Alarma;
    [SerializeField]
    private GameObject ObjetoAlarma;
    [SerializeField]
    private GameObject Target;

    [Header("MATERIALES JUGADORES SERVER")]
    private GameObject GameManager;
    [SerializeField]
    private Material MatChicoServer;
    //[SerializeField]
    //private Material MatChicaServer;

    [Header("ANTORCHA")]
    [SyncVar(hook = nameof(SetLinterna))]
    public bool OnLinterna;
    [SerializeField]
    private GameObject Linterna;

    /*[Header("PISTOLA")]
    [SyncVar(hook = nameof(SetPistola))]
    public bool OnPistola;
    [SerializeField]
    private GameObject Pistola;*/

    [SerializeField]
    private TextMeshPro texDEBUG;
    private GameObject NivelActual, ZonaRevivirA, ZonaRevivirB, ZonaRevivirC;
    //[SerializeField]
    //private GameObject UbicacionCOL;

    [SerializeField]
    private float TiempoOcultarLevel = 2f;

    // Start is called before the first frame update
    void Start()
    {
        ControlCamaras manager = FindObjectOfType<ControlCamaras>();

        if (manager != null)
        {
            // Pasa el GameObject que tiene el componente Camera y el tag "CamaraJugador"
            manager.RegistrarCamara(camaraServer);
        }

        if (!GameManager)
            GameManager = GameObject.Find("GameManager");

        if (isLocalPlayer)
        {
            ZonaRevivirA = GameObject.Find("REVIVIRZonaUbicacionA");
            ZonaRevivirB = GameObject.Find("REVIVIRZonaUbicacionB");
            ZonaRevivirC = GameObject.Find("REVIVIRZonaUbicacionC");
            ZonaRevivirA.SetActive(false);
            ZonaRevivirB.SetActive(false);
            ZonaRevivirC.SetActive(false);
            //texDEBUG.text += " ZONAS REVIVIR " + ZonaRevivirA + " " + ZonaRevivirB;
            CabezaFem.SetActive(false);
            CabezaFemDead.SetActive(false);
            CabezaMasc.SetActive(false);
            CabezaMascDead.SetActive(false);
            //camara.gameObject.SetActive(true);
        }


        if (isServer) // CAMBIA MATERIALES A LOS JUGADORES SI NO ES PARA MARKETING
        {
            //if (!GameManager)
                //GameManager = GameObject.Find("GameManager");
            if (!GameManager.GetComponent<JuegoManager>().isMARKETING)
            { 
                //AvatarChico.GetComponent<Renderer>().material = MatChicoServer;
                //AvatarChica.GetComponent<Renderer>().material = MatChicaServer;
                //limiteForUnescale.GetComponent<Renderer>().material = MatChicoServer;
            }
        }
        ResetScala();
    }

    [Server]
    public void Inmortalidad_SI()
    {
        Inmortal = true;
        Piernas.enabled = false;
        Cuerpo.enabled = false;
        Cabeza.enabled = false;
        //Caerse.GetComponent<Caida>().enabled = false;
    }
    [Server]
    public void Inmortalidad_NO()
    {
        Inmortal = false;
        Piernas.enabled = true;
        Cuerpo.enabled = true;
        Cabeza.enabled = true;
        //Caerse.GetComponent<Caida>().enabled = true;
    }

    //MUERE LOCAL
    [Client]
    public void ReviveLocal()
    {
        if (!isLocalPlayer)
            return;

        //camara.GetComponent<Skybox>().enabled = true;
        camara.clearFlags = CameraClearFlags.Skybox;

        NivelActual.SetActive(true);
        ZonaRevivirA.SetActive(false);
        ZonaRevivirB.SetActive(false);
        ZonaRevivirC.SetActive(false);
        //texDEBUG.text += " REVIVE LOCAL";

        CmdRevive();
    }
    [Command]
    private void CmdRevive()
    {
        StartCoroutine(WaitForRevivir());
    }
    [TargetRpc]
    void TargetMuereLocal(NetworkConnection target)
    {
        if (!isLocalPlayer)
            return;
        NivelActual = GameObject.FindGameObjectWithTag("Nivel");
        //texDEBUG.text = " MUERTE LOCAL TARGETRPC ";

        //camara.GetComponent<Skybox>().enabled = false;
        camara.clearFlags = CameraClearFlags.SolidColor;

        NivelActual.SetActive(false);

        if (NivelActual.name == "LEVEL 0")
            ZonaRevivirB.SetActive(true);
        if (NivelActual.name == "LEVEL 1")
            ZonaRevivirA.SetActive(true);
        if (NivelActual.name == "LEVEL 2")
            ZonaRevivirA.SetActive(true);
        if (NivelActual.name == "LEVEL 3")
            ZonaRevivirB.SetActive(true);
        if (NivelActual.name == "LEVEL 4 ida y vuelta")
            ZonaRevivirA.SetActive(true);
        if (NivelActual.name == "LEVEL 5 ida y vuelta")
            ZonaRevivirA.SetActive(true);
        if (NivelActual.name == "LEVEL 6 ida y vuelta")
        {
            if (GameManager.GetComponent<JuegoManager>().ActUbicacionL6_1)
                ZonaRevivirA.SetActive(true);
            if (GameManager.GetComponent<JuegoManager>().ActUbicacionL6_2)
                ZonaRevivirB.SetActive(true);
            //texDEBUG.text += GameManager.GetComponent<JuegoManager>().ActUbicacionL6_1 + "  " + GameManager.GetComponent<JuegoManager>().ActUbicacionL6_2;
        }
        if (NivelActual.name == "LEVEL 7 ida")
        {
            if (GameManager.GetComponent<JuegoManager>().ActUbicacionL7_1)
                ZonaRevivirA.SetActive(true);
            //if (GameManager.GetComponent<JuegoManager>().ActUbicacionL7_2)
                //ZonaRevivirB.SetActive(true);
        }
        if (NivelActual.name == "LEVEL 8")
            ZonaRevivirA.SetActive(true);
        if (NivelActual.name == "LEVEL 9 ida")
        {
            if (GameManager.GetComponent<JuegoManager>().ActUbicacionL9_1)
                ZonaRevivirA.SetActive(true);
            //if (GameManager.GetComponent<JuegoManager>().ActUbicacionL9_2)
                //ZonaRevivirA.SetActive(true);
        }
        if (NivelActual.name == "LEVEL 10")
            ZonaRevivirC.SetActive(true);
        if (NivelActual.name == "LEVEL 11")
            ZonaRevivirC.SetActive(true);
        if (NivelActual.name == "LEVEL 12")
            ZonaRevivirA.SetActive(true);
        if (NivelActual.name == "LEVEL 13")
            ZonaRevivirB.SetActive(true);
    }
    //MUERE SERVER
    [Server]
    void muerte()
    {
        if (!isDead)
        {
            isDead = true;
            Target.SetActive(false); // PARA QUE LOS ENEMIGOS NO TE ATAQUEN

            if (Avatar)
            {
                deadAvatarChico.SetActive(true);
                AvatarChico.SetActive(false);
            }
            else
            {
                deadAvatarChica.SetActive(true);
                AvatarChica.SetActive(false);
            } 


            //TIEMPO DESDE CAIDA HASTA PONERSE A NEGRO
            NivelActual = GameObject.FindGameObjectWithTag("Nivel");
            if (NivelActual.name == "LEVEL 11")
                TiempoOcultarLevel = 0.5f;
            if (NivelActual.name == "LEVEL 12")
                TiempoOcultarLevel = 2f;



            StartCoroutine(WaitForTargetMuereLocal());
            //TargetMuereLocal(connectionToClient);
            RpcMuerte();

            //llama a destruye o desactiva
            //StartCoroutine(WaitForRevivir());
        }
    }
    [Server]
    public IEnumerator WaitForTargetMuereLocal()
    {
        yield return new WaitForSeconds(TiempoOcultarLevel);
        TargetMuereLocal(connectionToClient);
    }
    [ClientRpc]
    void RpcMuerte()
    {
        //muerte clientes
        if (Avatar)
        {
            deadAvatarChico.SetActive(true);
            AvatarChico.SetActive(false);
        }
        else
        {
            deadAvatarChica.SetActive(true);
            AvatarChica.SetActive(false);
        }
        MsjDeadAvatar.SetActive(true);
    }
    //REVIVE
    [Server]
    public IEnumerator WaitForRevivir()
    {
        yield return new WaitForSeconds(0.1f);
        isDead = false;

        Target.SetActive(true);

        if (Avatar)
        {
            deadAvatarChico.SetActive(false);
            AvatarChico.SetActive(true);
        }
        else
        {
            deadAvatarChica.SetActive(false);
            AvatarChica.SetActive(true);
        }
        PlayerHealth = 100;
        PlayerPuntos -= restaPuntosMorir;  //RESTA PUNTOS AL MORIR
        if (PlayerPuntos < 0)
            PlayerPuntos = 0;
        RpcRevive();
    }
    [ClientRpc]
    void RpcRevive()
    {
        //muerte clientes
        if (Avatar)
        {
            deadAvatarChico.SetActive(false);
            AvatarChico.SetActive(true);
        }
        else
        {
            deadAvatarChica.SetActive(false);
            AvatarChica.SetActive(true);
        }
        MsjDeadAvatar.SetActive(false);
    }

    //RESETEA LA ESCALA
    [Server]
    public void ResetScala()
    {
        avatarForScale.localScale = new Vector3(MiAltura / alturaAvatar, MiAltura / alturaAvatar,
          MiAltura / alturaAvatar);

        avatarForScale2.localScale = new Vector3(MiAltura / alturaAvatar, MiAltura / alturaAvatar,
          MiAltura / alturaAvatar);

        limiteForUnescale.localScale = new Vector3(1.3f / (MiAltura / alturaAvatar), 
            1.3f / (MiAltura / alturaAvatar),
            1f / (MiAltura / alturaAvatar)); //MANTIENE ESCALA Y TAMAÑO DEL CIRCULO INDEPENDIENTEMENTE DE LA ESCALA DEL PLAYER

        RpcResetScala(MiAltura);
    }
    [ClientRpc]
    void RpcResetScala(float alt)
    {
        avatarForScale.localScale = new Vector3(alt / alturaAvatar, alt / alturaAvatar,
            alt / alturaAvatar);

        avatarForScale2.localScale = new Vector3(alt / alturaAvatar, alt / alturaAvatar,
    alt / alturaAvatar);

        limiteForUnescale.localScale = new Vector3(1.3f / (alt / alturaAvatar),
            1.3f / (alt / alturaAvatar),
            1f / (alt / alturaAvatar)); //MANTIENE ESCALA Y TAMAÑO DEL CIRCULO
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {
        if (PlayerHealth <= 0)
        {
            PlayerHealth = 0;
            muerte();
        }
        //ACTUALIZACIONES EN SERVIDOR
        textNombreTOP.text = Name;
        textNombre.text = Name;
        textEquipoTOP.text = Team;
    }

    //FUNCIONES HOOK ACTUALIZACIONES EN CLIENTE
    void SetMiAltura(float oldMiAltura, float newMiAltura) 
    {
        CmdMiAltura();
    }
    [Command] // LLAMA A RESETSCALA EN SERVIDOR Y LUEGO ESTE LLAMA AL CLIENTE
    void CmdMiAltura() //PRUEBA
    {
        ResetScala();
    }
    void SetVida(int oldVida, int newVida)
    {
        // SONIDO IMPACTO EN PLAYER
        if (newVida < 0) newVida = 0;
        textVida.text = newVida.ToString();
    }
    void SetMatados(int oldMatados, int newMatados)
    {
        textMatados.text = newMatados.ToString();
    }
    void SetPuntos(int oldPuntos, int newPuntos)
    {
        if (newPuntos < 0) newPuntos = 0;
        textPuntos.text = newPuntos.ToString();
    }
    void SetName(string oldName, string newName)
    {
        textNombreTOP.text = newName;
        textNombre.text = newName;
    }
    void SetTeam(string oldTeam, string newTeam)
    {
        textEquipoTOP.text = newTeam;
    }
    void SetMensaje(string oldMensaje, string newMensaje)
    {
        textMensaje.text = newMensaje;
    }
    // A NEGRO
    void SetAnegro(bool oldAnegro, bool newAnegro)
    {
        if (isLocalPlayer)
        {
            if (aNegro)
            {
                camara.cullingMask = mascaraMINIMO;
                cuboNegro.SetActive(true);
            }
            else
            {
                camara.cullingMask = mascaraTODO;
                cuboNegro.SetActive(false);
            }
        }
    }
    void SetFadeInFadeOut(bool oldFadeInFadeOut, bool newFadeInFadeOut)
    {
        if (isLocalPlayer)
        {
            if (FadeInFadeOut)
            {
                TapaFade.RestartBehaviour();
            }
        }
    }
    // ALARMA
    void SetAlarma(bool oldAlarma, bool newAlarma)
    {
        if (isLocalPlayer)
        {
            if (Alarma)
                ObjetoAlarma.SetActive(true);
            else
                ObjetoAlarma.SetActive(false);
        }
    }
    // LINTERNA
    void SetLinterna(bool oldOnLinterna, bool newOnLinterna)
    {
        if (isLocalPlayer)
        {
            if (OnLinterna)
            {
                Linterna.SetActive(true);
                CmdLinterna(true);
            }
            else
            {
                Linterna.SetActive(false);
                CmdLinterna(false);
            }
        }
    }
    [Command]
    void CmdLinterna(bool _newOnLinterna)
    {
        if (_newOnLinterna)
        {
            Linterna.SetActive(true);
            RpcLinterna(true);
        }
        else
        {
            Linterna.SetActive(false);
            RpcLinterna(false);
        }
    }
    [ClientRpc]
    void RpcLinterna(bool _newOnLinterna)
    {
        if (_newOnLinterna)
        {
            Linterna.SetActive(true);
        }
        else
        {
            Linterna.SetActive(false);
        }
    }


    // AVATAR - HOOKS
    void CambiarModelos(bool isChico)
    {
        if (isChico)
        {
            AvatarChico.SetActive(true);
            AvatarChica.SetActive(false);
        }
        else
        {
            AvatarChico.SetActive(false);
            AvatarChica.SetActive(true);
        }
    }

    // El botón / evento llama a esta función en el servidor
    [Server]
    public void SetAvatar(bool oldAvatar, bool newAvatar)
    {
        // 1. Cambiamos los GameObjects visualmente en el Servidor
        CambiarModelos(newAvatar);

        // 2. Disparamos el aviso por la red a todos los Clientes
        RpcAvatar(newAvatar);
    }

    // Esta función la reciben y ejecutan todos los clientes
    [ClientRpc]
    void RpcAvatar(bool newAvatar)
    {
        // 3. Los clientes cambian sus GameObjects visualmente
        CambiarModelos(newAvatar);
    }

    // AVATAR
    public void StartAvatar(bool avat) // LLAMADO DESDE DATOSJUGADORES
    {
        if (avat)
        {
            AvatarChico.SetActive(true);
            AvatarChica.SetActive(false);
        }
        else
        {
            AvatarChico.SetActive(false);
            AvatarChica.SetActive(true);
        }
        RpcAvatar(avat);
    }

    // PISTOLA
    /*void SetPistola(bool oldOnPistola, bool newOnPistola)
    {
        if (isLocalPlayer)
        {
            if (OnPistola)
            {
                Pistola.SetActive(true);
                CmdPistola(true);
            }
            else
            {
                Pistola.SetActive(false);
                CmdPistola(false);
            }
        }
    }
    [Command]
    void CmdPistola(bool _newOnPistola)
    {
        if (_newOnPistola)
            Pistola.SetActive(true);
        else
            Pistola.SetActive(false);
    }*/

    /*
    //IMPACTO SANGRE FADE
    public void FadeIn()
    {
        //imgScreen.CrossFadeAlpha(1.0f, speed, false);
        imgScreen.SetActive(true);
        FadeOut();
    }
    void FadeOut()
    {
        imgScreen.GetComponent<Image>().CrossFadeAlpha(1.0f, speed, true);
    }
    */
}
