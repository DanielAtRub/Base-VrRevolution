using System.Collections;
using UnityEngine;

public class MsjAutoDes : MonoBehaviour
{
    public float desactiveAfter = 5;

    [Header("Secuencia Opcional")]
    [SerializeField]
    private bool esperar = false;
    [SerializeField]
    private AudioSource audioSecuencia; // Arrastra aquí el AudioSource desde el Inspector

    [Header("A DEFAULT - ENTRE LEVELS")]
    [SerializeField]
    private Camera camara;
    [SerializeField]
    private LayerMask mascaraTODO;

    void Start()
    {
    }

    void OnEnable()
    {
        // Limpiamos ejecuciones anteriores por si el objeto se apaga y enciende rápido
        CancelInvoke(nameof(DesactiveSelf));
        StopAllCoroutines();

        if (esperar)
        {
            // Si esperar es true, lanzamos la secuencia de los 52 segundos
            StartCoroutine(SecuenciaLarga());
        }
        else
        {
            // Si es false, hace exactamente lo que hacía originalmente
            Invoke(nameof(DesactiveSelf), desactiveAfter);
        }
    }

    private IEnumerator SecuenciaLarga()
    {
        // 1. Espera 52 segundos
        yield return new WaitForSeconds(52f);

        // 2. Activa el audio (comprobando que no esté vacío para evitar errores)
        if (audioSecuencia != null)
        {
            audioSecuencia.Play();
        }

        // 3. Activa todos los hijos directos de este objeto
        foreach (Transform hijo in transform)
        {
            hijo.gameObject.SetActive(true);
        }

        // 4. Espera 5 segundos más
        yield return new WaitForSeconds(5f);

        // 5. Ejecuta tu método final de desactivación
        DesactiveSelf();
    }

    void DesactiveSelf()
    {
        gameObject.SetActive(false);
        // Only render objects in the layer
        if (camara != null)
        {
            camara.cullingMask = mascaraTODO;
        }
    }
}