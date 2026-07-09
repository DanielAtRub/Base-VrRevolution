using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapConector : NetworkBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public bool estaAgarrado = false;

    // Ya no necesitamos el "hook" porque las f�sicas nunca cambian.
    [SyncVar]
    public bool InSnap = false;

    // --- VARIABLES PARA EL EFECTO CABLE ---
    private Vector3 posInicial;
    private Quaternion rotInicial;
    private bool regresando = false;

    [Header("Configuraci�n del Cable")]
    public float velocidadRegreso = 8f; // Ajusta esto para que vuelva m�s r�pido o m�s lento

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // 1. Forzamos a que SIEMPRE sea kinematic y SIN gravedad
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // 2. Guardamos de d�nde sale el cable al empezar el juego
        posInicial = transform.position;
        rotInicial = transform.rotation;
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
        regresando = false; // Si lo vuelves a coger en el aire mientras regresaba, cancelamos el regreso

        if (isOwned)
        {
            CmdSetSnap(false);
        }
    }

    private void AlSoltar(SelectExitEventArgs args)
    {
        estaAgarrado = false;

        // Si somos los due�os, lo soltamos y NO est� pegado en la zona...
        if (isOwned && !InSnap)
        {
            // �Activamos la orden de regresar a la base!
            regresando = true;
        }
    }

    // --- 2. EL EFECTO DE REGRESO FLUIDO ---

    void Update()
    {
        // Solo el cliente que tiene la autoridad calcula el movimiento. El NetworkTransform se lo ense�ar� al resto.
        if (!isOwned) return;

        if (regresando && !estaAgarrado && !InSnap)
        {
            // Movemos el objeto fluidamente hacia su posici�n inicial
            transform.position = Vector3.Lerp(transform.position, posInicial, Time.deltaTime * velocidadRegreso);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotInicial, Time.deltaTime * velocidadRegreso);

            // Si ya est� muy cerca del origen, lo encajamos exactamente y apagamos el regreso
            if (Vector3.Distance(transform.position, posInicial) < 0.01f)
            {
                transform.position = posInicial;
                transform.rotation = rotInicial;
                regresando = false;
            }
        }
    }

    // --- 3. DETECCI�N DE LA ZONA ---

    void OnTriggerStay(Collider other)
    {
        if (!isOwned) return;

        if (other.CompareTag("SnapConector") && !InSnap && !estaAgarrado)
        {
            regresando = false; // Paramos el regreso por si acaso

            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (TryGetComponent<AudioSource>(out AudioSource audio)) audio.Play();

            CmdSetSnap(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isOwned) return;

        if (other.CompareTag("SnapConector") && InSnap)
        {
            CmdSetSnap(false);
        }
    }

    // --- 4. COMUNICACI�N CON EL SERVIDOR ---

    [Command]
    void CmdSetSnap(bool estado)
    {
        InSnap = estado;
    }
}