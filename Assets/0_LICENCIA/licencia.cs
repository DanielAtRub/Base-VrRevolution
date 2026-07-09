using System.Collections;
using UnityEngine;
using System.IO;

public class licencia : MonoBehaviour
{
    [SerializeField]
    private string EsteCentroLicenciado;
    public bool LicenciaOk;
    [SerializeField]
    private GameObject falloMsj;

    [SerializeField]
    private string[] lines;

    // Start is called before the first frame update
    void Start()
    {
        //if (File.Exists("C:/VrAirsoftGames/License.lic"))
        ReadString();
        StartCoroutine(GetLicencia(EsteCentroLicenciado));
        InvokeRepeating("Lic", 0, 5); // CADENCIA LECTURA DE B.D.
    }

    public void ReadString()
    {
        lines = File.ReadAllLines("C:/VrAirsoftGames_BD/Settings.global");
        EsteCentroLicenciado = lines[0];
    }

    void Lic()
    {
        StartCoroutine(GetLicencia(EsteCentroLicenciado));
    }

    IEnumerator GetLicencia(string Centro) // LEE B.D.
    {
        LicenciaOk = false;

        // SI NO HAY INTERNET ACTIVA falloMsj
        StartCoroutine(StartPing());

        WWW hs_get = new WWW("https://www.inusualinteractive.com/phpJuego/licencias.php");
        yield return hs_get;

        if (hs_get.error != null)
            Debug.Log("LEE LICENCIA DE B.D. There was an error getting: " + hs_get.error);

        string[] campo = hs_get.text.Split("\t".ToCharArray());
        for (int i = 0; i < campo.Length - 1; i++)
        {
            //Debug.Log("CAMPOS: " + campo[i]);
            if (campo[i] == EsteCentroLicenciado)
                LicenciaOk = false; // LicenciaOk = true; //TEMPORAL PARA ESTE JUEGO 23-05-23
        }
        if (!LicenciaOk)
            falloMsj.SetActive(true);
        else
            falloMsj.SetActive(false); 
    }

    IEnumerator StartPing()
    {
        Ping ping = new Ping("8.8.8.8");
        yield return new WaitForSeconds(2f);
        Debug.Log("Connected"+ ping.isDone);
        if (!ping.isDone)
          falloMsj.SetActive(true);
    }

}
