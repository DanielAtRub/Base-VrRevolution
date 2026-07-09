using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircleLevelTiempo : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    private Image imagen;
    private float tiempoTotal;
    [SerializeField]
    private TextMeshProUGUI textTiempoUI;

    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
        if (GameObject.Find("GameManager"))
            GameManager = GameObject.Find("GameManager");
        tiempoTotal = GameManager.GetComponent<JuegoManager>().TiempoPartidaTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager)
        {
            GameManager = GameObject.Find("GameManager");
        }
        int tp = (int)GameManager.GetComponent<JuegoManager>().TiempoPartida;
        textTiempoUI.text = tp.ToString();
        imagen.fillAmount = tp / tiempoTotal;
    }
}
