using UnityEngine;
using TMPro;
using System.IO;
using Mirror;

public class IdGetVisor : NetworkBehaviour
{
    public int idGafas;
    [SerializeField]
    private string estePaquete;
    //[SerializeField]
    //private TextMeshProUGUI textidGafas;

    void Start()
    {
        if (!isLocalPlayer) return;

        idGafas = int.Parse(LeerIDSet(estePaquete));

        CmdSetidGafas(idGafas);
    }

    [Command]
    void CmdSetidGafas(int id)
    {
        idGafas = id;
    }

    public string LeerIDSet(string paquete)
    {
        string archivo = "/sdcard/Android/media/" + paquete + "/files/idset.txt";
        try
        {
            if (File.Exists(archivo))
            {
                return File.ReadAllText(archivo);
            }
            else
            {
                return "No existe el archivo en: " + archivo;
            }
        }
        catch (System.Exception e)
        {
            return "Error leyendo archivo: " + e.Message;
        }
    }
}
