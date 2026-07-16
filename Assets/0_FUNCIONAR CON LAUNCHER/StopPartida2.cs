using UnityEngine;
using System.Diagnostics;
using System.Collections; // Necesario para usar Corrutinas

public class StopPartida2 : MonoBehaviour
{
    private GameObject netGui;
    [SerializeField]
    private string pathLauncherServer;
    private bool trig;
    [SerializeField]
    private GameObject GameManager;
    [SerializeField]
    private float tiempoExit;

    void Start()
    {
    }

    // NUEVO MÉTODO: Llama a este método para sacar al servidor en 150 segundos
    public void ApagarServidorEn150Segundos()
    {
        UnityEngine.Debug.Log("Iniciando secuencia: el servidor se cerrará en 150 segundos.");
        StartCoroutine(RutinaApagarServidor(150f));
    }

    // Corrutina que maneja la espera
    private IEnumerator RutinaApagarServidor(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);

        // Ejecutamos la misma lógica que tenías pensada para la salida
        if (GameManager != null)
        {
            GameManager.GetComponent<JuegoManager>().TiempoPartida = 0; //VRARENA
            UnityEngine.Debug.Log("--JUEGO CERRADO--");
        }

        LanzaLauncher();
    }

    public void ParaPartida() //LLAMADO DESDE BOTON STOP
    {
        /*
        //PARA PARTIDA
        netGui = GameObject.Find("NetworkManager");
        netGui.GetComponent<Mirror.NetworkManagerHUD>().StopButtons();
        //GUARDA SCORES
        */

        GameManager.GetComponent<JuegoManager>().TiempoPartida = 0; //VRARENA

        // Si prefieres usar tu sistema original con el tiempo del Inspector:
        Invoke("LanzaLauncher", tiempoExit);
    }

    public void LanzaLauncher()
    {
        if (!trig)
        {
            trig = true;

            Process foo = new Process();
            foo.StartInfo.FileName = pathLauncherServer;
            foo.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            foo.Start();

            Invoke("quit", 2);
        }
    }

    void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}