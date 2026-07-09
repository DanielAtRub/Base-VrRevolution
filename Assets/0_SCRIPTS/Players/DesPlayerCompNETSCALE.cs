/*using UnityEngine;
using Mirror;
using TMPro;

public class DesPlayerCompNETSCALE : NetworkBehaviour
{
    [Header("Captura posición y rotación de Gafas y Mandos")]
    [SerializeField]
    private GameObject OVR; // SE MANTIENE PARA NO ROMPER PREFABS
    [SerializeField]
    private Transform MainCamera, CamaraC;

    [Header("Escala de Movimiento")]
    [SyncVar]
    public float EscalaMov = 1.1f;

    Vector3 lastPos = Vector3.zero;

    [Header("Desactivar en NO LocalPlayer")]
    [SerializeField]
    private GameObject Cabeza;
    [SerializeField]
    private GameObject Cuerpo;

    private Transform Plataforma;

    [SerializeField]
    private GameObject GameManager;

    void Start()
    {
        if (!isLocalPlayer)
        {
            if (OVR != null)
                Destroy(OVR);
        }
        else
        {
            Cabeza.GetComponent<BoxCollider>().enabled = false;
            Cabeza.GetComponent<NoTraspasing>().enabled = false;
            Cuerpo.GetComponent<BoxCollider>().enabled = false;

            if (!Plataforma && GameObject.Find("Plataforma"))
                Plataforma = GameObject.Find("Plataforma").transform;

            if (!GameManager)
                GameManager = GameObject.Find("GameManager");

            EscalaMov = GameManager.GetComponent<JuegoManager>().ScalaMovGlobal;
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        // === SUSTITUCIÓN DIRECTA DE InputTracking ===

        // CABEZA
        CamaraC.localRotation = MainCamera.localRotation;
        CamaraC.localPosition = MainCamera.localPosition;

        // === ESCALADO DE MOVIMIENTO (MISMA LÓGICA ORIGINAL) ===
        var offset = CamaraC.localPosition * (EscalaMov - 1);
        offset.y = transform.position.y;

        var a = Plataforma.position;
        a.y = 0;

        transform.position = offset + a;
    }
}*/

