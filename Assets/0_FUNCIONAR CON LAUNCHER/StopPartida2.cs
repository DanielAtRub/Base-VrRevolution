using UnityEngine;
using System.Diagnostics;

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

    // Start is called before the first frame update
    void Start()
    { 
    }

    public void ParaPartida() //LAMADO DESDE BOTON STOP
    {
        /*
        //PARA PARTIDA
        netGui = GameObject.Find("NetworkManager");
        netGui.GetComponent<Mirror.NetworkManagerHUD>().StopButtons();
        //GUARDA SCORES
        */

        GameManager.GetComponent<JuegoManager>().TiempoPartida = 0; //VRARENA
        Invoke("LanzaLauncher", tiempoExit);
        //LanzaLauncher(); //QUITA EL JUEGO DIRECTAMENTE
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
