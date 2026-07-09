using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.InputSystem; //PICO

public class AthorPlayer : NetworkBehaviour {
    [SerializeField]
    private Transform manoD, manoI;
    [SerializeField]
    private GameObject HandPrefab;
    [SerializeField]
    private GameObject HandDerecha, HandIzquierda;
    [SerializeField]
    private TextMeshPro textDEBUG;
    [SyncVar]
    public bool AgarrarPulsadoD, AgarrarPulsadoI;
    [SyncVar]
    public NetworkIdentity _grabID;

    [SerializeField]
    private NetworkIdentity grabID_BUFFER;

    [Header("PICO")]
    [SerializeField]
    private InputActionReference rightTriggerAction; //PICO
    [SerializeField]
    private InputActionReference leftTriggerAction; //PICO

    void Start()
    {
        CmdParentHandD();
        CmdParentHandI();
    }

    //MANO DERECHA
    [Server]
    void CmdParentHandD()
    {
        //CREA MANO DERECHA INDEPENDIENTE
        HandDerecha = Instantiate(HandPrefab, manoD.position, manoD.rotation);
        NetworkServer.Spawn(HandDerecha, connectionToClient);
        RpcParentHandD(HandDerecha);
    }
    [ClientRpc]
    void RpcParentHandD(GameObject h)
    {
        //LA HACE PADRE DE LA MANO DEL PLAYER
        h.transform.SetParent(manoD);
        h.transform.position = Vector3.zero;
        h.transform.rotation = Quaternion.identity;
    }
    //MANO IZQUIERDA
    [Server]
    void CmdParentHandI()
    {
        //CREA MANO IZQUIERDA INDEPENDIENTE
        HandIzquierda = Instantiate(HandPrefab, manoI.position, manoI.rotation);
        NetworkServer.Spawn(HandIzquierda, connectionToClient);
        RpcParentHandI(HandIzquierda);
    }
    [ClientRpc]
    void RpcParentHandI(GameObject h)
    {
        //LA HACE PADRE DE LA MANO DEL PLAYER
        h.transform.SetParent(manoI);
        h.transform.position = Vector3.zero;
        h.transform.rotation = Quaternion.identity;
    }

    //////////////////////////////////////////////////////////////

    // SIN GRAVEDAD
    [Client]
    public void PermisoOn(Collider other)
    {
        CmdSetAuthority(other.GetComponent<NetworkIdentity>());
    }
    [Client]
    public void PermisoOff(Collider other)
    {
        CmdRemoveAuthority(other.GetComponent<NetworkIdentity>());
    }
    // CON GRAVEDAD
    /*[Client]
    public void PermisoOnG(Collider other)
    {
        CmdSetAuthorityG(other.GetComponent<NetworkIdentity>());
    }
    [Client]
    public void PermisoOffG(Collider other)
    {
        CmdRemoveAuthorityG(other.GetComponent<NetworkIdentity>());
    }*/

    //////////////////////////////////////////////////////////////

    /// ASSIGN AND REMOVE CLIENT AUTHORITY///
    // SIN GRAVEDAD
    /*[Command(requiresAuthority = false)]
    void CmdSetAuthority(NetworkIdentity grabID)
    {
        _grabID = grabID;

        grabID.RemoveClientAuthority();
        grabID.AssignClientAuthority(connectionToClient);

        grabID.GetComponent<Rigidbody>().useGravity = false;
        grabID.GetComponent<Rigidbody>().isKinematic = true;
    }
    [Command(requiresAuthority = false)]
    void CmdRemoveAuthority(NetworkIdentity grabID)
    {
        _grabID = null;

        grabID.RemoveClientAuthority();

        grabID.GetComponent<Rigidbody>().useGravity = false;
        grabID.GetComponent<Rigidbody>().isKinematic = false;
    }*/
    // CON GRAVEDAD
    [Command(requiresAuthority = false)]
    void CmdSetAuthority(NetworkIdentity grabID)
    {
        //Debug.Log(_grabID);
        if (_grabID)
            _grabID.GetComponent<Cube>().Parent = null;
        _grabID = grabID;
        //grabID.RemoveClientAuthority();
        //grabID.AssignClientAuthority(connectionToClient);
        //grabID.GetComponent<Rigidbody>().useGravity = false;
        //grabID.GetComponent<Rigidbody>().isKinematic = true;
    }
    [Command(requiresAuthority = false)]
    void CmdRemoveAuthority(NetworkIdentity grabID)
    {
        //Debug.Log(_grabID);
        _grabID = null;
        //grabID.RemoveClientAuthority();
        //grabID.GetComponent<Rigidbody>().useGravity = true;
        //grabID.GetComponent<Rigidbody>().isKinematic = false;
    }

    //////////////////////////////////////////////////////////////
    
