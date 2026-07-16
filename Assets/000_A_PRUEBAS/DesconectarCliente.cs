using System.Collections;
using UnityEngine;

public class DesconectarCliente : MonoBehaviour
{
    [SerializeField]
    private string targetPackage = "com.vr.Launcher";

    [SerializeField]
    private JuegoManager juegoManager;

    [SerializeField]
    private float tiempoScore = 10f;

    // Bandera para evitar que el Invoke se llame múltiples veces
    private bool cierreIniciado = false;

    void Start()
    {
        // Si no hay JuegoManager en esta escena, iniciamos el contador automáticamente
        if (juegoManager == null)
        {
            Debug.Log($"[VR Launcher] No hay JuegoManager. Autodesconexión programada en {tiempoScore} segundos.");
            cierreIniciado = true;
            Invoke(nameof(AbreLauncherYCierra), tiempoScore);
        }
    }

    void Update()
    {
        // Si SÍ hay juegoManager, el contador depende de que el TiempoPartida llegue a 0
        if (juegoManager != null && juegoManager.TiempoPartida <= 0 && !cierreIniciado)
        {
            cierreIniciado = true;
            Invoke(nameof(AbreLauncherYCierra), tiempoScore);
        }
    }

    // Se ejecuta automáticamente si cambiamos de escena o el objeto se desactiva/destruye
    private void OnDestroy()
    {
        // Si nos vamos de la escena antes de tiempo, cancelamos el salto al launcher
        if (cierreIniciado)
        {
            Debug.Log("[VR Launcher] Cambio de escena detectado. Abortando transición al launcher.");
            CancelInvoke(nameof(AbreLauncherYCierra));
        }
    }

    private void AbreLauncherYCierra()
    {
        Debug.Log($"[VR Launcher] Iniciando transición hacia: {targetPackage}");

        // 1. Lanzamos la nueva aplicación
        StartApp(targetPackage);

        // 2. Retrasamos el cierre medio segundo para que el SO procese el Intent
        Invoke(nameof(CerrarAppLimpio), 0.5f);
    }

    private void StartApp(string packageName)
    {
        if (Application.isEditor)
        {
            Debug.Log($"[Simulación de Editor] Lanzando Intent hacia: {packageName}");
            return;
        }

        try
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager"))
                    {
                        using (AndroidJavaObject launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", packageName))
                        {
                            if (launchIntent != null)
                            {
                                // Inyectamos los Flags obligatorios para visores VR
                                // 0x10000000 = FLAG_ACTIVITY_NEW_TASK
                                // 0x00008000 = FLAG_ACTIVITY_CLEAR_TASK
                                launchIntent.Call<AndroidJavaObject>("addFlags", 0x10000000);
                                launchIntent.Call<AndroidJavaObject>("addFlags", 0x00008000);

                                // Disparamos el Intent
                                currentActivity.Call("startActivity", launchIntent);
                            }
                            else
                            {
                                Debug.LogError($"[ERROR] Intent nulo. El visor no reconoce el paquete '{packageName}'. Revisa si está instalado.");
                            }
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("[ERROR] Error nativo al lanzar la app: " + e.Message);
        }
    }

    private void CerrarAppLimpio()
    {
        if (!Application.isEditor)
        {
            try
            {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        currentActivity.Call("finish");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Error en el finish de Android: " + e.Message);
            }
        }

        Application.Quit();
    }
}