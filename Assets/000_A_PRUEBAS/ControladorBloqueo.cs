using UnityEngine;
using Mirror;


public class ControladorBloqueo : NetworkBehaviour
{
    [Header("Referencias")]
    [Tooltip("El objeto Laser que está rotando y queremos vigilar.")]
    [SerializeField] private Transform transformLaser;

    [Tooltip("El script GiroLaser que queremos apagar.")]
    [SerializeField] private GiroLaser scriptGiroLaser1;
    [SerializeField] private GiroLaser scriptGiroLaser2;

    [Header("Materiales a Cambiar")]
    [Tooltip("Arrastra los objetos que tienen el material MC_Bright_54")]
    [SerializeField] private Renderer renderer1;
    [SerializeField] private Renderer renderer2;

    // Por defecto lo ponemos en blanco, pero puedes cambiarlo desde el Inspector
    [SerializeField] private Color colorBloqueo = Color.white;

    [Header("Configuración de Bloqueo")]
    [Tooltip("El ángulo X donde quieres que se detenga y se apague el script.")]
    [SerializeField] private float anguloObjetivoX;

    [Tooltip("Margen de error en grados. Como se mueve por frames, rara vez caerá en el decimal exacto.")]
    [SerializeField] private float tolerancia = 0.5f;

    private bool yaBloqueado = false;

    // Solo el servidor comprueba esto, para llevarle la contraria al [ServerCallback] de GiroLaser
    void Update()
    {
        if (yaBloqueado) return;

        // 1. Obtener la rotación actual y normalizarla igual que en tu script original
        float currentX = transformLaser.localEulerAngles.x;
        if (currentX > 180f)
        {
            currentX -= 360f;
        }

        // 2. Comprobar si hemos llegado al ángulo usando la tolerancia
        // Mathf.Abs mide la distancia entre la rotación actual y tu objetivo. 
        // Si esa distancia es menor que la tolerancia, ¡hemos llegado!
        if (Mathf.Abs(currentX - anguloObjetivoX) <= tolerancia)
        {
            EjecutarBloqueo();
            if (renderer1 != null)
            {
                renderer1.material.SetColor("_TintColor", colorBloqueo);
            }

            if (renderer2 != null)
            {
                renderer2.material.SetColor("_TintColor", colorBloqueo);
            }
        }
    }

    private void EjecutarBloqueo()
    {
        yaBloqueado = true;

        // 1. Forzar la rotación al número exacto para que no se quede desviado
        Vector3 rotacionActual = transformLaser.localEulerAngles;
        transformLaser.localEulerAngles = new Vector3(anguloObjetivoX, rotacionActual.y, rotacionActual.z);

        // 2. EL MARTILLAZO: Apagamos el script GiroLaser en el servidor
        if (scriptGiroLaser1 != null)
        {
            scriptGiroLaser1.gameObject.SetActive(false);
        }

        if (scriptGiroLaser2 != null)
        {
            scriptGiroLaser2.gameObject.SetActive(false);
        }

        // 3. Llamar a los clientes si necesitas el cambio de color o congelar físicas
        RpcBloqueoVisuales();
    }

    private void RpcBloqueoVisuales()
    {
        // Esto se ejecuta en todos los jugadores al mismo tiempo que se apaga el script.
        // Aquí puedes meter tus cambios de tintcolor de los materiales o cualquier otro efecto visual.
    }
}