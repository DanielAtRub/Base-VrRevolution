using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeOffline : MonoBehaviour
{
    [SerializeField]
    private char[] fecha;
    [SerializeField]
    private char[] fechaRefChar;
    [SerializeField]
    private string fechaRef;
    public int code;
    [SerializeField]
    private TMP_InputField InputCode;

    // Start is called before the first frame update
    void Start()
    {
        //LEE CODE GUARDADO
        InputCode.text = GetCode("CodeNoNet");

        //ALGORITMO 2
        fecha = System.DateTime.Now.ToString("ddMMyyyy").ToCharArray();

        fechaRefChar = (fecha[1].ToString() + fecha[0].ToString() + fecha[4].ToString() + fecha[3].ToString() +
            fecha[2].ToString() + fecha[7].ToString() + fecha[5].ToString() + fecha[6].ToString()).ToCharArray();

        fechaRef = fecha[1].ToString() + fecha[0].ToString() + fecha[4].ToString() + fecha[3].ToString() +
            fecha[2].ToString() + fecha[7].ToString() + fecha[5].ToString() + fecha[6].ToString();

        code = int.Parse(fechaRef);
        code = (int)(((code / 2.24) + 1437) * 3.56f);
    }

    public void SetCode()
    {
        PlayerPrefs.SetString("CodeNoNet", InputCode.text);
    }
    private string GetCode(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }
}