    //PULSAR AGARRAR
    void Update()
    {
        if (!isLocalPlayer) return;

        //MANDO MANO DERECHA
        //if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        if (rightTriggerAction.action.ReadValue<float>() > 0.8f && !AgarrarPulsadoD) //PICO
        {
            AgarrarPulsadoD = true;
            CmdAgarrarPulsadoD(AgarrarPulsadoD);
        }
        //if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        if (rightTriggerAction.action.ReadValue<float>() < 0.2f && AgarrarPulsadoD) //PICO
        {
            AgarrarPulsadoD = false;
            CmdAgarrarPulsadoD(AgarrarPulsadoD);
        }
        //MANDO MANO IZQUIERDA
        //if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        if (leftTriggerAction.action.ReadValue<float>() > 0.8f && !AgarrarPulsadoI) //PICO
        {
            AgarrarPulsadoI = true;
            CmdAgarrarPulsadoI(AgarrarPulsadoI);
        }
        //if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        if (leftTriggerAction.action.ReadValue<float>() < 0.2f && AgarrarPulsadoI) //PICO
        {
            AgarrarPulsadoI = false;
            CmdAgarrarPulsadoI(AgarrarPulsadoI);
        }
    }
    // MANO DERECHA
    [Command]
    void CmdAgarrarPulsadoD(bool val)
    {
        AgarrarPulsadoD = val;
        if (val)
        {
            grabID_BUFFER = _grabID; //PRUEBA

            if (_grabID)
            {
                _grabID.RemoveClientAuthority();
                _grabID.AssignClientAuthority(connectionToClient);

                _grabID.GetComponent<Cube>().Parent = HandDerecha.gameObject; //LE DA LA REFERENCIA DE LA MANO CREADA Y IGUALA POS Y ROT
                _grabID.GetComponent<Rigidbody>().useGravity = false;
                _grabID.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else
        {
            if (grabID_BUFFER) //PRUEBAS
            {
                grabID_BUFFER.RemoveClientAuthority();

                grabID_BUFFER.GetComponent<Cube>().Parent = null; //ELIMINA LA REFERENCIA DE LA MANO CREADA
                if (grabID_BUFFER.GetComponent<Cube>().TieneGravedad)
                {
                    grabID_BUFFER.GetComponent<Rigidbody>().useGravity = true;
                    grabID_BUFFER.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    grabID_BUFFER.GetComponent<Rigidbody>().useGravity = false;
                    grabID_BUFFER.GetComponent<Rigidbody>().isKinematic = false;
                }
                //SI TIENE ESTE COMPONENTE "GEMA" EJECUTA FUNCION REGRESO A POS ORIGINAL
                if (grabID_BUFFER.TryGetComponent<RegresaPos>(out RegresaPos RegresaP))
                {
                    RegresaP.RegresaPosOriginal();
                }
                //Debug.Log("QUITA PROPIEDAD");
                grabID_BUFFER = null;
            }
            if (_grabID)
            {
                _grabID.RemoveClientAuthority();

                _grabID.GetComponent<Cube>().Parent = null; //ELIMINA LA REFERENCIA DE LA MANO CREADA
                if (_grabID.GetComponent<Cube>().TieneGravedad)
                {
                    _grabID.GetComponent<Rigidbody>().useGravity = true;
                    _grabID.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    _grabID.GetComponent<Rigidbody>().useGravity = false;
                    _grabID.GetComponent<Rigidbody>().isKinematic = false;
                }
                //_grabID = null;
            }
        }
    }
    // MANO IZQUIERDA
    [Command]
    void CmdAgarrarPulsadoI(bool val)
    {
        AgarrarPulsadoI = val;
        if (val)
        {
            grabID_BUFFER = _grabID; //PRUEBA

            if (_grabID)
            {
                _grabID.RemoveClientAuthority();
                _grabID.AssignClientAuthority(connectionToClient);

                _grabID.GetComponent<Cube>().Parent = HandIzquierda.gameObject; //LE DA LA REFERENCIA DE LA MANO CREADA Y IGUALA POS Y ROT
                _grabID.GetComponent<Rigidbody>().useGravity = false;
                _grabID.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else
        {
            if (grabID_BUFFER) //PRUEBAS
            {
                grabID_BUFFER.RemoveClientAuthority();

                grabID_BUFFER.GetComponent<Cube>().Parent = null; //ELIMINA LA REFERENCIA DE LA MANO CREADA
                if (grabID_BUFFER.GetComponent<Cube>().TieneGravedad)
                {
                    grabID_BUFFER.GetComponent<Rigidbody>().useGravity = true;
                    grabID_BUFFER.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    grabID_BUFFER.GetComponent<Rigidbody>().useGravity = false;
                    grabID_BUFFER.GetComponent<Rigidbody>().isKinematic = false;
                }
                //SI TIENE ESTE COMPONENTE "GEMA" EJECUTA FUNCION REGRESO A POS ORIGINAL
                if (grabID_BUFFER.TryGetComponent<RegresaPos>(out RegresaPos RegresaP))
                {
                    RegresaP.RegresaPosOriginal();
                }
                //Debug.Log("QUITA PROPIEDAD");
                grabID_BUFFER = null;
            }
            if (_grabID)
            {
                _grabID.RemoveClientAuthority();

                _grabID.GetComponent<Cube>().Parent = null; //ELIMINA LA REFERENCIA DE LA MANO CREADA
                if (_grabID.GetComponent<Cube>().TieneGravedad)
                {
                    _grabID.GetComponent<Rigidbody>().useGravity = true;
                    _grabID.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    _grabID.GetComponent<Rigidbody>().useGravity = false;
                    _grabID.GetComponent<Rigidbody>().isKinematic = false;
                }
                //_grabID = null;
            }
        }
    }

}
