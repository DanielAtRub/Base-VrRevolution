using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapObject : NetworkBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public bool estaAgarrado = false;

    [SyncVar(hook = nameof(OnSnapChanged))]
    public bool InSnap = false;

    // Guardamos la referencia de la zona que estamos ocupando actualmente
    private SnapZone zonaActual;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(AlAgarrar);
            grabInteractable.selectExited.AddListener(AlSoltar);
        }
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(AlAgarrar);
            grabInteractable.selectExited.RemoveListener(AlSoltar);
        }
    }

    // --- 1. L�GICA DE AGARRE LOCAL ---

    private void AlAgarrar(SelectEnterEventArgs args)
    {
        estaAgarrado = true;

        if (isOwned)
        {
            // Al agarrarlo, pasamos la zona actual al Command para liberarla en el servidor
            GameObject zonaObj = zonaActual != null ? zonaActual.gameObject : null;
            CmdSetSnapState(false, zonaObj);

            // Limpiamos la referencia local
            zonaActual = null;
        }
    }

    private void AlSoltar(SelectExitEventArgs args)
    {
        estaAgarrado = false;

        if (!InSnap)
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }

    // --- 2. DETECCI�N DE LA ZONA (Ejecutado por el due�o del objeto) ---

    void OnTriggerStay(Collider other)
    {
        if (!isOwned) return;

        if (other.CompareTag("SnapZone") && !InSnap && !estaAgarrado)
        {
            SnapZone zona = other.GetComponent<SnapZone>();

            // Condici�n clave: Solo nos pegamos si la zona existe y NO est� ocupada
            if (zona != null && !zona.isOccupied)
            {
                zonaActual = zona; // Guardamos la zona en la que nos hemos pegado

                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;

                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                if (TryGetComponent<AudioSource>(out AudioSource audio)) audio.Play();

                // Avisamos al servidor que nos hemos pegado y qu� zona hemos ocupado
                CmdSetSnapState(true, zona.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOwned) return;

        if (other.CompareTag("SnapZone") && InSnap)
        {
            SnapZone zona = other.GetComponent<SnapZone>();

            // Solo liberamos la zona si estamos saliendo f�sicamente de la zona que ocup�bamos
            if (zona != null && zona == zonaActual)
            {
                CmdSetSnapState(false, zona.gameObject);
                zonaActual = null;
            }
        }
    }

    // --- 3. COMUNICACI�N CON EL SERVIDOR Y LOS DEM�S CLIENTES ---

    // Modificamos el Command para recibir la zona y cambiar ambos estados a la vez en el servidor
    [Command]
    void CmdSetSnapState(bool estado, GameObject zonaObj)
    {
        InSnap = estado;

        if (zonaObj != null)
        {
            SnapZone zona = zonaObj.GetComponent<SnapZone>();
            if (zona != null)
            {
                zona.isOccupied = estado;
            }
        }
    }

    void OnSnapChanged(bool valorAnterior, bool valorNuevo)
    {
        if (rb == null) return;

        if (valorNuevo == true)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            if (!estaAgarrado)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }
}