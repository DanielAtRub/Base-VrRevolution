using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

//ABRE EL LAUNCHER Y CIERRA EL JUEGO
public class AbreLyCierraSERVER : MonoBehaviour
{
    [SerializeField]
    private string pathLauncherServer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LanzaLauncher() //LAMADO DESDE EL BOTON QUIT
    {
        Process foo = new Process();
        foo.StartInfo.FileName = pathLauncherServer;
        foo.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        foo.Start();

        Invoke("quit", 2);
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
