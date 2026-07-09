using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager, UbicaPlayers;
    [SerializeField]
    private float DefaultScaleMov, DefaultFactorUbic;
    [SerializeField]
    private float ScaleMov, FactorUbic;
    [SerializeField]
    private TMP_InputField inputScaleMov, inputFactorUbic;
    //MARKETING
    [SerializeField]
    private Toggle ToggleMarketing;
    [SerializeField]
    private GameObject MarketingCanvas;
    //PASSWORD
    [SerializeField]
    private TMP_InputField inputPass;
    [SerializeField]
    private GameObject PanelPass;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SettingsScale"))
            PlayerPrefs.SetFloat("SettingsScale", DefaultScaleMov);
        if (!PlayerPrefs.HasKey("SettingsFactor"))
            PlayerPrefs.SetFloat("SettingsFactor", DefaultFactorUbic);
    }

    public void Activacion()
    {
        inputPass.text = "";
        PanelPass.SetActive(true);

        ScaleMov = PlayerPrefs.GetFloat("SettingsScale", ScaleMov);
        FactorUbic = PlayerPrefs.GetFloat("SettingsFactor", FactorUbic);

        inputScaleMov.text = ScaleMov.ToString();
        inputFactorUbic.text = FactorUbic.ToString();
    }

    public void SetSettings()
    {
        PlayerPrefs.SetFloat("SettingsScale", float.Parse(inputScaleMov.text));
        PlayerPrefs.SetFloat("SettingsFactor", float.Parse(inputFactorUbic.text));

        GameManager.GetComponent<JuegoManager>().ScalaMovGlobal = ScaleMov;
        UbicaPlayers.GetComponent<UbicaPlayers>().FactorUbica = FactorUbic;
    }

    public void Password()
    {
        //ALGORITMO
        char[] fecha;
        char[] fechaRefChar;
        string fechaRef;
        int code;

        fecha = System.DateTime.Now.ToString("ddMMyyyy").ToCharArray();

        fechaRefChar = (fecha[1].ToString() + fecha[0].ToString() + fecha[3].ToString() + fecha[2].ToString() +
            fecha[6].ToString() + fecha[4].ToString() + fecha[5].ToString() + fecha[7].ToString()).ToCharArray();

        fechaRef = fecha[1].ToString() + fecha[0].ToString() + fecha[3].ToString() + fecha[2].ToString() +
            fecha[6].ToString() + fecha[4].ToString() + fecha[5].ToString() + fecha[7].ToString();

        code = int.Parse(fechaRef);
        code = (int)((code / 2.8) * 3.18f);

        //COMPROBACION
        if (inputPass.text == code.ToString())
            PanelPass.SetActive(false);
    }

    public void Marketing()
    {
        if (ToggleMarketing.isOn)
        {
            GameManager.GetComponent<JuegoManager>().isMARKETING = true;
            MarketingCanvas.SetActive(true);
        }
        else
        {
            GameManager.GetComponent<JuegoManager>().isMARKETING = false;
            MarketingCanvas.SetActive(false);
        }
    }
}
