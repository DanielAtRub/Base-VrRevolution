using System.Collections;
using UnityEngine;
using Mirror;

public class ProximityAnimationTrigger : NetworkBehaviour
{
    [Header("Configuración de Referencias")]
    [Tooltip("El objeto que debe acercarse para activar la animación.")]
    public Transform objetoObjetivo;

    [Tooltip("Opcional: El punto central de la detección.")]
    public Transform centroDeDeteccion;

    [Header("Referencias del Cofre")]
    public Transform tapaCofre;
    public Transform candado;

    public Vector3 anguloCerrado = Vector3.zero;
    public Vector3 anguloAbierto = new Vector3(-90f, 0f, 0f);
    public float tiempoApertura = 2.0f;

    [Header("Configuración de Distancia")]
    public float distanciaParaActivar = 3.0f;

    [Header("Estado (Sincronizado en Red)")]
    // Ya no necesitamos el hook, Mirror solo sincronizará el valor de forma pasiva.
    [SyncVar]
    public bool estaCerca = false;

    // --- NUEVO: Variable local para asegurarnos de que la corrutina se hace 1 sola vez ---
    private bool animacionReproducida = false;

    void Update()
    {
        // 1. REPRODUCCIÓN LOCAL DE LA ANIMACIÓN
        // Si el cofre está abierto (estaCerca == true) pero aún no hemos hecho la animación localmente...
        if (estaCerca && !animacionReproducida)
        {
            animacionReproducida = true; // Activamos el seguro para que no entre aquí nunca más
            StartCoroutine(AnimarAperturaYCandado());
        }

        // 2. DETECCIÓN DE CERCANÍA (Solo si no se ha abierto, si hay objetivo y SI SOMOS CLIENTE)
        if (!estaCerca && isClient && objetoObjetivo != null)
        {
            Vector3 origen = centroDeDeteccion != null ? centroDeDeteccion.position : transform.position;
            float distanciaActual = Vector3.Distance(origen, objetoObjetivo.position);

            if (distanciaActual <= distanciaParaActivar)
            {
                // Un cliente ha entrado en el área. Llama al servidor.
                CmdAbrirCofre();
            }
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdAbrirCofre()
    {
        if (!estaCerca)
        {
            estaCerca = true;
        }
    }

    private IEnumerator AnimarAperturaYCandado()
    {
        float tiempoPasado = 0f;
        Quaternion rotacionInicial = Quaternion.Euler(anguloCerrado);
        Quaternion rotacionFinal = Quaternion.Euler(anguloAbierto);

        Vector3 posicionInicialCandado = Vector3.zero;
        if (candado != null) posicionInicialCandado = candado.position;

        while (tiempoPasado < tiempoApertura)
        {
            tiempoPasado += Time.deltaTime;
            float porcentaje = tiempoPasado / tiempoApertura;

            if (tapaCofre != null)
            {
                tapaCofre.localRotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, porcentaje);
            }

            if (candado != null)
            {
                float caidaY = porcentaje * -2.5f;
                float impulsoAdelanteZ = porcentaje * 1.5f;

                candado.position = posicionInicialCandado + (candado.forward * impulsoAdelanteZ) + (Vector3.up * caidaY);
                candado.Rotate(Vector3.right * (Time.deltaTime * 400f));
            }

            yield return null;
        }

        if (tapaCofre != null) tapaCofre.localRotation = rotacionFinal;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origen = centroDeDeteccion != null ? centroDeDeteccion.position : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origen, distanciaParaActivar);
    }
}