using UnityEngine;
using Mirror;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class DesPlayerCompNETSCALE : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [Header("Captura posición y rotación de Gafas y Mandos")]
    [SerializeField]
    private GameObject OVR;
    //[SerializeField]
    //private Camera CamaraL, CamaraR;
    [SerializeField]
    private Transform LHand, RHand, CamaraC;

    [SerializeField]
    private BNG.HandController manoIzquierda, manoDerecha;

    //Escala el movimiento lineal
    [Header("Escala de Movimiento")]
    //public TextMeshPro debugTextScala, debugTextNOScala; //PARA DEBUG
    //[SerializeField]
    //private bool escalarMov = false;
    //[SerializeField]
    //private float EscalaMov = 1.0f;
    [SyncVar]
    public float EscalaMov = 1.1f;

    Vector3 lastPos = Vector3.zero;

    //private float alturaAvatar = 1.75f;
    //private bool puls;

    [Header("Desactivar en NO LocalPlayer")]
    [SerializeField]
    private GameObject Cabeza;
    [SerializeField]
    private GameObject Cuerpo;

    //[Header("Global Settings")]
    //[SerializeField]
    //private GameObject Gsettings;

    //PRUEBA
    /*[SerializeField]
    private Transform Tio;
    private Transform CERO;
    private Vector3 DiferenciaP, DiferenciaPsave;
    private Vector3 DiferenciaR;*/
    private Transform Plataforma;

    //PRUEBAS
    [SerializeField]
    private GameObject GameManager;
    //PRUEBAS

    void Start()
    {
        if (!isLocalPlayer)
        {
            /*if (OVR != null)
                Destroy(OVR);*/

            RHand.GetComponent<XRTransformStabilizer>().enabled = false; //PICO
            LHand.GetComponent<XRTransformStabilizer>().enabled = false; //PICO
            manoIzquierda.enabled = false;
            manoDerecha.enabled = false;
        }
        else
        {
            if (OVR != null)
                OVR.SetActive(true);

            //CAPTURA ESCALA DE MOVIMIENTO DE BLOBAL SETTINGS
            //if (Gsettings.GetComponent<GlobalSettings>().EscalaM != 0f)
            //    EscalaMov = Gsettings.GetComponent<GlobalSettings>().EscalaM;

            //DESACTIVAR EN LOCAL PLAYER PARA QUE SEA EL SERVIDOR EL QUE ACTUE
            Cabeza.GetComponent<BoxCollider>().enabled = false;
            //Cabeza.GetComponent<NoTraspasing>().enabled = false;
            Cuerpo.GetComponent<BoxCollider>().enabled = false;
            /*
            //LEE DIFERENCIA POS Y MUEVE
            if (PlayerPrefs.HasKey("DiferenciaP_X"))
                DiferenciaPsave.x = PlayerPrefs.GetFloat("DiferenciaP_X");
            if (PlayerPrefs.HasKey("DiferenciaP_Y"))
                DiferenciaPsave.y = PlayerPrefs.GetFloat("DiferenciaP_Y");
            if (PlayerPrefs.HasKey("DiferenciaP_Z"))
                DiferenciaPsave.z = PlayerPrefs.GetFloat("DiferenciaP_Z");
            transform.position -= DiferenciaPsave;
            CmdDebug(DiferenciaP, DiferenciaPsave);
            */
            if (!Plataforma)
            {
                if (GameObject.Find("Plataforma"))
                    Plataforma = GameObject.Find("Plataforma").transform;
            }
            //LEE SCALAMOV DE GLOBALSETTINGS
            if (!GameManager)
                GameManager = GameObject.Find("GameManager");
            EscalaMov = GameManager.GetComponent<JuegoManager>().ScalaMovGlobal;
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            /* ORIGINAL - GUARDIAN ESTATICO - TRACKING FLOOR
            //ESCALA EL MOVIMIENTO LINEAL
            if (escalarMov)
            {
                if (lastPos == Vector3.zero) lastPos = CamaraC.localPosition;
                var offset = CamaraC.localPosition - lastPos;
                offset.y = 0;
                transform.position += offset * (EscalaMov - 1);
                lastPos = CamaraC.localPosition;
            }
            //COGE DEL SISTEMA LAS POSICIONES DE LAS MANOS Y GAFAS YA QUE SE HA ELIMINAADO OVRCAMERARIG
            LHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            RHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
            LHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.LeftHand);
            RHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.RightHand);
            CamaraC.localRotation = InputTracking.GetLocalRotation(Node.CenterEye);
            CamaraC.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.CenterEye);
            */


            //PRUEBA 3 - OK - GUARDIAN ESTATICO - TRACKING STAGE
            //ESCALA EL MOVIMIENTO LINEAL
            //****PROTOCOLO
            //SI EL TRACKING ESTA EN MOVIMIENTO (RUSOS) ACTIVAR GUARDIAN (DESARROLLADOR) Y CAMBIAR A
            //ESTATICO EN LA BARRA PRINCIPAL CONLOCANDOTE EN EL CENTRO Y MIRANDO HACIA ADELANTE.
            //PARA CAMBIAR A MOVIMIENTO (RUSOS) DESDE ESTATICO SIMPLEMENTE SELECIONALO DESDE LA BARRA.
            //****PROTOCOLO
            //COGE DEL SISTEMA LAS POSICIONES DE LAS MANOS Y GAFAS YA QUE SE HA ELIMINAADO OVRCAMERARIG
            LHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            RHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
            LHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.LeftHand);
            RHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.RightHand);
            CamaraC.localRotation = InputTracking.GetLocalRotation(Node.CenterEye);
            CamaraC.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.CenterEye);
            //ESCALA
            var offset = InputTracking.GetLocalPosition(Node.CenterEye) * (EscalaMov - 1);
            offset.y = transform.position.y;
            //POSICION
            var a = Plataforma.position;
            a.y = 0;
            transform.position = offset + a;


            /*
            //PRUEBA 4 - OK - GUARDIAN DESPLAZAMIENTO  - TRACKING STAGE
            //COGE DEL SISTEMA LAS POSICIONES DE LAS MANOS Y GAFAS YA QUE SE HA ELIMINAADO OVRCAMERARIG
            LHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            RHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
            LHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.LeftHand);
            RHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.RightHand);
            CamaraC.localRotation = InputTracking.GetLocalRotation(Node.CenterEye);
            CamaraC.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.CenterEye);
            if (lastPos == Vector3.zero) lastPos = CamaraC.localPosition;
            var offset = CamaraC.localPosition - lastPos;
            offset.y = 0;
            transform.position += offset * (EscalaMov - 1);
            lastPos = CamaraC.localPosition;
            if (OVRInput.GetDown(OVRInput.Button.Two)) //REAJUSTE DE POSICION
            {
                CERO = GameObject.Find("CERO").transform;
                //ROTACION
                //transform.rotation = Quaternion.Euler(Vector3.zero);
                //DiferenciaR = CamaraC.rotation.eulerAngles - CERO.rotation.eulerAngles;
                //DiferenciaR.x = 0;
                //DiferenciaR.z = 0;
                //transform.rotation = Quaternion.Euler(0, -DiferenciaR.y, 0);//PRIMERO ROTACION
                //OVR.GetComponent<OVRCameraRig>().trackingSpace.localRotation = Quaternion.Euler(0, -DiferenciaR.y, 0);
                //POSICION
                DiferenciaP = (Tio.position - CERO.position) - Plataforma.position;
                DiferenciaP.y = 0;
                transform.position -= DiferenciaP;

                DiferenciaPsave = DiferenciaP + DiferenciaPsave;
                PlayerPrefs.SetFloat("DiferenciaP_X", DiferenciaPsave.x);
                PlayerPrefs.SetFloat("DiferenciaP_Y", DiferenciaPsave.y);
                PlayerPrefs.SetFloat("DiferenciaP_Z", DiferenciaPsave.z);
                //OVR.GetComponent<OVRCameraRig>().trackingSpace.position -= Diferencia;
                //OVR.GetComponent<OVRCameraRig>().trackingSpace.rotation = Quaternion.Euler(DiferenciaR);
                //OVR.transform.Rotate (0,1,0);
                //transform.Rotate (0, 1, 0);//PRIMERO ROTACION
                CmdDebug(DiferenciaP, DiferenciaPsave);
            }
            */
        }
    }
    /*
    [Command]
    void CmdDebug(Vector3 dif, Vector3 dif2)
    {
        Debug.Log("DiferenciaP" + dif + "   " + "DiferenciaPsave" + dif2); // False
    }
    */
}
