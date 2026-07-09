using UnityEngine;
using TMPro;
using System.IO;

public class IdGetVisorSIMPLE : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textidGafas;
    [SerializeField]
    private string estePaquete;

    void Start()
    {
        textidGafas.text = LeerIDSet(estePaquete);
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
                return "No exist: " + archivo;
            }
        }
        catch (System.Exception e)
        {
            return "Error read: " + e.Message;
        }
    }


}
