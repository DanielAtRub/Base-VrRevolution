using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit;

public class CambiarColorGema : NetworkBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public Renderer objetoRenderer;
    public Material verde;

    public bool estaAgarrado = false;

    // 1. Convertimos la variable en SyncVar con un Hook
    [SyncVar(hook = nameof(OnColorChanged))]
    public bool colorCambiado = false;

    [SyncVar(hook = nameof(OnSnapChanged))]
    public bool InSnap = false;

    // NUEVO: Guardamos la referencia de la zona que estamos ocupando
    private SnapColor snapColor;
    private SnapZone zonaActual;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Buscamos el renderer en los hijos si no est� asignado
        if (objetoRenderer == null)
        {
            objetoRenderer = GetComponentInChildren<Renderer>();
        }
    }

    private void LateUpdate()
    {
        if (colorCambiado)
        {
            objetoRenderer.material = verde;
        }
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
            // Cogemos el GameObject de la zona en la que estemos (priorizando color, si no, normal, si no, null)
            GameObject zonaObj = snapColor != null ? snapColor.gameObject : (zonaActual != null ? zonaActual.gameObject : null);

            // Se lo mandamos al servidor. El Command ya se encarga de ver qu� componente tiene
            CmdSetSnapState(false, zonaObj);

            // Limpiamos las dos referencias locales para curarnos en salud
            snapColor = null;
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

        Collider[] cosasCercanas = Physics.OverlapSphere(transform.position, 0.2f);
        foreach (Collider col in cosasCercanas)
        {
            CambiarColorGema otraGema = col.GetComponentInParent<CambiarColorGema>();
            if (otraGema != null && otraGema != this)
            {
                return;
            }
        }

        // Importante: Al estar InSnap o estaAgarrado, no intentar� pegarse de nuevo
        if (other.CompareTag("CambioColor") && !InSnap && !estaAgarrado)
        {
            SnapColor zona = other.GetComponent<SnapColor>();

            if (zona != null && !zona.isOccupied)
            {
                snapColor = zona;
                transform.SetParent(other.transform, true);

                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;

                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                if (TryGetComponent<AudioSource>(out AudioSource audio)) audio.Play();

                CmdSetSnapState(true, zona.gameObject);
                CmdChangeColor(true);
                objetoRenderer.material = verde;
            }
        }

        if (other.CompareTag("SnapZone") && !InSnap && !estaAgarrado)
        {
            SnapZone zona = other.GetComponent<SnapZone>();

            if (zona != null && !zona.isOccupied)
            {
                zonaActual = zona;
                transform.SetParent(other.transform, true);

                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;

                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                if (TryGetComponent<AudioSource>(out AudioSource audio)) audio.Play();

                CmdSetSnapState(true, zona.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOwned) return;

        if (other.CompareTag("CambioColor") && InSnap)
        {
            SnapColor zona = other.GetComponent<SnapColor>();

            // NUEVO: Solo liberamos la zona si es la misma que ocup�bamos
            if (zona != null && zona == snapColor)
            {
                CmdSetSnapState(false, zona.gameObject);
                snapColor = null;
            }
        }

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

    // NUEVO: Modificamos el Command para que gestione tambi�n el estado de la zona
    [Command]
    void CmdSetSnapState(bool estado, GameObject zonaObj)
    {
        InSnap = estado;

        if (zonaObj != null)
        {
            SnapColor zonaColor = zonaObj.GetComponent<SnapColor>();
            if (zonaColor != null)
            {
                zonaColor.isOccupied = estado;
            }

            SnapZone zona = zonaObj.GetComponent<SnapZone>();
            if (zona != null)
            {
                zona.isOccupied = estado;
            }
        }

    }

    // Renombramos con "Cmd" por convenci�n de Mirror
    [Command]
    void CmdChangeColor(bool estado)
    {
        colorCambiado = estado;
    }

    // 4. El Hook que cambia el color en las pantallas de TODOS
    void OnColorChanged(bool valorAnterior, bool valorNuevo)
    {
        if (objetoRenderer == null) return;

        if (valorNuevo == true)
        {
            objetoRenderer.material = verde;
        }
    }

    // El servidor cambia el SyncVar y esto se ejecuta en las pantallas de TODOS
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