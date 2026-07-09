using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UbicaPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Players;
    //ICONOS
    [SerializeField]
    private TextMeshProUGUI[] Nombres; 
    [SerializeField]
    private RectTransform[] IconosPlayers;
    public float FactorUbica = 400f;

    // Start is called before the first frame update
    void Start()
    {
        //LEE FACTOR DE GLOBAL SETTINGS
        FactorUbica = PlayerPrefs.GetFloat("SettingsFactor", 254f);
    }

    // Update is called once per frame
    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Jugador");

        for(int i = 0; i < Players.Length; i++)
        {
            IconosPlayers[i].gameObject.SetActive(true);
            Nombres[i].text = Players[i].GetComponent<Player>().Name;
            IconosPlayers[i].localPosition = new Vector2(Players[i].transform.GetChild(0).localPosition.z * FactorUbica,
                -Players[i].transform.GetChild(0).localPosition.x * FactorUbica);
        }
    }
}
