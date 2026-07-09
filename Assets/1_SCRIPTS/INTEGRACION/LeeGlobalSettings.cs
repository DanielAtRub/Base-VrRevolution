using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mirror;

public class LeeGlobalSettings : NetworkBehaviour
{
    public string EsteCentro, EstaSala;
    [SerializeField]
    private string[] lines;

    // Start is called before the first frame update
    [ServerCallback]
    void Start()
    {
        //if (File.Exists("C:/VrAirsoftGames/License.lic"))
        ReadString();
        //InvokeRepeating("ReadString", 0, 5); // CADENCIA LECTURA DE Settings.global
    }

    [Server]
    private void ReadString()
    {
        //EsteCentroLicenciado = File.ReadAllText("C:/VrAirsoftGames/Settings.global");
        lines = File.ReadAllLines("C:/VrAirsoftGames_BD/Settings.global");
        EsteCentro = lines[1];
        EstaSala = lines[2];
    }

}
