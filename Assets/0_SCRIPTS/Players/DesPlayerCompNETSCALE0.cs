using UnityEngine;
using Mirror;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

public class DesPlayerCompNETSCALE0 : NetworkBehaviour
{
    //[Header("Desactivación VRIK")]
    //[SerializeField]
    //private GameObject VRIK;

    //NOTA: PONER CONTROL public override void OnStartServer() {}
    [Header("Captura posición y rotación de Gafas y Mandos")]
    [SerializeField]
    private GameObject OVR;
    [SerializeField]
    private Camera CamaraL, CamaraR;
    [SerializeField]
    private Transform LHand, RHand, CamaraC, CamaraCe;

    //Escala el movimiento lineal
    [Header("Escala de Movimiento")]

    //public TextMeshPro debugTextScala, debugTextNOScala; //PARA DEBUG
    [SerializeField]
    private bool escalarMov = false;
    [SerializeField]
    private float EscalaMov = 1.0f;
    Vector3 lastPos = Vector3.zero;

    //Establece altura/escala del Jugador o Avatar
    //[Header("Establece altura/escala del Jugador o Avatar")]
    /*
    [Header("        El Avatar se escala a la Altura predefinida en PLAYER")]
    [SerializeField]
    private bool scalaAvatarPlayer;
    [SerializeField]
    private float AlturaPlayer;
    public bool SetScala;
    */
    //[Header("        El Avatar se escala al Jugador")]
    //[SerializeField]
    //private bool scalaAvatar;
    //[SerializeField]
    //private Transform avatar;
    //[Header("        El Jugador se escala al Avatar")]
    //[SerializeField]
    //private bool scalaJugador;

    private float alturaAvatar = 1.75f;
    private bool puls;

    [Header("Desactivar en NO LocalPlayer")]
    [SerializeField]
    private GameObject Cabeza;
    [SerializeField]
    private GameObject Cuerpo;

    void Start()
    {
        if (!isLocalPlayer)
        {
            if (OVR != null)
                Destroy(OVR);
        }
        else
        {
            //DESACTIVAR EN LOCAL PLAYER PARA QUE SEA EL SERVIDOR EL QUE ACTUE
            Cabeza.GetComponent<BoxCollider>().enabled = false;
            Cabeza.GetComponent<NoTraspasing>().enabled = false;
            Cuerpo.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            if (OVR != null)
                Destroy(OVR);
        }
        else
        {
            //VERIFICAR FUNCIONALIDAD
            if (CamaraL.tag != "MainCamera")
            {
                CamaraL.tag = "MainCamera";
                CamaraL.enabled = true;
            }
            if (CamaraR.tag != "MainCamera")
            {
                CamaraR.tag = "MainCamera";
                CamaraR.enabled = true;
            }
            
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
        }

    }
    
}
